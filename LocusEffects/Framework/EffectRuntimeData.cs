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
using System.Windows.Forms;
using System.Drawing;
using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// EffectRuntimeData -
    ///		Runtime data of the effect when it is animating
    /// </summary>
    public class EffectRuntimeData
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:EffectRuntimeData"/> class.
        /// </summary>
        public EffectRuntimeData()
        {
            StopRequested = false;
            IsAnimating = false;
            Opacity = InitialOpacity;
        }

        #endregion

        #region Public Fields

        /// <summary>
        /// StopRequested is volatile since it is a thread control flag
        /// </summary>
        public volatile bool StopRequested = false;
        public Form ActivatorForm = null;
        public Rectangle LocusScreenBounds;
        public Rectangle LastActivatorFormBounds;
        public Bitmap EffectBitmap = null;
        public System.Threading.Thread AnimationThread = null;
        public bool IsAnimating = false;
        public int Opacity; // %
        public AnchoringCorner AnchoringCorner;
        public Point MovementOffset = Point.Empty;
        public float StepMaxDuration; // msec

        #endregion

        #region Constants

        private const int InitialOpacity = 70; // %

        #endregion
    }
}
