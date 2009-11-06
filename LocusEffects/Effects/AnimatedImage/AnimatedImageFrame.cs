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

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// AnimatedImageFrame -
    ///     Data structure for holding data of a single image frame
    /// </summary>
    public class AnimatedImageFrame : IDisposable
    {
        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if ( m_bitmap != null )
            {
                m_bitmap.Dispose();
                m_bitmap = null;
            }
        }

        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public int Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                if ( m_duration == value )
                {
                    return;
                }

                m_duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the bitmap.
        /// </summary>
        /// <value>The bitmap.</value>
        public Bitmap Bitmap
        {
            get
            {
                return m_bitmap;
            }
            set
            {
                if ( m_bitmap == value )
                {
                    return;
                }

                m_bitmap = value;
            }
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index
        {
            get
            {
                return m_index;
            }
            set
            {
                m_index = value;
            }
        }

        #endregion

        #region Private members

        private int m_duration;
        private Bitmap m_bitmap;
        private int m_index;

        #endregion
    }
}
