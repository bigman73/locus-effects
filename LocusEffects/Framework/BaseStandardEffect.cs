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
using System.Windows.Forms ;
using System.Reflection ;

using BigMansStuff.Common ;

namespace BigMansStuff.LocusEffects
{
	/// <summary>
	/// BaseStandardEffect -
	///		Base class for standard effects that have a lead in, animation and lead out sequence
	/// </summary>
	public abstract class BaseStandardEffect: BaseLocusEffect
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		protected BaseStandardEffect()
		{
			m_animationStartColor = DefaultAnimationStartColor ; 
			m_animationEndColor = DefaultAnimationEndColor ; 
		}

		
		#endregion

		#region Public Methods

		/// <summary>
		/// Provies standard animation sequence logic - lead in, body, lead out
		/// </summary>
		protected override void AnimateEffect()
		{
			IntPtr activeWindowHandle = Win32NativeMethods.GetForegroundWindow() ;

			// Set initial bitmap
			m_animationStep = 0 ;
			using ( Graphics g = Graphics.FromImage( m_runTimeData.EffectBitmap ) )
			{
				PaintLeadIn( g ) ;
			}
            this.CalculateMovementOffset() ;

			// Move form out of visible area before showing it -
			//  We don't want the last bitmap
			m_owner.EffectWindow.ShowOutOfScreen() ;

			m_owner.EffectWindow.ApplyGraphics() ;
		
			Win32NativeMethods.SetForegroundWindow( activeWindowHandle ) ;
			
			m_startTime = DateTime.Now ;
		
			// Heart of a standard effect: (Lead in, body, lead out sequence)

			// 1. Lead in
			if ( m_leadInTime > 0 )
			{
				this.LeadIn() ;
			}

			// 2. Then, do some animation (body)
			if ( m_animationTime > 0 )
			{
				this.AnimateBody() ;
			}

			// 3. Finally, lead out
			if ( m_leadOutTime > 0 )
			{
				this.LeadOut() ;	
			}
		}


		#endregion

		#region Public Properties

		/// <summary>
		/// Start color of animation (at start of lead in)
		/// </summary>
		public Color AnimationStartColor
		{
			get
			{
				return m_animationStartColor ;
			}
			set
			{
				if ( m_animationStartColor == value )
					return ;
				m_animationStartColor = value ;
			}
		}

		/// <summary>
		/// End color of animation (at end of body)
		/// </summary>
		public Color AnimationEndColor
		{
			get
			{
				return m_animationEndColor ;
			}
			set
			{
				if ( m_animationEndColor == value )
					return ;
				m_animationEndColor = value ;
			}
		}

		/// <summary>
		/// Duration of lead in part of the animation sequence
		/// </summary>
		public int LeadInTime
		{
			get
			{
				return m_leadInTime ;
			}
			set
			{
				if ( m_leadInTime == value )
					return ;
				m_leadInTime = value ;
			}
		}

		/// <summary>
		/// Duration of body part of the animation sequence
		/// </summary>
		public int AnimationTime
		{
			get
			{
				return m_animationTime ;
			}
			set
			{
				if ( m_animationTime == value )
					return ;
				m_animationTime = value ;
			}
		}

		/// <summary>
		/// Duration of lead out part of the animation sequence
		/// </summary>
		public int LeadOutTime
		{
			get
			{
				return m_leadOutTime ;
			}
			set
			{
				if ( m_leadOutTime == value )
					return ;
				m_leadOutTime = value ;
			}
		}
  
		#endregion

		#region Protected methods

