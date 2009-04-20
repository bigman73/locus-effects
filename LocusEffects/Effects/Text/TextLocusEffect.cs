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

