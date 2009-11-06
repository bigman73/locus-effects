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
using System.Windows.Forms;
using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// BaseBitmapEffect -
    ///		An effect that handles constant bitmaps
    /// </summary>
    public abstract class BaseBitmapEffect : BaseStandardEffect
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBitmapEffect"/> class.
        /// </summary>
        public BaseBitmapEffect()
        {
            m_anchoringMode = AnchoringMode.Center;
        }


        #endregion

        #region Public Properties

        /// <summary>
        /// Bitmap used in effect
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return m_bitmap;
            }
            set
            {
                if ( m_bitmap == value )
                    return;
                m_bitmap = value;
            }
        }

        #endregion

        #region Protected Members

        protected Bitmap m_bitmap = null;

        #endregion
    }
}

