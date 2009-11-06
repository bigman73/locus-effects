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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// TextLocusEffect -
    ///		A predefined text Locus Effect
    /// </summary>
    public class TextLocusEffect : BaseBitmapEffect
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLocusEffect"/> class.
        /// </summary>
        public TextLocusEffect()
        {
            m_animationStartColor = Color.Yellow;
            m_animationEndColor = Color.Indigo;
            m_leadInTime = 500;
            m_animationTime = 100;
            m_leadOutTime = 500;
            m_font = new Font( "Arial", 50 );
            m_text = "Test";
            m_shadowOffset = new Point( 1, 1 );
        }


        #endregion

        #region Dispose

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public override void Dispose()
        {
            if ( m_font != null )
            {
                m_font.Dispose();
                m_font = null;
            }

            base.Dispose();
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return m_text;
            }
            set
            {
                m_text = value;
            }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font
        {
            get
            {
                return m_font;
            }
            set
            {
                if ( m_font != null )
                {
                    m_font.Dispose();
                }

                m_font = value.Clone() as Font;
            }
        }

        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        /// <value>The rotation angle.</value>
        public float RotationAngle
        {
            get
            {
                return m_rotationAngle;
            }
            set
            {
                if ( m_rotationAngle != value )
                {
                    m_rotationAngle = value;
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the initial run time data.
        /// </summary>
        protected override void SetInitialRunTimeData()
        {
            if ( m_bitmap == null )
            {
                SizeF textBounds;
                Bitmap textBitmap = new Bitmap( 1, 1 );
                using ( Graphics textGraphics = Graphics.FromImage( textBitmap ) )
                {
                    using ( StringFormat stringFormat = new StringFormat() )
                    {
                        stringFormat.LineAlignment = StringAlignment.Center;
                        stringFormat.Alignment = StringAlignment.Center;

                        textBounds = textGraphics.MeasureString( m_text, m_font, MaxTextWidth, stringFormat );
                    }
                }

                // Create bitmap from text
                textBitmap.Dispose();
                textBitmap = new Bitmap( textBounds.ToSize().Width, textBounds.ToSize().Height );
                using ( Graphics textGraphics = Graphics.FromImage( textBitmap ) )
                {
                    using ( StringFormat stringFormat = new StringFormat() )
                    {
                        stringFormat.LineAlignment = StringAlignment.Center;
                        stringFormat.Alignment = StringAlignment.Center;

                        textGraphics.DrawString( m_text, m_font, Brushes.Black, textBitmap.Width / 2, textBitmap.Height / 2, stringFormat );
                    }
                }

                if ( Math.Abs( m_rotationAngle ) > EpsilonAngle )
                {
                    Bitmap rotatedBitmap = BitmapUtilities.RotateImage( textBitmap, m_rotationAngle );
                    textBitmap.Dispose();
                    textBitmap = rotatedBitmap;
                }

                m_runTimeData.EffectBitmap = textBitmap;
            }
        }

        #endregion

        #region Protected Members

        protected string m_text;
        protected Font m_font;
        protected float m_rotationAngle = 0; // degrees

        #endregion

        #region Constants

        private const int MaxTextWidth = 5000;
        private const float EpsilonAngle = 0.1f;

        #endregion
    }
}