		/// <summary>
		/// Perform lead in steps
		/// </summary>
		protected virtual void LeadIn()
		{
            TimeSpan duration ;

            DateTime stepStartTime ;
            
            while ( m_runTimeData != null && !m_runTimeData.StopRequested )
			{
                stepStartTime = DateTime.Now ;
                
                this.OnLeadInStep() ;

				using ( Graphics g = Graphics.FromImage( m_runTimeData.EffectBitmap ) )
				{
					PaintLeadIn( g ) ;
				}

                #region Diagnostics
                CommonData.WriteDiagnosticsLine( "LeadIn - PaintLeadIn took: " + ( DateTime.Now - stepStartTime ).TotalMilliseconds ) ;
                #endregion

				m_owner.EffectWindow.ApplyGraphics() ;

				duration = ( DateTime.Now - m_startTime ) ;
				if ( duration.TotalMilliseconds >= m_leadInTime )
				{
					break ;
				}
				else
				{
                    TimeSpan stepDuration = ( DateTime.Now - stepStartTime ) ;

                    // Allow the processor to rest - we don't want the animation thread to work all the time
                    if ( stepDuration.TotalMilliseconds < m_runTimeData.StepMaxDuration )
                    {
                        System.Threading.Thread.Sleep( ( int ) ( m_runTimeData.StepMaxDuration - stepDuration.TotalMilliseconds ) ) ;
                    }
                }			
			}
		}

		/// <summary>
		/// Perform body steps
		/// </summary>
        protected virtual void AnimateBody()
        {
            m_startAnimationTime = DateTime.Now ;

            TimeSpan duration ;

            DateTime stepStartTime ;
            while ( m_runTimeData != null && !m_runTimeData.StopRequested )
            {
                stepStartTime = DateTime.Now ;
                
                this.OnAnimationBodyStep() ;

                using ( Graphics g = Graphics.FromImage( m_runTimeData.EffectBitmap ) )
                {
                    PaintBodyAnimation( g ) ;
                }

                #region Diagnostics
                CommonData.WriteDiagnosticsLine( "LeadIn - PaintBodyAnimation took: " + ( DateTime.Now - stepStartTime ).TotalMilliseconds ) ;
                #endregion

                m_owner.EffectWindow.ApplyGraphics() ;

                duration = ( DateTime.Now - m_startAnimationTime ) ;
                if ( duration.TotalMilliseconds >= m_animationTime )
                {
                    // Force draw of last position
                    m_animationStep = 100.0f ;
                    using ( Graphics g = Graphics.FromImage( m_runTimeData.EffectBitmap ) )
                    {
                        PaintBodyAnimation( g ) ;
                    }

                    m_owner.EffectWindow.ApplyGraphics() ;

                    break ;
                }
                else
                {
                    TimeSpan stepDuration = ( DateTime.Now - stepStartTime ) ;

                    // Allow the processor to rest - we don't want the animation thread to work all the time
                    if ( stepDuration.TotalMilliseconds < m_runTimeData.StepMaxDuration )
                    {
                        System.Threading.Thread.Sleep( ( int ) ( m_runTimeData.StepMaxDuration - stepDuration.TotalMilliseconds ) ) ;
                    }
                }
            }
        }

		/// <summary>
		/// Perform lead out steps
		/// </summary>
		protected virtual void LeadOut()
		{
			m_startLeadOutTime = DateTime.Now ;

			TimeSpan duration ;

            DateTime stepStartTime ;
            
            while ( m_runTimeData != null && !m_runTimeData.StopRequested )
			{
                stepStartTime = DateTime.Now ;
                
                this.OnLeadOutStep() ;

				using ( Graphics g = Graphics.FromImage( m_runTimeData.EffectBitmap ) )
				{
					PaintLeadOut( g ) ;
				}

                #region Diagnostics
                CommonData.WriteDiagnosticsLine( "LeadIn - PaintLeadOut took: " + ( DateTime.Now - stepStartTime ).TotalMilliseconds ) ;
                #endregion

				m_owner.EffectWindow.ApplyGraphics() ;
				
				duration = ( DateTime.Now - m_startLeadOutTime ) ;
				if ( duration.TotalMilliseconds >= m_leadOutTime )
				{
					break ;
				}
				else
				{
                    TimeSpan stepDuration = ( DateTime.Now - stepStartTime ) ;

                    // Allow the processor to rest - we don't want the animation thread to work all the time
                    if ( stepDuration.TotalMilliseconds < m_runTimeData.StepMaxDuration )
                    {
                        System.Threading.Thread.Sleep( ( int ) ( m_runTimeData.StepMaxDuration - stepDuration.TotalMilliseconds ) ) ;
                    }				
                }		
			}
		}
	
