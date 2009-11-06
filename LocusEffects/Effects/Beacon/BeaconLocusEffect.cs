#region © Copyright 2005, 2009 Yuval Naveh, Locus Effects. LGPL.
/* Locus Effects
 
    © Copyright 2005, 2009, Yuval Naveh.
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Resources;
using System.Reflection;
using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    public enum BeaconEffectStyles { None, Shrink, HeartBeat } ;

    /// <summary>
    /// BeaconLocusEffect -
    ///		A predefined beacon Locus Effect
    /// </summary>
    public class BeaconLocusEffect : BaseStandardEffect
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BeaconLocusEffect"/> class.
        /// </summary>
        public BeaconLocusEffect()
        {
            m_leadInTime = 0;
            m_animationTime = 800;
            m_leadOutTime = 200;
            m_anchoringMode = AnchoringMode.Center;
            m_showShadow = false;

            // Default colors						
            m_animationStartColor = Color.Yellow;
            m_animationEndColor = Color.Yellow;
            m_animationOuterColor = Color.FromArgb( 220, 220, 0 );
        }


        #endregion

        #region Public properties

        /// <summary>
        /// Color of outer ring
        /// </summary>
        public Color AnimationOuterColor
        {
            get
            {
                return m_animationOuterColor;
            }
            set
            {
                if ( m_animationOuterColor == value )
                {
                    return;
                }

                m_animationOuterColor = value;
            }
        }

        /// <summary>
        /// Flag for setting the beacon's broken ring mode on/off
        /// </summary>
        public bool BrokenRing
        {
            get
            {
                return m_brokenRing;
            }
            set
            {
                if ( m_brokenRing == value )
                {
                    return;
                }

                m_brokenRing = value;
            }
        }

        /// <summary>
        /// Width of beacon ring
        /// </summary>
        public int RingWidth
        {
            get
            {
                return m_ringWidth;
            }
            set
            {
                if ( m_ringWidth == value )
                {
                    return;
                }

                m_ringWidth = value;
            }
        }

        /// <summary>
        /// Width of beacon's outer ring
        /// </summary>
        public int OuterRingWidth
        {
            get
            {
                return m_outerRingWidth;
            }
            set
            {
                if ( m_outerRingWidth == value )
                {
                    return;
                }

                m_outerRingWidth = value;
            }
        }

        /// <summary>
        /// Broken ring margin
        /// </summary>
        public int BrokenRingMargin
        {
            get
            {
                return m_brokenRingMargin;
            }
            set
            {
                if ( m_brokenRingMargin == value )
                {
                    return;
                }

                m_brokenRingMargin = value;
            }
        }


        /// <summary>
        /// Flag for setting beacon rotation mode on/off
        /// </summary>
        public bool Rotate
        {
            get
            {
                return m_rotate;
            }
            set
            {
                if ( m_rotate == value )
                {
                    return;
                }

                m_rotate = value;
            }
        }

        /// <summary>
        /// Rotation speed in degrees - Amount of degrees to rotate per second
        /// </summary>
        public float RotatationSpeed
        {
            get
            {
                return m_rotatationSpeed;
            }
            set
            {
                if ( m_rotatationSpeed == value )
                {
                    return;
                }

                m_rotatationSpeed = value;
            }
        }

        /// <summary>
        /// Enumeration of controlling beacon style
        /// </summary>
        public BeaconEffectStyles Style
        {
            get
            {
                return m_style;
            }
            set
            {
                if ( m_style == value )
                {
                    return;
                }

                m_style = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial size.
        /// </summary>
        /// <value>The initial size.</value>
        public Size InitialSize
        {
            get
            {
                return m_initialSize;
            }
            set
            {
                if ( m_initialSize == value )
                {
                    return;
                }
                m_initialSize = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to rotate in laundry machine style.
        /// </summary>
        /// <value><c>true</c> if laundry machine style; otherwise, <c>false</c>.</value>
        public bool RotateLaundry
        {
            get
            {
                return m_rotateLaundry;
            }
            set
            {
                if ( m_rotateLaundry == value )
                {
                    return;
                }

                m_rotateLaundry = value;
            }
        }


        /// <summary>
        /// Gets or sets the heart beat cycles.
        /// </summary>
        /// <value>The heart beat cycles.</value>
        public int HeartBeatCycles
        {
            get
            {
                return m_heartBeatCycles;
            }
            set
            {
                if ( m_heartBeatCycles == value )
                {
                    return;
                }

                m_heartBeatCycles = value;
            }
        }

        /// <summary>
        /// Gets or sets the laundry cycles.
        /// </summary>
        /// <value>The laundry cycles.</value>
        public int LaundryCycles
        {
            get
            {
                return m_laundryCycles;
            }
            set
            {
                if ( m_laundryCycles == value )
                {
                    return;
                }

                m_laundryCycles = value;
            }
        }

        /// <summary>
        /// Gets or sets the laundry angle.
        /// </summary>
        /// <value>The laundry angle.</value>
        public int LaundryAngle
        {
            get
            {
                return m_laundryAngle;
            }
            set
            {
                if ( m_laundryAngle == value )
                {
                    return;
                }

                m_laundryAngle = value;
            }
        }


        #endregion

        #region Public methods

        /// <summary>
        /// Provies standard animation sequence logic - lead in, body, lead out
        /// </summary>
        protected override void AnimateEffect()
        {
            base.m_runTimeData.EffectBitmap = new Bitmap( m_initialSize.Width, m_initialSize.Height, PixelFormat.Format32bppArgb );
            m_rotationAngle = 0;

            using ( System.Drawing.Graphics g = Graphics.FromImage( base.m_runTimeData.EffectBitmap ) )
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear( Color.Transparent );
            }

            base.AnimateEffect();
        }


        #endregion

        #region Protected methods

        /// <summary>
        /// Paint animation for body step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintBodyAnimation( Graphics graphics )
        {
            #region Diagnostics
            CommonData.WriteDiagnosticsLine( "PaintBodyAnimation" );
            #endregion

            base.PaintBodyAnimation( graphics );

            Size bitmapSize;
            if ( m_style == BeaconEffectStyles.Shrink )
            {
                if ( m_animationStep < MaxAnimationStepThreshold )
                {
                    bitmapSize = new Size(
                        m_initialSize.Width - ( int ) ( m_animationStep * m_initialSize.Width / 100 ),
                        m_initialSize.Height - ( int ) ( m_animationStep * m_initialSize.Height / 100 ) );
                }
                else
                {
                    bitmapSize = new Size(
                        m_initialSize.Width - ( int ) ( MaxAnimationStepThreshold * m_initialSize.Width / 100 ),
                        m_initialSize.Height - ( int ) ( MaxAnimationStepThreshold * m_initialSize.Height / 100 ) );
                }
            }
            // Heart Beat
            else if ( m_style == BeaconEffectStyles.HeartBeat )
            {
                int heartBeatSize = ( int ) ( m_heartBeatAmplitude * Math.Cos( m_animationStep * m_heartBeatCycles / 180 * Math.PI ) );
                bitmapSize = new Size(
                    m_initialSize.Width + heartBeatSize,
                    m_initialSize.Height + heartBeatSize );
            }
            // No size change: None, Laundry
            else
            {
                bitmapSize = m_initialSize;
            }
            Bitmap bitmap = new Bitmap( bitmapSize.Width, bitmapSize.Height, PixelFormat.Format32bppArgb );

            using ( System.Drawing.Graphics g = Graphics.FromImage( bitmap ) )
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear( Color.Transparent );

                // Paint outer ring
                using ( SolidBrush outerBrush = new SolidBrush( m_animationOuterColor ) )
                {
                    g.FillEllipse( outerBrush, new Rectangle( 0, 0, bitmap.Width - 1, bitmap.Height - 1 ) );
                }

                // Paint inner ring
                using ( SolidBrush innerBrush = new SolidBrush( m_animationEndColor ) )
                {
                    g.FillEllipse(
                        innerBrush,
                        new Rectangle(
                        m_outerRingWidth,
                        m_outerRingWidth,
                        bitmap.Width - 2 - m_outerRingWidth * 2,
                        bitmap.Height - 2 - m_outerRingWidth * 2 ) );
                }

                using ( SolidBrush transparentBrush = new SolidBrush( Color.Transparent ) )
                {
                    if ( m_brokenRing )
                    {
                        g.FillRectangle( transparentBrush, new Rectangle( m_brokenRingMargin, m_brokenRingMargin, bitmap.Width - m_brokenRingMargin * 2 - 1, bitmap.Height - m_brokenRingMargin * 2 - 1 ) );
                    }

                    // Clear center of ring
                    if ( bitmap.Width >= m_ringWidth * 2 )
                    {
                        g.FillEllipse( transparentBrush,
                            new Rectangle(
                                m_ringWidth - 1,
                                m_ringWidth - 1,
                                bitmap.Width - 2 - m_ringWidth * 2,
                                bitmap.Height - 1 - m_ringWidth * 2 ) );
                    }
                }

                if ( m_runTimeData.EffectBitmap != null )
                {
                    m_runTimeData.EffectBitmap.Dispose();
                    m_runTimeData.EffectBitmap = null;
                }
            }

            // Rotate bitmap
            if ( m_rotate )
            {
                if ( m_rotateLaundry )
                {
                    m_rotationAngle = ( float ) ( m_laundryAngle * Math.Sin( m_animationStep * m_laundryCycles * Math.PI / 180 ) );
                }
                else
                {
                    m_rotationAngle = m_animationStep / 100 * m_animationTime / 1000 * m_rotatationSpeed;
                }

                Bitmap rotatedBitmap = BitmapUtilities.RotateImage( bitmap, m_rotationAngle );
                bitmap.Dispose();
                bitmap = rotatedBitmap;
            }

            m_runTimeData.EffectBitmap = bitmap;
            if ( m_bodyFadeOut )
            {
                m_runTimeData.Opacity = ( int ) ( m_initialOpacity * ( 100 - m_animationStep ) / 100 );
            }
        }

        #endregion

        #region Protected members

        protected Color m_animationOuterColor = Color.Transparent;
        protected int m_outerRingWidth = 1;
        protected int m_ringWidth = 6;
        protected bool m_brokenRing = false;
        protected int m_brokenRingMargin = 6;
        protected bool m_rotate = false;
        protected float m_rotatationSpeed = 360.0f; // Degrees per second
        protected float m_rotationAngle = 0; // Degrees
        protected BeaconEffectStyles m_style = BeaconEffectStyles.Shrink;
        protected Size m_initialSize = new Size( 200, 200 );
        protected int m_heartBeatCycles = 12;
        protected bool m_rotateLaundry = false;
        protected int m_heartBeatAmplitude = 30;
        protected int m_laundryCycles = 4;
        protected int m_laundryAngle = 60;

        #endregion

        #region Private constants

        private const int MaxAnimationStepThreshold = 95;

        #endregion
    }
}
