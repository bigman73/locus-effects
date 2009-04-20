using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using BigMansStuff.LocusEffects;
using Tao.OpenGl;
using Tao.Platform.Windows;
using System.Runtime.InteropServices;
using BigMansStuff.Common;
using System.Windows.Forms;

namespace BigMansStuff.LocusEffects.OpenGL
{
    /// <summary>
    /// Custom OpenGL Locus Effect - 
    /// Base Class for all OpenGL LocusEffects
    /// </summary>
    /// <remarks>
    /// Make sure Tao DLLs are gac'ed with GACUtil (or using the .NET Framework 2.0 configuration console),
    ///   otherwise .NET fails in runtime with "Could not load file or assembly ..." exception!
    /// </remarks>
    /// <see cref="http://sourceforge.net/projects/taoframework/"/>
    public class OpenGLLocusEffect : BaseStandardEffect
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpenGLLocusEffect"/> class.
        /// </summary>
        public OpenGLLocusEffect()
        {
            m_anchoringMode = AnchoringMode.Center;
            m_showShadow = true;
            m_animationStartColor = Color.Transparent;
            m_animationEndColor = Color.Transparent;

            m_leadInTime = 0;
            m_animationTime = 1000;
            m_leadOutTime = 1000;
        }

        #endregion

        #region OpenGL Rendering

        /// <summary>
        /// Inits the GL scene.
        /// </summary>
        protected virtual void InitGLScene()
        {
            IntPtr pBitmapBits = IntPtr.Zero;
         
            // Set the pixel format
            Gdi.PIXELFORMATDESCRIPTOR pfd = new Gdi.PIXELFORMATDESCRIPTOR();
            pfd.nSize = ( short ) Marshal.SizeOf( pfd );
            pfd.nVersion = 1;
            pfd.dwFlags = Gdi.PFD_SUPPORT_OPENGL | Gdi.PFD_DRAW_TO_BITMAP;
            pfd.iPixelType = Gdi.PFD_TYPE_RGBA;
            pfd.cColorBits = 32;
            pfd.cDepthBits = 16;
            pfd.iLayerType = Gdi.PFD_MAIN_PLANE;
            pfd.cAlphaBits = 8;

            // Set the bitmap info
            BigMansStuff.Common.Win32NativeMethods.BITMAPINFO_FLAT bmi = new Win32NativeMethods.BITMAPINFO_FLAT();
            bmi.bmiHeader_biSize = Marshal.SizeOf( typeof( Win32NativeMethods.BITMAPINFOHEADER ) );
            bmi.bmiHeader_biPlanes = 1;
            bmi.bmiHeader_biBitCount = 32;
            bmi.bmiHeader_biCompression = Win32Constants.BI_RGB;
            bmi.bmiHeader_biWidth = Width;
            bmi.bmiHeader_biHeight = Height;
            bmi.bmiHeader_biSizeImage = Width * Height * 4;

            // Create a memory DC for OpenGL background drawing
            m_hMemDC = Win32NativeMethods.CreateCompatibleDC( IntPtr.Zero );

            // Create a DIB section, to be used with m_hMemDC
            m_hBmp = Win32NativeMethods.CreateDIBSection( m_hMemDC, ref bmi, Win32Constants.DIB_RGB_COLORS, ref pBitmapBits, IntPtr.Zero, 0 );
            int lastError = Marshal.GetLastWin32Error();
            m_hOldBmp = Gdi.SelectObject( m_hMemDC, m_hBmp );
            
            int iPixelFormat = Gdi.ChoosePixelFormat( m_hMemDC, ref pfd );
            Gdi.SetPixelFormat( m_hMemDC, iPixelFormat, ref pfd );

            // Create an OpneGL context
            m_hMemRC = Wgl.wglCreateContext( m_hMemDC );
            Wgl.wglMakeCurrent( m_hMemDC, m_hMemRC );

            Gl.glShadeModel( Gl.GL_SMOOTH ); // Enable Smooth Shading

            m_transparentColor = Color.FromArgb(
            ( int ) Math.Round( m_transparentR * 255 ),
            ( int ) Math.Round( m_transparentG * 255 ),
            ( int ) Math.Round( m_transparentB * 255 ) );

            Gl.glClearColor(
                m_transparentR, 
                m_transparentG, 
                m_transparentB, 
                0.0f );  // Clear Background
            Gl.glClearDepth( 1 );                                                 // Depth Buffer Setup
            Gl.glEnable( Gl.GL_DEPTH_TEST );                                      // Enables Depth Testing
            Gl.glDepthFunc( Gl.GL_LEQUAL );                                       // The Type Of Depth Testing To Do
            Gl.glHint( Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST );         // Really Nice Perspective Calculations

            Gl.glViewport( 0, 0, Width, Height );                                 // Reset The Current Viewport

            Gl.glMatrixMode( Gl.GL_PROJECTION );                                  // Select The Projection Matrix
            Gl.glLoadIdentity();                                                // Reset The Projection Matrix

            Glu.gluPerspective( 45, Width / ( double ) Height, 0.1, 100 );          // Calculate The Aspect Ratio Of The Window

        }

