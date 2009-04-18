#region © Copyright 2005, BigMan's Stuff - Yuval Naveh, Locus Effects
// Locus Effects
// 
// © Copyright 2005, BigMan's Stuff - Yuval Naveh
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//  * Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer. 
//  * Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution. 
//  * Neither the name of BigMan's Stuff, Locus Effects, nor the names of its contributors 
//    may be used to endorse or promote products derived from this software
//    without specific prior written permission. 
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System ;
using System.Collections ;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging ;
using System.Resources ;
using System.Reflection ;
using System.ComponentModel ;
using System.Windows.Forms ;


namespace BigMansStuff.LocusEffects
{
	/// <summary>
    /// AnimatedImageLocusEffect -
    ///		A predefined locus effect for displaying an animated sequence of images
    ///	</summary>
	public class AnimatedImageLocusEffect: BaseStandardEffect
	{
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AnimatedImageLocusEffect()
        {
            m_leadInTime = 1000 ;
            m_animationTime = 1200 ;
            m_leadOutTime = 1000 ;
            m_anchoringMode = AnchoringMode.Center ;
            m_showShadow = false ;

            m_frameSequence = new ArrayList() ;

            this.CreateDefaultImageSequence() ;
		}


        #endregion

        #region Dispose

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose() ;

            if ( m_frameSequence != null )
            {
                this.DisposeFrames() ;
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
                    frame.Dispose() ;
                }

                m_frameSequence.Clear() ;
                m_frameIndex = 0 ;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Clears all frames in the sequence
        /// </summary>
        public void ClearAllFrames()
        {
            this.DisposeFrames() ;
        }

        /// <summary>
        /// Adds frames in the given image to this instance's frame sequence
        /// </summary>
        /// <param name="image"></param>
        public void AddImageFrames( Image image )
        {
            if ( image == null )
            {
                throw new ArgumentNullException( "image" ) ;
            }

            // Create a new FrameDimension object from this image
            System.Drawing.Imaging.FrameDimension frameDimensions = new System.Drawing.Imaging.FrameDimension( image.FrameDimensionsList[ 0 ] ) ;

            int numberOfFrames = image.GetFrameCount( frameDimensions ) ;

            // Check for valid image
            if ( numberOfFrames <= 0 )
            {
                throw new ArgumentException( "Provided image has no frames", "image" ) ;
            }
                    
            // Get HasFrameDuration flag (PropertyTagFrameDelay) from source image
            System.Drawing.Imaging.PropertyItem propertyItem = GetFrameDurationPropertyItem( image ) ;
            bool hasFrameDuration = ( propertyItem != null ) ;

            // TODO: Check if there is a loop flag on
            int frameDuration ;

            // Extract each frame in the image, and import into our animated frame sequence
            for ( int frameIndex = 0 ; frameIndex < numberOfFrames ; frameIndex++ )
            {
                // Select frame in source image by index
                image.SelectActiveFrame( frameDimensions, frameIndex ) ;

                if ( hasFrameDuration )
                {
                    // Get frame duration of the active frame
                    frameDuration = 
                        propertyItem.Value[ frameIndex * 4 ] +
                        0x100 * propertyItem.Value[ frameIndex * 4 + 1 ] +
                        0x10000 * propertyItem.Value[ frameIndex * 4 + 2 ] +
                        0x1000000 * propertyItem.Value[ frameIndex * 4 + 3 ] ;
                }
                else
                {
                    // No frame duration
                    frameDuration = 0 ;
                }

                // Extract the active frame image into a memory stream, and make it a stand alone bitmap
                Bitmap frameBitmap = null ;
                System.IO.MemoryStream imageMemoryStream = new System.IO.MemoryStream() ;
                try
                {
                    image.Save( imageMemoryStream, System.Drawing.Imaging.ImageFormat.Png ) ; 
 
                    frameBitmap = new Bitmap( imageMemoryStream ) ;
                }
                finally
                {
                    imageMemoryStream.Close() ;
                }

                // Create a new animated image frame
                AnimatedImageFrame imageFrame = new AnimatedImageFrame() ;
                // Set its properties
                imageFrame.Duration = frameDuration ;
                imageFrame.Bitmap = frameBitmap ;
                imageFrame.Index = frameIndex ;

                // Add it to the frame sequence
                m_frameSequence.Add( imageFrame ) ;
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
                    return m_defaultFocusPoint ;
                }

                return m_focusPoint ;
            }
            set
            {
                if ( m_focusPoint == value )
                    return ;
                m_focusPoint = value ;
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
            // TODO: Move default animated image to AnimatedImageLocusEffect
            if ( m_defaultAnimatedImage == null )
            {
                m_defaultFrameSequence = new ArrayList() ;    
                
                // Load default bitmap just in time
                // Note: Default arrow orientation is SW			
                ResourceManager rm = new ResourceManager( "BigMansStuff.LocusEffects.Effects.AnimatedImage.AnimatedImageLocusEffect_Images", Assembly.GetExecutingAssembly() ) ;
                m_defaultAnimatedImage = rm.GetObject( "DefaultAnimatedImage" ) as Image ;

                // Add all frames from default animated image
                if ( m_defaultAnimatedImage != null )
                {
                    this.AddImageFrames( m_defaultAnimatedImage );

                    m_focusPoint = new Point( -30, 0 ) ;
                }
            }
        }


