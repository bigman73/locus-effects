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
