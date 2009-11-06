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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// LayeredWindow -
    ///		A native layered window
    /// </summary>
    public class LayeredWindow : NativeWindow, IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Create the actual native layered window
        /// </summary>
        public void Create()
        {
            CreateParams createParams = new CreateParams();
            createParams.Caption = string.Empty;
            createParams.X = -1;
            createParams.Y = -1;
            createParams.Width = 1;
            createParams.Height = 1;
            createParams.Style = Win32Constants.WS_POPUP;
            createParams.ExStyle =
                Win32Constants.WS_EX_TOOLWINDOW |
                Win32Constants.WS_EX_LAYERED |
                Win32Constants.WS_EX_TOPMOST;

            this.CreateHandle( createParams );
        }


        /// <summary>
        /// Hide the window
        /// </summary>
        public void Hide()
        {
            Win32NativeMethods.ShowWindow( this.Handle, Win32Constants.SW_HIDE );
        }

        /// <summary>
        /// Show the window
        /// </summary>
        public void Show()
        {
            Win32NativeMethods.ShowWindow( this.Handle, Win32Constants.SW_SHOW );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Window bounds
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                Win32NativeMethods.RECT rect = Win32NativeMethods.RECT.Empty;
                Win32NativeMethods.GetWindowRect( this.Handle, ref rect );

                return rect.Bounds;
            }
            set
            {
                Win32NativeMethods.MoveWindow( this.Handle, value.X, value.Y, value.Width, value.Height, true );
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose all allocated resources
        /// </summary>
        public virtual void Dispose()
        {
            if ( this.Handle != IntPtr.Zero )
            {
                this.DestroyHandle();
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Handle window messages
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc( ref Message m )
        {
            // Do not allow this window to become active - it should be "transparent" to mouse clicks
            //  i.e. Mouse clicks pass through this window
            if ( m.Msg == Win32Constants.WM_NCHITTEST )
            {
                m.Result = new IntPtr( Win32Constants.HTTRANSPARENT );
                return;
            }

            base.WndProc( ref m );
        }

        #endregion
    }
}