        /// <summary>
        /// Renders the frame using OpenGL.
        /// </summary>
        protected void RenderFrame()
        {
            // Dispose old bitmap
            if ( m_runTimeData.EffectBitmap != null )
            {
                m_runTimeData.EffectBitmap.Dispose();
                m_runTimeData.EffectBitmap = null;
            }

            // Render the object
            RenderOpenGLScene();

            Gl.glFlush();

            if ( m_runTimeData.EffectBitmap == null )
            {
                //Bitmap bitmap = new Bitmap( Width, Height );
                Bitmap bitmap = Bitmap.FromHbitmap( m_hBmp );

                bitmap.MakeTransparent( m_transparentColor );

                m_runTimeData.EffectBitmap = bitmap;
            }
        }

        /// <summary>
        /// Renders the open GL scene.
        /// </summary>
        public virtual void RenderOpenGLScene()
        {
            Gl.glMatrixMode( Gl.GL_MODELVIEW );                                   // Select The Modelview Matrix
            Gl.glLoadIdentity();                                                // Reset The Modelview Matrix

            Gl.glClear( Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT );	// Clear The Screen And The Depth Buffer
            Gl.glLoadIdentity();					// Reset The View
        }

        #endregion

        #region Animation

        /// <summary>
        /// Thread method -
        /// Performs the animation sequence
        /// </summary>
        protected override void DoAnimation()
        {
            InitGLScene();


            base.DoAnimation();
        }
        
        /// <summary>
        /// Provies standard animation sequence logic - lead in, body, lead out
        /// </summary>
        protected override void AnimateEffect()
        {
            RenderFrame();

            base.AnimateEffect();
        }

        /// <summary>
        /// Paint animation for lead in step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintLeadIn( Graphics graphics )
        {
            RenderFrame();
        }

        /// <summary>
        /// Paint animation for body step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintBodyAnimation( Graphics graphics )
        {
            RenderFrame();
        }

        /// <summary>
        /// Paint animation for lead out step
        /// </summary>
        /// <param name="graphics"></param>
        protected override void PaintLeadOut( Graphics graphics )
        {
            RenderFrame();
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Cleans up the effect.
        /// </summary>
        protected override void CleanUpEffect()
        {
            base.CleanUpEffect();
           
            // Clean up resources
            if ( m_hMemDC != IntPtr.Zero )
            {
                Gdi.SelectObject( m_hMemDC, m_hOldBmp );
                Wgl.wglMakeCurrent( IntPtr.Zero, IntPtr.Zero );
                Wgl.wglDeleteContext( m_hMemRC );
                Gdi.DeleteObject( m_hMemDC );
                Gdi.DeleteObject( m_hBmp );

                m_hBmp = IntPtr.Zero;
                m_hMemDC = IntPtr.Zero;
                m_hMemRC = IntPtr.Zero;
                m_hOldBmp = IntPtr.Zero;
 
            }
        }

        #endregion

        #region Constants

        protected int Width = 100;
        protected int Height = 100;

        #endregion

        #region Private Members

        private IntPtr m_hBmp;
        private IntPtr m_hMemDC;
        private IntPtr m_hOldBmp;
        private IntPtr m_hMemRC;

        protected float m_transparentR = 0.2f;
        protected float m_transparentG = 0.3f;
        protected float m_transparentB = 0.4f;
        protected Color m_transparentColor;

        #endregion
    }
}
