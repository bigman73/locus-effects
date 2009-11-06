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
using System.Diagnostics;

namespace BigMansStuff.Common
{
    /// <summary>
    /// CommonData
    /// </summary>
    internal class CommonData
    {
        #region Constructors

        /// <summary>
        /// Hide constructor
        /// </summary>
        private CommonData()
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Writes the diagnostics line.
        /// </summary>
        /// <param name="diagnosticsMessage">The diagnostics message.</param>
        public static void WriteDiagnosticsLine( string diagnosticsMessage )
        {
            if ( m_diagnosticsMode.Enabled )
            {
                System.Diagnostics.Trace.WriteLine( diagnosticsMessage );
            }
        }
        #endregion

        #region Private members
        private static BooleanSwitch m_diagnosticsMode = new BooleanSwitch( "LocusEffectsDiagnostics", "Control LocusEffects Diagnostics Mode" );
        #endregion
    }
}
