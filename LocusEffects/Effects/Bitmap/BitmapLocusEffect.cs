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
using System.ComponentModel;
using System.Resources;
using System.Reflection;
using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// BitmapLocusEffect -
    ///		A predefined bitmap Locus Effect
    /// </summary>
    public class BitmapLocusEffect : BaseBitmapEffect
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapLocusEffect"/> class.
        /// </summary>
        public BitmapLocusEffect()
        {
            m_animationStartColor = Color.FromArgb( 0, 0, 200 );
            m_animationEndColor = Color.FromArgb( 0, 50, 255 );
        }


        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the initial run time data.
        /// </summary>
        protected override void SetInitialRunTimeData()
        {
            // Load default bitmap
            if ( m_defaultBitmap == null )
            {
                // Load default bitmap just in time
                // Note: Default arrow orientation is SW			
                ResourceManager rm = new ResourceManager( "BigMansStuff.LocusEffects.Effects.Bitmap.BitmapLocusEffect_Images", Assembly.GetExecutingAssembly() );

                m_defaultBitmap = rm.GetObject( "DefaultBitmap" ) as Bitmap;
            }

            // Use default bitmap, if nothing else was set by the user
            if ( m_bitmap == null )
            {
                m_runTimeData.EffectBitmap = m_defaultBitmap.Clone() as Bitmap;
            }
            // Use user defined bitmap
            else
            {
                m_runTimeData.EffectBitmap = m_bitmap.Clone() as Bitmap;
            }
        }

        #endregion

        #region Private Members

        private static Bitmap m_defaultBitmap = null;

        #endregion
    }
}

