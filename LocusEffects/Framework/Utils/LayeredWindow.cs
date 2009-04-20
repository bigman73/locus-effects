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