        /// <summary>
        /// Provies standard animation sequence logic - lead in, body, lead out
        /// </summary>
        protected override void AnimateEffect()
        {
            Size bitmapSize = ( FrameSequence[ 0 ] as AnimatedImageFrame ).Bitmap.Size ;

            base.m_runTimeData.EffectBitmap = new Bitmap( bitmapSize.Width, bitmapSize.Height, PixelFormat.Format32bppArgb ) ;
            m_remainingAnimationLoops = m_animationLoops - 1;
            m_frameIndex = 0;
            
            using ( System.Drawing.Graphics g = Graphics.FromImage( base.m_runTimeData.EffectBitmap ) )
            {
                g.CompositingMode = CompositingMode.SourceCopy ;
                g.SmoothingMode = SmoothingMode.HighQuality ;

                g.Clear( Color.Transparent ) ;
            }

            base.AnimateEffect() ;
        }

        /// <summary>
        /// Paint animation for lead in step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintLeadIn( Graphics graphics )
        {
            base.PaintLeadIn( graphics ) ;
            
            PaintImageFrame() ;

            // TODO: Refactor FadeIn/FadeOut logic to base class (Standard effect)
            m_runTimeData.Opacity = ( int ) ( m_initialOpacity * ( m_animationStep ) / 100 ) ; 
        }

        /// <summary>
        /// Paint animation for body step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintBodyAnimation( Graphics graphics )
        {
            base.PaintBodyAnimation( graphics ) ;

            PaintImageFrame();

            m_runTimeData.Opacity = m_initialOpacity ;
        }


        /// <summary>
        /// Paint animation for lead out step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintLeadOut( Graphics graphics )
        {
            base.PaintLeadOut( graphics ) ;

            PaintImageFrame() ;

            m_runTimeData.Opacity = ( int ) ( m_initialOpacity * ( 100 - m_animationStep ) / 100 ) ; 
        }


        /// <summary>
        /// Paints the image frame.
        /// </summary>
        protected void PaintImageFrame()
        {
            Size bitmapSize = ( FrameSequence[ m_frameIndex ] as AnimatedImageFrame ).Bitmap.Size ;

            Bitmap bitmap = new Bitmap( bitmapSize.Width, bitmapSize.Height, PixelFormat.Format32bppArgb ) ;

            using ( System.Drawing.Graphics g = Graphics.FromImage( bitmap ) )
            {
                g.CompositingMode = CompositingMode.SourceCopy ;
                g.SmoothingMode = SmoothingMode.HighQuality ;

                // Get current frame bitmap
                AnimatedImageFrame imageFrame = FrameSequence[ m_frameIndex ] as AnimatedImageFrame ;
                
                // Draw current frame bitmap
                g.DrawImageUnscaled( imageFrame.Bitmap, 0, 0, bitmapSize.Width, bitmapSize.Height ) ;

                // Advance to next frame
                IncrementFrameIndex() ;

                if ( m_runTimeData.EffectBitmap != null )
                {
                    m_runTimeData.EffectBitmap.Dispose() ;
                    m_runTimeData.EffectBitmap = null ;
                }
            }

            m_runTimeData.EffectBitmap = bitmap ;
            m_runTimeData.MovementOffset = FocusPoint ;
        }

        /// <summary>
        /// Gets the frame duration property item from the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        private System.Drawing.Imaging.PropertyItem GetFrameDurationPropertyItem( Image image )
        {
            System.Drawing.Imaging.PropertyItem propertyItem = null ;
            ArrayList propertyIds = new ArrayList( image.PropertyIdList ) ;
           
            if ( propertyIds.IndexOf( PropertyTagFrameDelay ) >= 0 )
            {
                propertyItem = image.GetPropertyItem( PropertyTagFrameDelay ) ;
            }

            return propertyItem;
        }

        /// <summary>
        /// Increments the index of the frame.
        /// </summary>
        protected void IncrementFrameIndex()
        {
            m_frameIndex++ ;
            
            if ( m_frameIndex >= FrameSequence.Count )
            {
                if ( m_animationLoops <= 0 )
                {
                    m_frameIndex = 0 ;
                }
                else
                {
                    if ( m_remainingAnimationLoops > 0 )
                    {
                        m_frameIndex = 0 ;
                        m_remainingAnimationLoops-- ;
                    }
                    else
                    {
                        m_frameIndex = FrameSequence.Count - 1 ;
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
        protected ArrayList FrameSequence
        {
            get
            {
                if ( m_frameSequence.Count <= 0 )
                    return m_defaultFrameSequence ;

                return m_frameSequence ;
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

        protected ArrayList m_frameSequence = null ;
        protected int m_frameIndex = 0 ;
        protected int m_animationLoops = 0;
        /// <summary>
        /// Runtime data
        /// </summary>
        protected int m_remainingAnimationLoops = 0;

        protected Point m_focusPoint = Point.Empty ;
        protected Point m_defaultFocusPoint = Point.Empty ;
                
        protected static ArrayList m_defaultFrameSequence ;
        protected static Image m_defaultAnimatedImage = null ;

        #endregion

        #region Constants
        protected const int PropertyTagFrameDelay = 0x5100 ;
        #endregion
	}
}
