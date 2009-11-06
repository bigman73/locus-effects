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