		/// <summary>
		/// Calculate lead in animation step
		/// </summary>
		protected virtual void OnLeadInStep()
		{
			TimeSpan leadInDuration ;
			leadInDuration = ( DateTime.Now - m_startTime ) ;
			m_animationStep = ( float ) ( leadInDuration.TotalMilliseconds / m_leadInTime * 100 ) ;
            
			if ( m_animationStep >= 100 )
			{
				m_animationStep = 100 ;
			}

            #region Diagnostics
            CommonData.WriteDiagnosticsLine( "LeadIn - AnimationStep: " + m_animationStep + "%, Duration: " + leadInDuration.TotalMilliseconds ) ;
            #endregion
        }

		/// <summary>
		/// Calculate body animation step
		/// </summary>
		protected virtual void OnAnimationBodyStep()
		{
			TimeSpan animationDuration ;
			animationDuration = ( DateTime.Now - m_startAnimationTime ) ;
			m_animationStep = ( float ) ( animationDuration.TotalMilliseconds / m_animationTime * 100 ) ;
			if ( m_animationStep >= 100 )
			{
				m_animationStep = 100 ;
			}

            #region Diagnostics
            CommonData.WriteDiagnosticsLine( "AnimationBody - AnimationStep: " + m_animationStep + "%, Duration: " + animationDuration.TotalMilliseconds ) ;
            #endregion
        }

		/// <summary>
		/// Calculate lead out animation step
		/// </summary>
		protected virtual void OnLeadOutStep()
		{
			TimeSpan leadOutDuration ;
			leadOutDuration = ( DateTime.Now - m_startLeadOutTime ) ;
			m_animationStep = ( float ) ( leadOutDuration.TotalMilliseconds / m_leadOutTime * 100 ) ;
			if ( m_animationStep >= 100 )
			{
				m_animationStep = 100 ;
			}

            #region Diagnostics
            CommonData.WriteDiagnosticsLine( "LeadOut - AnimationStep: " + m_animationStep + "%, Duration: " + leadOutDuration.TotalMilliseconds ) ;
            #endregion
		}

		/// <summary>
		/// Paint animation for lead in step
		/// </summary>
		/// <param name="graphics"></param>
		protected virtual void PaintLeadIn( Graphics graphics )
		{
			#region Diagnostics
			CommonData.WriteDiagnosticsLine( "PaintLeadIn" ) ;
			#endregion

			Color animationColor = 
				Color.FromArgb( 
				m_animationStartColor.R + ( int ) ( m_animationStep / 100 * ( m_animationEndColor.R - m_animationStartColor.R  ) ),
				m_animationStartColor.G + ( int ) ( m_animationStep / 100 * ( m_animationEndColor.G - m_animationStartColor.G  ) ),
				m_animationStartColor.B + ( int ) ( m_animationStep / 100 * ( m_animationEndColor.B - m_animationStartColor.B  ) ) ) ;

			ImageBlender.OverlaySolidColorOnNonTransparentPixels( m_runTimeData.EffectBitmap, animationColor ) ;
		}

		/// <summary>
		/// Paint animation for body step
		/// </summary>
		/// <param name="graphics"></param>
		protected virtual void PaintBodyAnimation( Graphics graphics )
		{
			#region Diagnostics
            CommonData.WriteDiagnosticsLine( "PaintBodyAnimation" ) ;
			#endregion

			ImageBlender.OverlaySolidColorOnNonTransparentPixels( m_runTimeData.EffectBitmap, m_animationEndColor ) ;

            this.CalculateMovementOffset() ;
		}

        /// <summary>
		/// Paint animation for lead out step
		/// </summary>
		/// <param name="graphics"></param>
		protected virtual void PaintLeadOut( Graphics graphics )
		{
			#region Diagnostics
            CommonData.WriteDiagnosticsLine( "PaintLeadOut" ) ;
			#endregion

			Color animationColor = 
				Color.FromArgb( 
				m_animationEndColor.R + ( int ) ( m_animationStep / 100 * ( m_animationStartColor.R - m_animationEndColor.R  ) ),
				m_animationEndColor.G + ( int ) ( m_animationStep / 100 * ( m_animationStartColor.G - m_animationEndColor.G  ) ),
				m_animationEndColor.B + ( int ) ( m_animationStep / 100 * ( m_animationStartColor.B - m_animationEndColor.B  ) ) ) ;

			ImageBlender.OverlaySolidColorOnNonTransparentPixels( m_runTimeData.EffectBitmap, animationColor ) ;
		}

