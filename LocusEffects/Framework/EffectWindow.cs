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

using System ;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging ;
using System.Collections ;
using System.ComponentModel ;
using System.Windows.Forms ;
using System.Reflection ;
using BigMansStuff.Common ;

namespace BigMansStuff.LocusEffects
{
    #region Public Enumerations
    public enum AnchoringMode { AutoCorner, Center, CenterOffset, CenterMonitor } ;
    public enum AnchoringCorner { NW, N, NE, W, Center, E, SW, S, SE } ;

    /// <summary>
    /// Effect movement mode
    /// </summary>
    public enum MovementMode 
    { 
        /// <summary>
        /// No movement
        /// </summary>
        None, 
        /// <summary>
        /// Bounce back and forth along a given vector angle
        /// </summary>
        BounceAlongVector, 
        /// <summary>
        /// Move once from a given distance in a given vector angle, to the locus point
        /// </summary>
        OneWayAlongVector,
        /// <summary>
        /// Move from point A to locus point along a given path
        /// </summary>
        OneWayAlongPath,
        /// <summary>
        /// Buzz randomly
        /// </summary>
        Buzz, 
        /// <summary>
        /// Custom movement
        /// </summary>
        Custom 
    } ;
    #endregion

	/// <summary>
	/// EffectWindow -
	///		Layered window for presenting transparent Locus effects
	/// </summary>
	public class EffectWindow : LayeredWindow
	{
		#region Dispose

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public override void Dispose()
		{
			this.DisposeResources() ;

			base.Dispose() ;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Initialize the effect window
		/// </summary>
		public void Initialize()
		{
			base.Create() ;

			this.ShowOutOfScreen() ;
		}

		/// <summary>
		/// Assign the active effect
		/// </summary>
		/// <param name="effect"></param>
		public void SetEffect( BaseLocusEffect effect )
		{
			m_effect = effect ;

			if ( effect == null )
			{
                // Effect is finished, dispose resources
				this.DisposeResources() ;
			}
			else
			{
                // Effect has started , create buffer bitmaps
                this.CreateBufferBitmaps() ;
			}
		}

	
		/// <summary>
		/// Apply graphics to effect window
		/// </summary>
        public virtual void ApplyGraphics()
        {
            using ( Bitmap finalBitmap = new Bitmap( m_effect.EffectBitmap ) )
            {
                    
                // Calculate bounds of effect bitmap
                this.CalculateBitmapBounds( finalBitmap, m_effect.LocusScreenBounds ) ;

                // Handle anchoring logic of effect bitmap
                this.HandleEffectBitmapAnchoring( finalBitmap ) ;

                Size backBitmapSize = finalBitmap.Size ;

                // Extend back bitmap size so it has space for the shadow
                if ( m_effect.ShowShadow )
                {
                    backBitmapSize.Width += Math.Abs( m_effect.ShadowOffset.X ) ;
                    backBitmapSize.Height += Math.Abs( m_effect.ShadowOffset.Y ) ;
                }

                bool bitmapsRecreated = false ;

                // Re-create back bitmap size
                if ( backBitmapSize != m_backBitmapSize )
                {
                    this.RecreateBitmaps( backBitmapSize ) ;

                    bitmapsRecreated = true ;
                }

                // Note: FromHdc is very important as it hooks to the DIB Section (Back bitmap)
                //   in the memory DC directly, without creating an intermediate buffer
                // Thank you Lou Amadio for helping out here!

                // Draw effect bitmap and its shadow on back bitmap
                using ( Graphics g = Graphics.FromHdc( m_memDC ) )
                {
                    g.SmoothingMode = SmoothingMode.HighQuality ;

                    // Clear the back bitmap
                    if ( !bitmapsRecreated )
                    {
                        g.Clear( Color.Transparent ) ;
                    }

                    // Draw shadow before final bitmap is drawn
                    this.DrawShadow( finalBitmap, g ) ;

                    // Draw final effect bitmap
                    g.DrawImage( finalBitmap, 
                        0, 
                        0,
                        m_bitmapSize.Width, 
                        m_bitmapSize.Height ) ;
                }

                // Render bitmap to window 
                this.SetWindowBitmap( m_effect.RunTimeData.Opacity ) ;
            }
        }
      
		#endregion

		#region Internal Methods

        /// <summary>
        /// Hides the window but moving it to a place totally out of bounds
        /// This is used instead of Hide() so the window is actually visible from Windows point of view
        ///   and this reduces flickers
        /// </summary>
		internal void ShowOutOfScreen()
		{
			base.Bounds = new Rectangle( -10000, -10000, 10, 10 ) ;
			this.Show() ;
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Changes the current bitmap with a custom opacity level
		/// </summary>
		/// <param name="opacity">0..100% (in percents)</param>
		/// <remarks>
		/// Based on Code Project article: Per Pixel Alpha Blend in C# by Rui Godinho Lopes.
		/// http://www.codeproject.com/cs/media/perpxalpha_sharp.asp
		///  Thanks Rui!
		/// </remarks>
        protected virtual void SetWindowBitmap( int opacity ) 
        {
            #region Diagnostics
            DateTime measureTime = DateTime.Now ;
            #endregion
      
            // Calculate coordinates
            Win32NativeMethods.Point pointMemory = new Win32NativeMethods.Point( 0, 0 ) ;
            Win32NativeMethods.Point pointScreen = new Win32NativeMethods.Point( m_bitmapLocation.X, m_bitmapLocation.Y ) ;
            Win32NativeMethods.Size size = new Win32NativeMethods.Size( m_backBitmapSize.Width, m_backBitmapSize.Height ) ;

            // Create blending function
            Win32NativeMethods.BLENDFUNCTION blend = new Win32NativeMethods.BLENDFUNCTION() ;
            blend.BlendOp             = Win32Constants.AC_SRC_OVER  ;
            blend.BlendFlags          = 0 ;
            blend.SourceConstantAlpha = ( byte ) ( opacity * 255 / 100 ) ;
            blend.AlphaFormat         = Win32Constants.AC_SRC_ALPHA ;

            // Render the layered window, by supplying it a memory DC which contains updated LocusEffect bitmap
            Win32NativeMethods.UpdateLayeredWindow( 
                this.Handle, 
                m_screenDC, ref pointScreen, ref size, 
                m_memDC, ref pointMemory, 0, 
                ref blend, 
                Win32Constants.ULW_ALPHA ) ;

            #region Diagnostics
            CommonData.WriteDiagnosticsLine( "SetWindowBitmap took " + ( DateTime.Now - measureTime ).TotalMilliseconds ) ;
            #endregion
        }
	
		/// <summary>
		/// Calculate bounds of the effect bitmap, using AnchoringMode
		/// </summary>
		/// <param name="effectBitmap"></param>
		/// <param name="locusScreenBounds"></param>
		protected virtual void CalculateBitmapBounds( Bitmap effectBitmap, Rectangle locusScreenBounds )
		{
			Point newLocation = this.Bounds.Location ;
			Size newSize = effectBitmap.Size ;
    
			m_effect.RunTimeData.AnchoringCorner = AnchoringCorner.Center ;

			if ( m_effect.AnchoringMode == AnchoringMode.AutoCorner )
			{
				// Determine anchoring corner - NE, NW, SE, SW

				bool south = true ;
				bool west = true ;
    
				// North
				if ( locusScreenBounds.Top - newSize.Height <= SystemInformation.VirtualScreen.Top )
				{
					newLocation.Y = locusScreenBounds.Bottom ;
    				
					south = false ;
				}
					// South
				else
				{
					newLocation.Y = locusScreenBounds.Top - newSize.Height  ;
				}
    
				// East
				if ( locusScreenBounds.Right + newSize.Width >= SystemInformation.VirtualScreen.Right )
				{
					newLocation.X = locusScreenBounds.Left + locusScreenBounds.Width / 2 - newSize.Width ;
    
					west = false ;
				}
					// West
				else
				{
					newLocation.X = locusScreenBounds.Left + locusScreenBounds.Width / 2  ;
				}

				if ( south && west )
					m_effect.RunTimeData.AnchoringCorner = AnchoringCorner.SW ;
				else  if ( south && !west )
					m_effect.RunTimeData.AnchoringCorner = AnchoringCorner.SE ;
				else  if ( !south && west )
					m_effect.RunTimeData.AnchoringCorner = AnchoringCorner.NW ;
				else  if ( !south && !west )
					m_effect.RunTimeData.AnchoringCorner = AnchoringCorner.NE ;
			}
			else if ( m_effect.AnchoringMode == AnchoringMode.Center )
			{
				newLocation.X = locusScreenBounds.Left + locusScreenBounds.Width / 2 - newSize.Width / 2 ;
				newLocation.Y = locusScreenBounds.Top + locusScreenBounds.Height / 2 - newSize.Height / 2 ;
			}
			else if ( m_effect.AnchoringMode == AnchoringMode.CenterOffset )
			{
				newLocation.X = locusScreenBounds.Left + locusScreenBounds.Width / 2 - newSize.Width / 2 + m_effect.AnchoringOffset.X ;
				newLocation.Y = locusScreenBounds.Top + locusScreenBounds.Height / 2 - newSize.Height / 2 + m_effect.AnchoringOffset.Y ;
			}
            else if ( m_effect.AnchoringMode == AnchoringMode.CenterMonitor )
            {
                // Get monitor from point
                Screen screen = Screen.FromPoint( new Point( 
                    locusScreenBounds.Left + locusScreenBounds.Width / 2,
                    locusScreenBounds.Top + locusScreenBounds.Height / 2 ) ) ;

                // Calculate monitor center
                newLocation.X = screen.WorkingArea.Left + ( screen.WorkingArea.Width - newSize.Width ) / 2 ;
                newLocation.Y = screen.WorkingArea.Top +( screen.WorkingArea.Height - newSize.Height ) / 2 ;
            }
            else
			{
				// Should never get here
				throw new NotImplementedException( string.Format( "Anchoring mode {0} is not implemented", m_effect.AnchoringMode.ToString() ) ) ;
			}

            // Apply the offset in location needed to create a movement
            this.ApplyMovementOffset( ref newLocation ) ;

			// Keep current bitmap location & size
			m_lastBitmapLocation = m_bitmapLocation ;
			m_lastBitmapSize = m_bitmapSize ;

			// Set new bitmap location & size
			m_bitmapLocation = newLocation ;
			m_bitmapSize = newSize ;
		}

        /// <summary>
        /// Applies the movement offset to the bitmap location
        /// </summary>
        /// <param name="bitmapLocation"></param>
        protected virtual void ApplyMovementOffset( ref Point bitmapLocation )
        {
            if ( m_effect.RunTimeData.MovementOffset != Point.Empty )
            {
                bool movementApplied = false ;
                if ( m_effect.AnchoringMode == AnchoringMode.AutoCorner )
                {
                    if ( m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.SE )
                    {
                        bitmapLocation.Offset(
                            -m_effect.RunTimeData.MovementOffset.X,
                            m_effect.RunTimeData.MovementOffset.Y ) ;
                        movementApplied = true ;
                    }
                    else if ( m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.NE )
                    {
                        bitmapLocation.Offset(
                            -m_effect.RunTimeData.MovementOffset.X,
                            -m_effect.RunTimeData.MovementOffset.Y ) ;
                        movementApplied = true ;
                    }
                    else if ( m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.NW )
                    {
                        bitmapLocation.Offset(
                            m_effect.RunTimeData.MovementOffset.X,
                            -m_effect.RunTimeData.MovementOffset.Y ) ;
                        movementApplied = true ;
                    }
                }

                // Default movement offset
                if ( !movementApplied )
                {
                    bitmapLocation.Offset(
                        m_effect.RunTimeData.MovementOffset.X,
                        m_effect.RunTimeData.MovementOffset.Y ) ;
                }
            }
        }

        /// <summary>
        /// Draws a shadow bitmap that will be based on the shape of the final bitmap and
        ///   displayed behind it in an offset &amp; color
        /// </summary>
        /// <param name="finalBitmap"></param>
        /// <param name="g"></param>
        protected void DrawShadow( Bitmap finalBitmap, Graphics g )
        {
            // Draw shadow
            if ( m_effect.ShowShadow )
            {
                using ( Bitmap shadowBitmap = new Bitmap( finalBitmap ) )
                {
                    // Create the shadow image
                    //     Case the shadow color
                    ImageBlender.OverlaySolidColorOnNonTransparentPixels( shadowBitmap, m_effect.ShadowColor ) ;
                    //     Make the shadow transparent
                    ImageBlender.AdjustTransparency( shadowBitmap, m_effect.ShadowOpacity / 100.0f ) ;
              
                    // Draw the shadow image
                    g.DrawImage(
                        shadowBitmap,
                        m_effect.ShadowOffset.X,
                        m_effect.ShadowOffset.Y,
                        m_bitmapSize.Width,
                        m_bitmapSize.Height ) ;
                }
            }
        }

        /// <summary>
        /// Handles the anchoring of the effect bitmap
        /// </summary>
        /// <param name="effectBitmap"></param>
        protected void HandleEffectBitmapAnchoring( Bitmap effectBitmap )
        {
            // Flip bitmap in auto corner anchoring mode - if needed
            if ( m_effect.AnchoringMode == AnchoringMode.AutoCorner )
            {
                if ( m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.NW ||
                    m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.N ||
                    m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.NE )
                {
                    effectBitmap.RotateFlip( RotateFlipType.Rotate180FlipX ) ;
                }

                if ( m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.NE ||
                    m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.E ||
                    m_effect.RunTimeData.AnchoringCorner == AnchoringCorner.SE )
                {
                    effectBitmap.RotateFlip( RotateFlipType.Rotate180FlipY ) ;
                }
            }
        }

        /// <summary>
		/// Creates the buffer bitmaps
		/// </summary>
		protected void CreateBufferBitmaps()
		{
			// Create a new back bitmap
			if ( m_hBackBitmap == IntPtr.Zero )
			{
                // Create and keep screen and memory device contexts
                m_screenDC = Win32NativeMethods.GetDC( IntPtr.Zero ) ;
                m_memDC = Win32NativeMethods.CreateCompatibleDC( m_screenDC ) ;

                // Create initial DIB section
                this.CreateDIBSection( new Size( 1, 1 ) ) ;
			}
		}


        /// <summary>
        /// Recreate the buffer bitmap in the new size
        /// </summary>
        /// <param name="bufferBitmapSize"></param>
        protected void RecreateBitmaps( Size bufferBitmapSize )
        {
            // Dispose old DIBSection
            this.DisposeDIBSection() ;

            // Create DIB section in new size
            this.CreateDIBSection( bufferBitmapSize ) ;

            #region Diagnostics
            CommonData.WriteDiagnosticsLine( "RecreateBitmaps: " + bufferBitmapSize.ToString() ) ;
            #endregion
        }

        /// <summary>
        /// Disposes the resources - buffer bitmaps, clears members
        /// </summary>
        protected void DisposeResources()
        {
            // Dispose back bitmap
            if ( m_hBackBitmap != IntPtr.Zero )
            {          
                // Dispose the GDI DIB Section back buffer bitmap
                this.DisposeDIBSection() ;

                // Release screen device context
                Win32NativeMethods.ReleaseDC( IntPtr.Zero, m_screenDC ) ;
                m_screenDC = IntPtr.Zero ;

                // Delete memory device context
                Win32NativeMethods.DeleteDC( m_memDC ) ;
                m_memDC = IntPtr.Zero ;

                // Reset members
                m_bitmapLocation = Point.Empty ;
                m_bitmapSize = Size.Empty ;
                m_lastBitmapLocation = Point.Empty ;
                m_lastBitmapSize = Size.Empty ;
            }
        }

        /// <summary>
        /// Creates the DIB Section bitmap
        /// </summary>
        protected void CreateDIBSection( Size bitmapSize )
        {
            Win32NativeMethods.BITMAPINFO_FLAT bitmapInfo =
                new Win32NativeMethods.BITMAPINFO_FLAT() ;
            bitmapInfo.bmiHeader_biSize =
                System.Runtime.InteropServices.Marshal.SizeOf(
                typeof( Win32NativeMethods.BITMAPINFOHEADER ) ) ;
            bitmapInfo.bmiHeader_biWidth = bitmapSize.Width ;
            bitmapInfo.bmiHeader_biHeight = bitmapSize.Height ;
            bitmapInfo.bmiHeader_biPlanes = 1 ;
            bitmapInfo.bmiHeader_biBitCount = 32 ;
            bitmapInfo.bmiHeader_biCompression = Win32Constants.BI_RGB ;
            bitmapInfo.bmiHeader_biSizeImage =
                bitmapInfo.bmiHeader_biWidth *
                bitmapInfo.bmiHeader_biHeight * 4 ; // 4 bytes per pixel
            // Create a DIB Section, m_pDIBRawBits will hold a direct reference to the DIB's memory
            m_hBackBitmap = Win32NativeMethods.CreateDIBSection(
                m_memDC,
                ref bitmapInfo,
                Win32Constants.DIB_RGB_COLORS,
                ref m_pDIBRawBits,
                IntPtr.Zero,
                0 ) ;

            m_backBitmapSize = bitmapSize ;

            // Select DIB Section into memory DC
            m_oldBitmap = Win32NativeMethods.SelectObject( m_memDC, m_hBackBitmap ) ;
        }

        /// <summary>
        /// Disposes the DIB Section
        /// </summary>
        protected void DisposeDIBSection()
        {
            // Delete DIB Section
            if ( m_hBackBitmap != IntPtr.Zero )
            {
                // Select old bitmap back into memory device context
                Win32NativeMethods.SelectObject( m_memDC, m_oldBitmap ) ;

                // Delete the DIB Section
                Win32NativeMethods.DeleteObject( m_hBackBitmap ) ;
                m_hBackBitmap = IntPtr.Zero ;
                m_pDIBRawBits = IntPtr.Zero ;

                m_oldBitmap = IntPtr.Zero ;
            }

        }

		#endregion

		#region Protected Members

		protected BaseLocusEffect m_effect = null ;

        protected IntPtr m_hBackBitmap = IntPtr.Zero ;
        protected Size m_backBitmapSize = Size.Empty ;
        protected IntPtr m_pDIBRawBits = IntPtr.Zero ;
        protected IntPtr m_oldBitmap = IntPtr.Zero ;

		protected Point m_bitmapLocation = Point.Empty ;
		protected Size m_bitmapSize = Size.Empty ;
		protected Point m_lastBitmapLocation = Point.Empty ;
		protected Size m_lastBitmapSize = Size.Empty ;

        protected IntPtr m_screenDC = IntPtr.Zero ;
        protected IntPtr m_memDC = IntPtr.Zero ;


		#endregion
	}
}
