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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BigMansStuff.Common
{
    /// <summary>
    /// Utility class for blending two images
    /// </summary>
    /// <remarks>
    /// Based on Code Project article: Blending of images, raster operations and basic color adjustments with GDI+
    /// By Konstantin Vasserman
    /// http://www.codeproject.com/cs/media/KVImageProcess.asp
    ///  Thanks Konstantin!
    /// </remarks>
    public class ImageBlender
    {
        public enum BlendOperation : int
        {
            SourceCopy = 1,
            ROP_MergePaint,
            ROP_NOTSourceErase,
            ROP_SourceAND,
            ROP_SourceErase,
            ROP_SourceInvert,
            ROP_SourcePaint,
            Blend_Darken,
            Blend_Multiply,
            Blend_ColorBurn,
            Blend_Lighten,
            Blend_Screen,
            Blend_ColorDodge,
            Blend_Overlay,
            Blend_SoftLight,
            Blend_HardLight,
            Blend_PinLight,
            Blend_Difference,
            Blend_Exclusion,
            Blend_Hue,
            Blend_Saturation,
            Blend_Color,
            Blend_Luminosity,
            Blend_AlphaMask // BigMan
        }

        // NTSC defined color weights
        public const float R_WEIGHT = 0.299f;
        public const float G_WEIGHT = 0.587f;
        public const float B_WEIGHT = 0.114f;

        public const ushort HLSMAX = 360;
        public const byte RGBMAX = 255;
        public const byte HUNDEFINED = 0;

        private delegate byte PerChannelProcessDelegate( ref byte nSrc, ref byte nDst );
        private delegate void RGBProcessDelegate( byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB );

        /// <summary>
        /// BigMan extention - support alpha channel processsing
        /// </summary>
        private delegate void RGBAProcessDelegate( byte sA, byte sR, byte sG, byte sB, ref byte dA, ref byte dR, ref byte dG, ref byte dB );


        // Invert image
        public static void Invert( Image img )
        {
            if ( img == null )
                throw new Exception( "Image must be provided" );

            ColorMatrix cMatrix = new ColorMatrix( new float[][] {
						new float[] {-1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
						new float[] { 0.0f,-1.0f, 0.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f,-1.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
						new float[] { 1.0f, 1.0f, 1.0f, 0.0f, 1.0f }
					} );
            ApplyColorMatrix( ref img, cMatrix );
        }


        // Adjustment values are between -1.0 and 1.0
        public static void AdjustBrightness( Image img, float adjValueR, float adjValueG, float adjValueB )
        {
            if ( img == null )
                throw new Exception( "Image must be provided" );

            ColorMatrix cMatrix = new ColorMatrix( new float[][] {
						new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
						new float[] { adjValueR, adjValueG, adjValueB, 0.0f, 1.0f }
					} );
            ApplyColorMatrix( ref img, cMatrix );
        }


        // Adjustment values are between -1.0 and 1.0
        public static void AdjustBrightness( Image img, float adjValue )
        {
            AdjustBrightness( img, adjValue, adjValue, adjValue );
        }


        // Saturation. 0.0 = desaturate, 1.0 = identity, -1.0 = complementary colors
        public static void AdjustSaturation( Image img, float sat, float rweight, float gweight, float bweight )
        {
            if ( img == null )
                throw new Exception( "Image must be provided" );

            ColorMatrix cMatrix = new ColorMatrix( new float[][] {
						new float[] { (1.0f-sat)*rweight+sat, (1.0f-sat)*rweight, (1.0f-sat)*rweight, 0.0f, 0.0f },
						new float[] { (1.0f-sat)*gweight, (1.0f-sat)*gweight+sat, (1.0f-sat)*gweight, 0.0f, 0.0f },
						new float[] { (1.0f-sat)*bweight, (1.0f-sat)*bweight, (1.0f-sat)*bweight+sat, 0.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
						new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
					} );
            ApplyColorMatrix( ref img, cMatrix );
        }


        // Saturation. 0.0 = desaturate, 1.0 = identity, -1.0 = complementary colors
        public static void AdjustSaturation( Image img, float sat )
        {
            AdjustSaturation( img, sat, R_WEIGHT, G_WEIGHT, B_WEIGHT );
        }


        // Weights between 0.0 and 1.0
        public static void Desaturate( Image img, float RWeight, float GWeight, float BWeight )
        {
            AdjustSaturation( img, 0.0f, RWeight, GWeight, BWeight );
        }


        // Desaturate using "default" NTSC defined color weights
        public static void Desaturate( Image img )
        {
            AdjustSaturation( img, 0.0f, R_WEIGHT, G_WEIGHT, B_WEIGHT );
        }

        /// <summary>
        /// BigMan extention - Adjusts the transparency of a given image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="transparency"></param>
        public static void AdjustTransparency( Image image, float transparency )
        {
            if ( image == null )
                throw new ArgumentNullException( "Image must be provided" );

            ColorMatrix cMatrix = new ColorMatrix( new float[][] {
				new float[] {1, 0, 0, 0, 0},
				new float[] {0, 1, 0, 0, 0},
				new float[] {0, 0, 1, 0, 0},
				new float[] {0, 0, 0, transparency, 0}, 
				new float[] {0, 0, 0, 0, 1} } );

            using ( Graphics gr = Graphics.FromImage( image ) )
            {
                using ( Image tempImage = image.Clone() as Image )
                {
                    gr.Clear( Color.Transparent );
                    using ( ImageAttributes attrs = new ImageAttributes() )
                    {
                        attrs.SetColorMatrix(
                            cMatrix,
                            ColorMatrixFlag.Default,
                            ColorAdjustType.Bitmap );
                        gr.DrawImage(
                            tempImage, new Rectangle( 0, 0, tempImage.Width, tempImage.Height ),
                            0, 0, tempImage.Width, tempImage.Height, GraphicsUnit.Pixel, attrs );
                    }
                }
            }

        }

        /// <summary>
        /// BigMan extention - Helper function that overlays a color on non transparent pixels
        /// </summary>
        public static void OverlaySolidColorOnNonTransparentPixels( Image image, Color color )
        {
            // Overlay solid color on the non transparent pixels
            using ( SolidBrush brush = new SolidBrush( color ) )
            {
                using ( Bitmap colorBitmap = image.Clone() as Bitmap )
                {
                    using ( Graphics tempGraphics = Graphics.FromImage( colorBitmap ) )
                    {
                        tempGraphics.FillRectangle( brush, 0, 0, colorBitmap.Width, colorBitmap.Height );
                    }

                    BlendImages(
                        image,
                        colorBitmap,
                        BlendOperation.Blend_AlphaMask );
                }
            }
        }

        public static void ApplyColorMatrix( ref Image img, ColorMatrix colMatrix )
        {
            Graphics gr = Graphics.FromImage( img );
            ImageAttributes attrs = new ImageAttributes();
            attrs.SetColorMatrix( colMatrix );
            gr.DrawImage( img, new Rectangle( 0, 0, img.Width, img.Height ),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attrs );
            gr.Dispose();
        }

        #region BlendImages functions ...
        /* 
			destImage - image that will be used as background
			destX, destY - define position on destination image where to start applying blend operation
			destWidth, destHeight - width and height of the area to apply blending
			srcImage - image to use as foreground (source of blending)	
			srcX, srcY - starting position of the source image 	  
		*/
        public static void BlendImages( Image destImage, int destX, int destY, int destWidth, int destHeight,
                                Image srcImage, int srcX, int srcY, BlendOperation BlendOp )
        {
            if ( destImage == null )
                throw new Exception( "Destination image must be provided" );

            if ( destImage.Width < destX + destWidth || destImage.Height < destY + destHeight )
                throw new Exception( "Destination image is smaller than requested dimentions" );

            if ( srcImage == null )
                throw new Exception( "Source image must be provided" );

            if ( srcImage.Width < srcX + destWidth || srcImage.Height < srcY + destHeight )
                throw new Exception( "Source image is smaller than requested dimentions" );

            Bitmap tempBmp = null;
            Graphics gr = Graphics.FromImage( destImage );
            gr.CompositingMode = CompositingMode.SourceCopy;

            switch ( BlendOp )
            {
                case BlendOperation.SourceCopy:
                    gr.DrawImage( srcImage, new Rectangle( destX, destY, destWidth, destHeight ),
                            srcX, srcY, destWidth, destHeight, GraphicsUnit.Pixel );
                    break;

                case BlendOperation.ROP_MergePaint:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                                ref srcImage, srcX, srcY, new PerChannelProcessDelegate( MergePaint ) );
                    break;

                case BlendOperation.ROP_NOTSourceErase:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( NOTSourceErase ) );
                    break;

                case BlendOperation.ROP_SourceAND:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( SourceAND ) );
                    break;

                case BlendOperation.ROP_SourceErase:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( SourceErase ) );
                    break;

                case BlendOperation.ROP_SourceInvert:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( SourceInvert ) );
                    break;

                case BlendOperation.ROP_SourcePaint:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( SourcePaint ) );
                    break;

                case BlendOperation.Blend_Darken:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendDarken ) );
                    break;

                case BlendOperation.Blend_Multiply:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendMultiply ) );
                    break;

                case BlendOperation.Blend_Screen:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendScreen ) );
                    break;

                case BlendOperation.Blend_Lighten:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendLighten ) );
                    break;

                case BlendOperation.Blend_HardLight:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendHardLight ) );
                    break;

                case BlendOperation.Blend_Difference:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendDifference ) );
                    break;

                case BlendOperation.Blend_PinLight:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendPinLight ) );
                    break;

                case BlendOperation.Blend_Overlay:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendOverlay ) );
                    break;

                case BlendOperation.Blend_Exclusion:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendExclusion ) );
                    break;

                case BlendOperation.Blend_SoftLight:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendSoftLight ) );
                    break;

                case BlendOperation.Blend_ColorBurn:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendColorBurn ) );
                    break;

                case BlendOperation.Blend_ColorDodge:
                    tempBmp = PerChannelProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new PerChannelProcessDelegate( BlendColorDodge ) );
                    break;

                case BlendOperation.Blend_Hue:
                    tempBmp = RGBProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new RGBProcessDelegate( BlendHue ) );
                    break;

                case BlendOperation.Blend_Saturation:
                    tempBmp = RGBProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new RGBProcessDelegate( BlendSaturation ) );
                    break;

                case BlendOperation.Blend_Color:
                    tempBmp = RGBProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new RGBProcessDelegate( BlendColor ) );
                    break;

                case BlendOperation.Blend_Luminosity:
                    tempBmp = RGBProcess( ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY, new RGBProcessDelegate( BlendLuminosity ) );
                    break;

                case BlendOperation.Blend_AlphaMask:
                    tempBmp = RGBAProcess(
                        ref destImage, destX, destY, destWidth, destHeight,
                        ref srcImage, srcX, srcY,
                        new RGBAProcessDelegate( BlendAlphaMask ) );
                    break;
            }

            if ( tempBmp != null )
            {
                gr.DrawImage( tempBmp, 0, 0, tempBmp.Width, tempBmp.Height );
                tempBmp.Dispose();
                tempBmp = null;
            }

            gr.Dispose();
            gr = null;
        }


        public static void BlendImages( Image destImage, Image srcImage, BlendOperation BlendOp )
        {
            BlendImages( destImage, 0, 0, destImage.Width, destImage.Height, srcImage, 0, 0, BlendOp );
        }

        public static void BlendImages( Image destImage, BlendOperation BlendOp )
        {
            BlendImages( destImage, 0, 0, destImage.Width, destImage.Height, null, 0, 0, BlendOp );
        }

        public static void BlendImages( Image destImage, int destX, int destY, BlendOperation BlendOp )
        {
            BlendImages( destImage, destX, destY, destImage.Width - destX, destImage.Height - destY, null, 0, 0, BlendOp );
        }

        public static void BlendImages( Image destImage, int destX, int destY, int destWidth, int destHeight, BlendOperation BlendOp )
        {
            BlendImages( destImage, destX, destY, destWidth, destHeight, null, 0, 0, BlendOp );
        }

        #endregion

        #region Private Blending Functions ...

        private static Bitmap PerChannelProcess( ref Image destImg, int destX, int destY, int destWidth, int destHeight,
                                ref Image srcImg, int srcX, int srcY,
                                PerChannelProcessDelegate ChannelProcessFunction )
        {
            Bitmap dst = new Bitmap( destImg );
            Bitmap src = new Bitmap( srcImg );

            BitmapData dstBD = dst.LockBits( new Rectangle( destX, destY, destWidth, destHeight ), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb );
            BitmapData srcBD = src.LockBits( new Rectangle( srcX, srcY, destWidth, destHeight ), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb );

            int dstStride = dstBD.Stride;
            int srcStride = srcBD.Stride;

            System.IntPtr dstScan0 = dstBD.Scan0;
            System.IntPtr srcScan0 = srcBD.Scan0;

            unsafe
            {
                byte* pDst = ( byte* ) ( void* ) dstScan0;
                byte* pSrc = ( byte* ) ( void* ) srcScan0;

                for ( int y = 0; y < destHeight; y++ )
                {
                    for ( int x = 0; x < destWidth * 3; x++ )
                    {
                        pDst[ x + y * dstStride ] = ChannelProcessFunction( ref pSrc[ x + y * srcStride ], ref pDst[ x + y * dstStride ] );
                    }
                }
            }

            src.UnlockBits( srcBD );
            dst.UnlockBits( dstBD );

            src.Dispose();

            return dst;
        }

        private static Bitmap RGBProcess( ref Image destImg, int destX, int destY, int destWidth, int destHeight,
            ref Image srcImg, int srcX, int srcY,
            RGBProcessDelegate RGBProcessFunction )
        {
            Bitmap dst = new Bitmap( destImg );
            Bitmap src = new Bitmap( srcImg );

            BitmapData dstBD = dst.LockBits( new Rectangle( destX, destY, destWidth, destHeight ), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb );
            BitmapData srcBD = src.LockBits( new Rectangle( srcX, srcY, destWidth, destHeight ), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb );

            int dstStride = dstBD.Stride;
            int srcStride = srcBD.Stride;

            System.IntPtr dstScan0 = dstBD.Scan0;
            System.IntPtr srcScan0 = srcBD.Scan0;

            unsafe
            {
                byte* pDst = ( byte* ) ( void* ) dstScan0;
                byte* pSrc = ( byte* ) ( void* ) srcScan0;

                for ( int y = 0; y < destHeight; y++ )
                {
                    for ( int x = 0; x < destWidth; x++ )
                    {
                        RGBProcessFunction(
                                pSrc[ x * 3 + 2 + y * srcStride ], pSrc[ x * 3 + 1 + y * srcStride ], pSrc[ x * 3 + y * srcStride ],
                                ref pDst[ x * 3 + 2 + y * dstStride ], ref pDst[ x * 3 + 1 + y * dstStride ], ref pDst[ x * 3 + y * dstStride ]
                            );
                    }
                }
            }

            src.UnlockBits( srcBD );
            dst.UnlockBits( dstBD );

            src.Dispose();

            return dst;
        }


        /// <summary>
        /// BigMan extention - supports alpha processing
        /// </summary>
        /// <param name="destImg"></param>
        /// <param name="destX"></param>
        /// <param name="destY"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        /// <param name="srcImg"></param>
        /// <param name="srcX"></param>
        /// <param name="srcY"></param>
        /// <param name="RGBAProcessFunction"></param>
        /// <returns></returns>
        private static Bitmap RGBAProcess( ref Image destImg, int destX, int destY, int destWidth, int destHeight,
            ref Image srcImg, int srcX, int srcY,
            RGBAProcessDelegate RGBAProcessFunction )
        {
            Bitmap dst = new Bitmap( destImg );
            Bitmap src = new Bitmap( srcImg );

            BitmapData dstBD = dst.LockBits( new Rectangle( destX, destY, destWidth, destHeight ), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb );
            BitmapData srcBD = src.LockBits( new Rectangle( srcX, srcY, destWidth, destHeight ), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb );

            int dstStride = dstBD.Stride;
            int srcStride = srcBD.Stride;

            System.IntPtr dstScan0 = dstBD.Scan0;
            System.IntPtr srcScan0 = srcBD.Scan0;

            unsafe
            {
                byte* pDst = ( byte* ) ( void* ) dstScan0;
                byte* pSrc = ( byte* ) ( void* ) srcScan0;

                for ( int y = 0; y < destHeight; y++ )
                {
                    for ( int x = 0; x < destWidth; x++ )
                    {
                        RGBAProcessFunction(
                            pSrc[ x * 4 + 3 + y * srcStride ], pSrc[ x * 4 + 2 + y * srcStride ], pSrc[ x * 4 + 1 + y * srcStride ], pSrc[ x * 4 + y * srcStride ],
                            ref pDst[ x * 4 + 3 + y * dstStride ], ref pDst[ x * 4 + 2 + y * dstStride ], ref pDst[ x * 4 + 1 + y * dstStride ], ref pDst[ x * 4 + y * dstStride ]
                            );
                    }
                }
            }

            src.UnlockBits( srcBD );
            dst.UnlockBits( dstBD );

            src.Dispose();

            return dst;
        }

        #endregion

        #region HLS Conversion Functions ...

        public static void RGBToHLS( byte R, byte G, byte B, out ushort H, out ushort L, out ushort S )
        {
            byte cMax, cMin;      /* max and min RGB values */
            float Rdelta, Gdelta, Bdelta; /* intermediate value: % of spread from max */

            /* calculate lightness */
            cMax = Math.Max( Math.Max( R, G ), B );
            cMin = Math.Min( Math.Min( R, G ), B );
            L = ( ushort ) ( ( ( ( cMax + cMin ) * HLSMAX ) + RGBMAX ) / ( 2 * RGBMAX ) );

            if ( cMax == cMin )
            {/* r=g=b --> achromatic case */
                S = 0;                     /* saturation */
                H = HUNDEFINED;             /* hue */
            }
            else
            {/* chromatic case */
                /* saturation */
                if ( L <= ( HLSMAX / 2 ) )
                    S = ( ushort ) ( ( ( ( cMax - cMin ) * HLSMAX ) + ( ( cMax + cMin ) / 2 ) ) / ( cMax + cMin ) );
                else
                    S = ( ushort ) ( ( ( ( cMax - cMin ) * HLSMAX ) + ( ( 2 * RGBMAX - cMax - cMin ) / 2 ) ) / ( 2 * RGBMAX - cMax - cMin ) );

                /* hue */
                Rdelta = ( float ) ( ( ( ( cMax - R ) * ( HLSMAX / 6 ) ) + ( ( cMax - cMin ) / 2 ) ) / ( cMax - cMin ) );
                Gdelta = ( float ) ( ( ( ( cMax - G ) * ( HLSMAX / 6 ) ) + ( ( cMax - cMin ) / 2 ) ) / ( cMax - cMin ) );
                Bdelta = ( float ) ( ( ( ( cMax - B ) * ( HLSMAX / 6 ) ) + ( ( cMax - cMin ) / 2 ) ) / ( cMax - cMin ) );

                if ( R == cMax )
                    H = ( ushort ) ( Bdelta - Gdelta );
                else if ( G == cMax )
                    H = ( ushort ) ( ( HLSMAX / 3 ) + Rdelta - Bdelta );
                else /* B == cMax */
                    H = ( ushort ) ( ( ( 2 * HLSMAX ) / 3 ) + Gdelta - Rdelta );

                if ( H < 0 )
                    H += HLSMAX;
                if ( H > HLSMAX )
                    H -= HLSMAX;
            }
        }

        public static void HLSToRGB( ushort H, ushort L, ushort S, out byte R, out byte G, out byte B )
        {
            float Magic1, Magic2;       /* calculated magic numbers (really!) */

            if ( S == 0 )
            {/* achromatic case */
                R = G = B = ( byte ) ( ( L * RGBMAX ) / HLSMAX );
            }
            else
            {/* chromatic case */
                /* set up magic numbers */
                if ( L <= ( HLSMAX / 2 ) )
                    Magic2 = ( float ) ( ( L * ( HLSMAX + S ) + ( HLSMAX / 2 ) ) / HLSMAX );
                else
                    Magic2 = ( float ) ( L + S - ( ( L * S ) + ( HLSMAX / 2 ) ) / HLSMAX );

                Magic1 = ( float ) ( 2 * L - Magic2 );

                /* get RGB, change units from HLSMAX to RGBMAX */
                R = ( byte ) ( ( HueToRGB( Magic1, Magic2, H + ( HLSMAX / 3 ) ) * RGBMAX + ( HLSMAX / 2 ) ) / HLSMAX );
                G = ( byte ) ( ( HueToRGB( Magic1, Magic2, H ) * RGBMAX + ( HLSMAX / 2 ) ) / HLSMAX );
                B = ( byte ) ( ( HueToRGB( Magic1, Magic2, H - ( HLSMAX / 3 ) ) * RGBMAX + ( HLSMAX / 2 ) ) / HLSMAX );
            }
        }

        /* utility routine for HLStoRGB */
        private static float HueToRGB( float n1, float n2, float hue )
        {
            /* range check: note values passed add/subtract thirds of range */
            if ( hue < 0 )
                hue += HLSMAX;

            if ( hue > HLSMAX )
                hue -= HLSMAX;

            /* return r,g, or b value from this tridrant */
            if ( hue < ( HLSMAX / 6 ) )
                return ( float ) ( n1 + ( ( ( n2 - n1 ) * hue + ( HLSMAX / 12 ) ) / ( HLSMAX / 6 ) ) );
            if ( hue < ( HLSMAX / 2 ) )
                return ( float ) ( n2 );
            if ( hue < ( ( HLSMAX * 2 ) / 3 ) )
                return ( float ) ( n1 + ( ( ( n2 - n1 ) * ( ( ( HLSMAX * 2 ) / 3 ) - hue ) + ( HLSMAX / 12 ) ) / ( HLSMAX / 6 ) ) );
            else
                return ( float ) ( n1 );
        }
        #endregion

        #region Raster Operation Functions ...

        // (NOT Source) OR Destination
        private static byte MergePaint( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( ( 255 - Src ) | Dst, 255 ), 0 );
        }

        // NOT (Source OR Destination)
        private static byte NOTSourceErase( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( 255 - ( Src | Dst ), 255 ), 0 );
        }

        // Source AND Destination
        private static byte SourceAND( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( Src & Dst, 255 ), 0 );
        }

        // Source AND (NOT Destination)
        private static byte SourceErase( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( Src & ( 255 - Dst ), 255 ), 0 );
        }

        // Source XOR Destination
        private static byte SourceInvert( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( Src ^ Dst, 255 ), 0 );
        }

        // Source OR Destination
        private static byte SourcePaint( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( Src | Dst, 255 ), 0 );
        }

        #endregion

        #region Blend Pixels Functions ...
        // Choose darkest color 
        private static byte BlendDarken( ref byte Src, ref byte Dst )
        {
            return ( ( Src < Dst ) ? Src : Dst );
        }

        // Multiply
        private static byte BlendMultiply( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( ( Src / 255.0f * Dst / 255.0f ) * 255.0f, 255 ), 0 );
        }

        // Screen
        private static byte BlendScreen( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( 255 - ( ( 255 - Src ) / 255.0f * ( 255 - Dst ) / 255.0f ) * 255.0f, 255 ), 0 );
        }

        // Choose lightest color 
        private static byte BlendLighten( ref byte Src, ref byte Dst )
        {
            return ( ( Src > Dst ) ? Src : Dst );
        }

        // hard light 
        private static byte BlendHardLight( ref byte Src, ref byte Dst )
        {
            return ( ( Src < 128 ) ? ( byte ) Math.Max( Math.Min( ( Src / 255.0f * Dst / 255.0f ) * 255.0f * 2, 255 ), 0 ) : ( byte ) Math.Max( Math.Min( 255 - ( ( 255 - Src ) / 255.0f * ( 255 - Dst ) / 255.0f ) * 255.0f * 2, 255 ), 0 ) );
        }

        // difference 
        private static byte BlendDifference( ref byte Src, ref byte Dst )
        {
            return ( byte ) ( ( Src > Dst ) ? Src - Dst : Dst - Src );
        }

        // pin light 
        private static byte BlendPinLight( ref byte Src, ref byte Dst )
        {
            return ( Src < 128 ) ? ( ( Dst > Src ) ? Src : Dst ) : ( ( Dst < Src ) ? Src : Dst );
        }

        // overlay 
        private static byte BlendOverlay( ref byte Src, ref byte Dst )
        {
            return ( ( Dst < 128 ) ? ( byte ) Math.Max( Math.Min( ( Src / 255.0f * Dst / 255.0f ) * 255.0f * 2, 255 ), 0 ) : ( byte ) Math.Max( Math.Min( 255 - ( ( 255 - Src ) / 255.0f * ( 255 - Dst ) / 255.0f ) * 255.0f * 2, 255 ), 0 ) );
        }

        // exclusion 
        private static byte BlendExclusion( ref byte Src, ref byte Dst )
        {
            return ( byte ) ( Src + Dst - 2 * ( Dst * Src ) / 255f );
        }

        // Soft Light (XFader formula)  
        private static byte BlendSoftLight( ref byte Src, ref byte Dst )
        {
            return ( byte ) Math.Max( Math.Min( ( Dst * Src / 255f ) + Dst * ( 255 - ( ( 255 - Dst ) * ( 255 - Src ) / 255f ) - ( Dst * Src / 255f ) ) / 255f, 255 ), 0 );
        }

        // Color Burn 
        private static byte BlendColorBurn( ref byte Src, ref byte Dst )
        {
            return ( Src == 0 ) ? ( byte ) 0 : ( byte ) Math.Max( Math.Min( 255 - ( ( ( 255 - Dst ) * 255 ) / Src ), 255 ), 0 );
        }

        // Color Dodge 
        private static byte BlendColorDodge( ref byte Src, ref byte Dst )
        {
            return ( Src == 255 ) ? ( byte ) 255 : ( byte ) Math.Max( Math.Min( ( Dst * 255 ) / ( 255 - Src ), 255 ), 0 );
        }

        // use source Hue
        private static void BlendHue( byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB )
        {
            ushort sH, sL, sS, dH, dL, dS;
            RGBToHLS( sR, sG, sB, out sH, out sL, out sS );
            RGBToHLS( dR, dG, dB, out dH, out dL, out dS );
            HLSToRGB( sH, dL, dS, out dR, out dG, out dB );
        }

        // use source Saturation
        private static void BlendSaturation( byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB )
        {
            ushort sH, sL, sS, dH, dL, dS;
            RGBToHLS( sR, sG, sB, out sH, out sL, out sS );
            RGBToHLS( dR, dG, dB, out dH, out dL, out dS );
            HLSToRGB( dH, dL, sS, out dR, out dG, out dB );
        }

        // use source Color
        private static void BlendColor( byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB )
        {
            ushort sH, sL, sS, dH, dL, dS;
            RGBToHLS( sR, sG, sB, out sH, out sL, out sS );
            RGBToHLS( dR, dG, dB, out dH, out dL, out dS );
            HLSToRGB( sH, dL, sS, out dR, out dG, out dB );
        }

        // use source Luminosity
        private static void BlendLuminosity( byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB )
        {
            ushort sH, sL, sS, dH, dL, dS;
            RGBToHLS( sR, sG, sB, out sH, out sL, out sS );
            RGBToHLS( dR, dG, dB, out dH, out dL, out dS );
            HLSToRGB( dH, sL, dS, out dR, out dG, out dB );
        }

        // Copy source over destination using alpha channel as a mask
        private static void BlendAlphaMask(
            byte sA, byte sR, byte sG, byte sB,
            ref byte dA, ref byte dR, ref byte dG, ref byte dB )
        {
            dR = sR;
            dG = sG;
            dB = sB;

            dA = ( byte ) ( sA * dA / 255 );
        }
        #endregion
    }
}
