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

namespace BigMansStuff.Common
{
    /// <summary>
    /// Win32NativeMethods -
    ///		Interop layer to required Win32 native methods
    /// </summary>
    public class Win32NativeMethods
    {
        #region user32 Functions

        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        public static extern int SetLayeredWindowAttributes( IntPtr hWnd, int clrKey, Byte bAlpha, int dwFlags );

        [DllImport( "user32.dll" )]
        public static extern int SetWindowLong( IntPtr window, int index, int value );

        [DllImport( "user32.dll" )]
        public static extern int GetWindowLong( IntPtr window, int index );

        [DllImport( "user32.dll" )]
        public static extern IntPtr GetForegroundWindow();

        [DllImport( "user32.dll", CharSet = CharSet.Auto, ExactSpelling = true )]
        public static extern bool SetForegroundWindow( IntPtr window );

        [DllImport( "user32", EntryPoint = "DefWindowProc" )]
        public static extern int DefWindowProc( IntPtr hwnd, int wMsg, int wParam, int lParam );

        [DllImport( "User32" )]
        public static extern bool SendMessage( IntPtr hWnd, int msg, int wParam, int lParam );

        [DllImport( "user32.dll", ExactSpelling = true, SetLastError = true )]
        public static extern Bool UpdateLayeredWindow( IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags );

        [DllImport( "user32.dll", ExactSpelling = true, SetLastError = true )]
        public static extern IntPtr GetDC( IntPtr hWnd );

        [DllImport( "user32.dll", ExactSpelling = true )]
        public static extern int ReleaseDC( IntPtr hWnd, IntPtr hDC );

        [DllImport( "user32.dll", CharSet = CharSet.Auto, ExactSpelling = true )]
        public static extern bool ShowWindow( IntPtr hWnd, int nCmdShow );

        [DllImport( "user32.dll", CharSet = CharSet.Auto, ExactSpelling = true )]
        public static extern bool GetWindowRect( IntPtr hWnd, ref RECT rect );

        [DllImport( "user32.dll", CharSet = CharSet.Auto, ExactSpelling = true )]
        public static extern bool MoveWindow( IntPtr hWnd, int X, int Y, int newWidth, int newHeight, bool repaint );

        #endregion

        #region gdi32 Functions

        [DllImport( "gdi32.dll", EntryPoint = "CreateDIBSection", CharSet = CharSet.Auto, ExactSpelling = true )]
        public static extern IntPtr CreateDIBSection( IntPtr hdc, ref BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset );

        [DllImport( "gdi32.dll", ExactSpelling = true, SetLastError = true )]
        public static extern IntPtr CreateCompatibleDC( IntPtr hDC );

        [DllImport( "gdi32.dll", ExactSpelling = true, SetLastError = true )]
        public static extern Bool DeleteDC( IntPtr hdc );

        [DllImport( "gdi32.dll", ExactSpelling = true )]
        public static extern IntPtr SelectObject( IntPtr hDC, IntPtr hObject );

        [DllImport( "gdi32.dll", ExactSpelling = true, SetLastError = true )]
        public static extern Bool DeleteObject( IntPtr hObject );

        #endregion

        #region Public methods

        /// <summary>
        /// Freezes the control's painting.
        /// </summary>
        /// <param name="control">The control.</param>
        public static void FreezePainting( Control control )
        {
            SendMessage( control.Handle, Win32Constants.WM_SETREDRAW, 0, 0 );
        }

        /// <summary>
        /// Unfreezes the control's painting.
        /// </summary>
        /// <param name="control">The control.</param>
        public static void UnfreezePainting( Control control )
        {
            SendMessage( control.Handle, Win32Constants.WM_SETREDRAW, 1, 0 );
        }

        #endregion

        #region Structures

        [StructLayout( LayoutKind.Sequential, Pack = 2 )]
        public class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }

        [StructLayout( LayoutKind.Sequential )]
        public class BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            public RGBQUAD bmiColors;
        }

        [StructLayout( LayoutKind.Sequential )]
        public struct BITMAPINFO_FLAT
        {
            public int bmiHeader_biSize;
            public int bmiHeader_biWidth;
            public int bmiHeader_biHeight;
            public short bmiHeader_biPlanes;
            public short bmiHeader_biBitCount;
            public int bmiHeader_biCompression;
            public int bmiHeader_biSizeImage;
            public int bmiHeader_biXPelsPerMeter;
            public int bmiHeader_biYPelsPerMeter;
            public int bmiHeader_biClrUsed;
            public int bmiHeader_biClrImportant;
            [MarshalAs( UnmanagedType.ByValArray, SizeConst = 0x400 )]
            public byte[] bmiColors;
        }

        [StructLayout( LayoutKind.Sequential )]
        public class RGBQUAD
        {
            public int rgbBlue;

            public int rgbGreen;

            public int rgbRed;
            public int rgbReserved;
        }

        public enum Bool
        {
            False = 0,
            True
        };

        [StructLayout( LayoutKind.Sequential )]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point( Int32 x, Int32 y ) { this.x = x; this.y = y; }
        }

        [StructLayout( LayoutKind.Sequential )]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size( Int32 cx, Int32 cy ) { this.cx = cx; this.cy = cy; }
        }

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout( LayoutKind.Sequential )]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT( int left, int top, int right, int bottom )
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static RECT Empty = new RECT( 0, 0, 0, 0 );

            public Rectangle Bounds
            {
                get
                {
                    return Rectangle.FromLTRB( left, top, right, bottom );
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Win32Constants -
    ///		Interop layer to required Win32 constants
    /// </summary>
    public class Win32Constants
    {
        public static int LWA_ALPHA = 2;

        public const int GWL_EXSTYLE = -20;

        public const int WM_DESTROY = 0x2;
        public const int WM_SETREDRAW = 0xB;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCACTIVATE = 0x86;
        public const int HTTRANSPARENT = -1;

        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;

        public const int WS_VISIBLE = 0x10000000;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TOPMOST = 8;
        public const int WS_DLGFRAME = 0x400000;
        public const int WS_POPUP = -2147483648;
        public const int WS_EX_TOOLWINDOW = 0x80;

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        public const int SW_SHOWNOACTIVATE = 4;

        public const int BI_RGB = 0;
        public const int DIB_RGB_COLORS = 0;
    }
}
