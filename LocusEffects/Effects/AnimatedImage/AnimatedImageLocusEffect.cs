#region � Copyright 2005, 2009 Yuval Naveh, Locus Effects. LGPL.
/* Locus Effects
 
    � Copyright 2005, 2009, Yuval Naveh.
     All rights reserved.
 
    This file is part of Locus Effects.

    Locus Effects is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Locus Effects is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser Public License for more details.

    You should have received a copy of the GNU Lesser Public License
    along with Locus Effects.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Resources;
using System.Reflection;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// AnimatedImageLocusEffect -
    ///		A predefined locus effect for displaying an animated sequence of images
    ///	</summary>
    public class AnimatedImageLocusEffect : BaseStandardEffect
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AnimatedImageLocusEffect()
        {
            m_leadInTime = 1000;
            m_animationTime = 1200;
            m_leadOutTime = 1000;
            m_anchoringMode = AnchoringMode.Center;
            m_showShadow = false;
            m_animationStartColor = Color.Transparent;
            m_animationEndColor = Color.Transparent;

            m_frameSequence = new List<AnimatedImageFrame>();

            this.CreateDefaultImageSequence();
        }


        #endregion

        #region Dispose

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            if ( m_frameSequence != null )
            {
                this.DisposeFrames();
            }
        }


        /// <summary>
        /// Disposes the frames.
        /// </summary>
        protected void DisposeFrames()
        {
            // Dispose old frames
            if ( m_frameSequence != null )
            {
                foreach ( AnimatedImageFrame frame in m_frameSequence )
                {
                    frame.Dispose();
                }

                m_frameSequence.Clear();
                m_frameIndex = 0;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Clears all frames in the sequence
        /// </summary>
        public void ClearAllFrames()
        {
            this.DisposeFrames();
        }

        /// <summary>
        /// Adds frames in the given image to this instance's frame sequence
        /// </summary>
        /// <param name="image"></param>
        public void AddImageFrames( Image image )
        {
            if ( image == null )
            {
                throw new ArgumentNullException( "image" );
            }

            // Create a new FrameDimension object from this image
            System.Drawing.Imaging.FrameDimension frameDimensions = new System.Drawing.Imaging.FrameDimension( image.FrameDimensionsList[ 0 ] );

            int numberOfFrames = image.GetFrameCount( frameDimensions );

            // Check for valid image
            if ( numberOfFrames <= 0 )
            {
                throw new ArgumentException( "Provided image has no frames", "image" );
            }

            // Get HasFrameDuration flag (PropertyTagFrameDelay) from source image
            System.Drawing.Imaging.PropertyItem propertyItem = GetFrameDurationPropertyItem( image );
            bool hasFrameDuration = ( propertyItem != null );

            // TODO: Check if there is a loop flag on
            int frameDuration;

            // Extract each frame in the image, and import into our animated frame sequence
            for ( int frameIndex = 0; frameIndex < numberOfFrames; frameIndex++ )
            {
                // Select frame in source image by index
                image.SelectActiveFrame( frameDimensions, frameIndex );

                if ( hasFrameDuration )
                {
                    int frameBase = frameIndex * 4;
                    // Get frame duration of the active frame
                    frameDuration =
                        propertyItem.Value[ frameBase ] +
                        0x100 * propertyItem.Value[ frameBase + 1 ] +
                        0x10000 * propertyItem.Value[ frameBase + 2 ] +
                        0x1000000 * propertyItem.Value[ frameBase + 3 ];
                }
                else
                {
                    // No frame duration
                    frameDuration = 0;
                }

                // Extract the active frame image into a memory stream, and make it a stand alone bitmap
                Bitmap frameBitmap = null;
                System.IO.MemoryStream imageMemoryStream = new System.IO.MemoryStream();
                try
                {
                    image.Save( imageMemoryStream, System.Drawing.Imaging.ImageFormat.Png );

                    frameBitmap = new Bitmap( imageMemoryStream );
                }
                finally
                {
                    imageMemoryStream.Close();
                    imageMemoryStream.Dispose();
                }

                // Create a new animated image frame
                AnimatedImageFrame imageFrame = new AnimatedImageFrame();
                // Set its properties
                imageFrame.Duration = frameDuration;
                imageFrame.Bitmap = frameBitmap;
                imageFrame.Index = frameIndex;

                // Add it to the frame sequence
                m_frameSequence.Add( imageFrame );
            }
        }


        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the focus point.
        /// </summary>
        /// <value>The focus point.</value>
        public Point FocusPoint
        {
            get
            {
                if ( m_frameSequence.Count <= 0 )
                {
                    return m_defaultFocusPoint;
                }

                return m_focusPoint;
            }
            set
            {
                if ( m_focusPoint == value )
                {
                    return;
                }

                m_focusPoint = value;
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Creates the default image sequence.
        /// </summary>
        protected virtual void CreateDefaultImageSequence()
        {
            // Load default bitmap
            if ( m_defaultAnimatedImage == null )
            {
                m_defaultFrameSequence = new List<AnimatedImageFrame>();

                // Load default bitmap just in time
                // Note: Default arrow orientation is SW			
                ResourceManager rm = new ResourceManager( "BigMansStuff.LocusEffects.Effects.AnimatedImage.AnimatedImageLocusEffect_Images", Assembly.GetExecutingAssembly() );
                m_defaultAnimatedImage = rm.GetObject( "DefaultAnimatedImage" ) as Image;

                // Add all frames from default animated image
                if ( m_defaultAnimatedImage != null )
                {
                    this.AddImageFrames( m_defaultAnimatedImage );

                    m_focusPoint = DefaultFocusPoint;
                }
            }
        }

        /// <summary>
        /// Provies standard animation sequence logic - lead in, body, lead out
        /// </summary>
        protected override void AnimateEffect()
        {
            Size bitmapSize = FrameSequence[ 0 ].Bitmap.Size;

            base.m_runTimeData.EffectBitmap = new Bitmap( bitmapSize.Width, bitmapSize.Height, PixelFormat.Format32bppArgb );
            m_remainingAnimationLoops = m_animationLoops - 1;
            m_frameIndex = 0;

            using ( System.Drawing.Graphics g = Graphics.FromImage( base.m_runTimeData.EffectBitmap ) )
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear( Color.Transparent );
            }

            base.AnimateEffect();
        }

        /// <summary>
        /// Paint animation for lead in step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintLeadIn( Graphics graphics )
        {
            base.PaintLeadIn( graphics );

            PaintImageFrame();

            // TODO: Refactor FadeIn/FadeOut logic to base class (Standard effect)
            m_runTimeData.Opacity = ( int ) ( m_initialOpacity * ( m_animationStep ) / 100 );
        }

        /// <summary>
        /// Paint animation for body step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintBodyAnimation( Graphics graphics )
        {
            base.PaintBodyAnimation( graphics );

            PaintImageFrame();

            m_runTimeData.Opacity = m_initialOpacity;
        }

        /// <summary>
        /// Paint animation for lead out step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintLeadOut( Graphics graphics )
        {
            base.PaintLeadOut( graphics );

            PaintImageFrame();

            m_runTimeData.Opacity = ( int ) ( m_initialOpacity * ( 100 - m_animationStep ) / 100 );
        }

        /// <summary>
        /// Paints the image frame.
        /// </summary>
        protected void PaintImageFrame()
        {
            // Get current frame bitmap
            AnimatedImageFrame imageFrame = FrameSequence[ m_frameIndex ];
            Size bitmapSize = imageFrame.Bitmap.Size;

            Bitmap bitmap = new Bitmap( bitmapSize.Width, bitmapSize.Height, PixelFormat.Format32bppArgb );

            using ( System.Drawing.Graphics g = Graphics.FromImage( bitmap ) )
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.SmoothingMode = SmoothingMode.HighQuality;

                // Draw current frame bitmap
                g.DrawImageUnscaled( imageFrame.Bitmap, 0, 0, bitmapSize.Width, bitmapSize.Height );

                // Advance to next frame
                IncrementFrameIndex();

                if ( m_runTimeData.EffectBitmap != null )
                {
                    m_runTimeData.EffectBitmap.Dispose();
                    m_runTimeData.EffectBitmap = null;
                }
            }

            m_runTimeData.EffectBitmap = bitmap;
            m_runTimeData.MovementOffset = FocusPoint;
        }

        /// <summary>
        /// Gets the frame duration property item from the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        private System.Drawing.Imaging.PropertyItem GetFrameDurationPropertyItem( Image image )
        {
            System.Drawing.Imaging.PropertyItem propertyItem = null;
            List<int> propertyIds = new List<int>( image.PropertyIdList );

            if ( propertyIds.IndexOf( PropertyTagFrameDelay ) >= 0 )
            {
                propertyItem = image.GetPropertyItem( PropertyTagFrameDelay );
            }

            return propertyItem;
        }

        /// <summary>
        /// Increments the index of the frame.
        /// </summary>
        protected void IncrementFrameIndex()
        {
            m_frameIndex++;

            if ( m_frameIndex >= FrameSequence.Count )
            {
                if ( m_animationLoops <= 0 )
                {
                    m_frameIndex = 0;
                }
                else
                {
                    if ( m_remainingAnimationLoops > 0 )
                    {
                        m_frameIndex = 0;
                        m_remainingAnimationLoops--;
                    }
                    else
                    {
                        m_frameIndex = FrameSequence.Count - 1;
                    }
                }
            }
        }


        #endregion

        #region Protected properties

        /// <summary>
        /// Gets the frame sequence.
        /// </summary>
        /// <value>The frame sequence.</value>
        protected List<AnimatedImageFrame> FrameSequence
        {
            get
            {
                if ( m_frameSequence.Count <= 0 )
                {
                    return m_defaultFrameSequence;
                }

                return m_frameSequence;
            }
        }

        /// <summary>
        /// Gets or sets the amount animation loops to perfrom.
        /// </summary>
        /// <value>The animation loops.</value>
        /// <remarks>
        /// Value less or equal to zero - Infinite loops
        /// </remarks>
        public int AnimationLoops
        {
            get
            {
                return m_animationLoops;
            }
            set
            {
                m_animationLoops = value;
            }
        }

        #endregion

        #region Protected members

        protected List<AnimatedImageFrame> m_frameSequence = null;
        protected int m_frameIndex = 0;
        protected int m_animationLoops = 0;
        /// <summary>
        /// Runtime data
        /// </summary>
        protected int m_remainingAnimationLoops = 0;

        protected Point m_focusPoint = Point.Empty;
        protected Point m_defaultFocusPoint = Point.Empty;

        protected static List<AnimatedImageFrame> m_defaultFrameSequence;
        protected static Image m_defaultAnimatedImage = null;

        #endregion

        #region Constants

        protected const int PropertyTagFrameDelay = 0x5100;
        protected readonly Point DefaultFocusPoint = new Point( -30, 0 );

        #endregion
    }
}