        /// <summary>
        /// Calculates the movement offset using movement mode rules
        /// </summary>
        protected virtual void CalculateMovementOffset()
        {
            // TODO: Strategy design pattern

            // Handle bouncing movement along a vector
            if ( m_movementMode == MovementMode.BounceAlongVector )
            {
                m_runTimeData.MovementOffset.X = ( int ) ( 
                    ( m_movementAmplitude * Math.Abs( Math.Sin( m_movementCycles * 
                    ( m_animationStep / 100 ) * Math.PI ) ) * 
                    Math.Cos( m_movementVectorAngle / 180 * Math.PI ) ) ) ;

                m_runTimeData.MovementOffset.Y = ( int ) ( 
                    ( m_movementAmplitude * Math.Abs( Math.Sin( m_movementCycles *
                    ( m_animationStep / 100 ) * Math.PI ) ) * 
                    -Math.Sin( m_movementVectorAngle / 180 * Math.PI ) ) ) ;
            }
            // Handle one way movement along a vector
            if ( m_movementMode == MovementMode.OneWayAlongVector )
            {
                m_runTimeData.MovementOffset.X = ( int ) ( 
                    ( m_movementAmplitude * Math.Abs( 
                        Math.Sin( Math.Pow( ( ( 100 -  m_animationStep ) / 100 ), 2 ) * Math.PI / 2 ) ) * 
                    Math.Cos( m_movementVectorAngle / 180 * Math.PI ) ) ) ;

                m_runTimeData.MovementOffset.Y = ( int ) ( 
                    ( m_movementAmplitude * Math.Abs( 
                        Math.Sin( Math.Pow( ( ( 100 - m_animationStep ) / 100 ), 2 ) * Math.PI / 2 ) ) * 
                    -Math.Sin( m_movementVectorAngle / 180 * Math.PI ) ) ) ;
            }
            // Handle one way movement along a path
            else if ( m_movementMode == MovementMode.OneWayAlongPath )
            {
                // TODO: Implement
                m_runTimeData.MovementOffset = Point.Empty ;
            }
            // Handle buzz movement 
            else if ( m_movementMode == MovementMode.Buzz )
            {
                Random random = new Random() ;
                float randomAmplitudeX = ( float ) random.Next( ( int ) m_movementAmplitude ) ;
                float randomAmplitudeY = ( float ) random.Next( ( int ) m_movementAmplitude ) ;
                float randomAngleX = ( float ) random.Next( 360 ) ;
                float randomAngleY = ( float ) random.Next( 360 ) ;

                m_runTimeData.MovementOffset.X = ( int ) ( 
                    ( randomAmplitudeX * Math.Sin( randomAngleX / 180 * Math.PI ) ) ) ;

                m_runTimeData.MovementOffset.Y = ( int ) ( 
                    ( randomAmplitudeY * Math.Sin( randomAngleY / 180 * Math.PI ) ) ) ;
            }
            else
            {
                m_runTimeData.MovementOffset = Point.Empty ;
            }

            #region Diagnostics
            CommonData.WriteDiagnosticsLine( "CalculateMovementOffset - " +
                m_runTimeData.MovementOffset ) ;
            #endregion
        }

		#endregion

		#region Protected Members

		protected int m_leadInTime = 300 ; // msec
		protected int m_animationTime = 400 ; // msec
		protected int m_leadOutTime = 250 ; // msec

		protected Color m_animationStartColor ;
		protected Color m_animationEndColor ;

		protected float m_animationStep = 0 ; // %

		protected DateTime m_startTime ;
		protected DateTime m_startAnimationTime ;
		protected DateTime m_startLeadOutTime ;

		protected int m_transparencyOnLeadOutStep = 5 ;

		#endregion
		
		#region Constants

		private static Color DefaultAnimationStartColor = Color.FromArgb( 128, 192, 241 ) ;
		private static Color DefaultAnimationEndColor = Color.FromArgb( 52, 99, 135 ) ;

		#endregion
	}
}
