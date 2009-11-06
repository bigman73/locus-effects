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
using System.Runtime.CompilerServices;
using System.Threading;
using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// BaseLocusEffect - 
    ///		Base class for all locus effect classes
    ///		
    ///	Contains common interface &amp; core functionality needed for a locus effect class
    /// </summary>
    public abstract class BaseLocusEffect : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseLocusEffect()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Name of effect - must be unique
        /// </summary>
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// Opacity to start with when effect starts
        /// </summary>
        public int InitialOpacity
        {
            get
            {
                return m_initialOpacity;
            }
        }

        /// <summary>
        /// Type of anchoring
        /// </summary>
        public AnchoringMode AnchoringMode
        {
            get
            {
                return m_anchoringMode;
            }
            set
            {
                m_anchoringMode = value;
            }
        }

        /// <summary>
        /// Anchoring offset - in pixels
        /// </summary>
        public Point AnchoringOffset
        {
            get
            {
                return m_anchoringOffset;
            }
            set
            {
                m_anchoringOffset = value;
            }
        }

        /// <summary>
        /// Readonly flag for querying the effect's animation state
        /// </summary>
        public bool IsAnimating
        {
            get
            {
                bool isAnimating;
                lock ( this )
                {
                    isAnimating = ( m_runTimeData != null ) &&
                                  ( m_runTimeData.IsAnimating );
                }

                return isAnimating;
            }
        }


        /// <summary>
        /// Rendering bitmap used by effect in runtimeData
        /// </summary>
        public Bitmap EffectBitmap
        {
            get
            {
                return m_runTimeData.EffectBitmap;
            }
        }

        /// <summary>
        /// Flag for allowing body fade out - fade out on body animation
        /// </summary>
        public bool BodyFadeOut
        {
            get
            {
                return m_bodyFadeOut;
            }
            set
            {
                if ( m_bodyFadeOut == value )
                {
                    return;
                }

                m_bodyFadeOut = value;
            }
        }

        /// <summary>
        /// Run time data used by the effect when animating
        /// </summary>
        public EffectRuntimeData RunTimeData
        {
            get
            {
                return m_runTimeData;
            }
        }

        /// <summary>
        /// Flag for controlling shadow visibility
        /// </summary>
        public bool ShowShadow
        {
            get
            {
                return m_showShadow;
            }
            set
            {
                if ( m_showShadow == value )
                {
                    return;
                }

                m_showShadow = value;
            }
        }


        /// <summary>
        /// Opacity (Transparency) of shadow
        /// </summary>
        public int ShadowOpacity
        {
            get
            {
                return m_shadowOpacity;
            }
            set
            {
                if ( m_shadowOpacity == value )
                {
                    return;
                }

                m_shadowOpacity = value;
            }
        }

        /// <summary>
        /// Offset of shadow from main figure
        /// </summary>
        public Point ShadowOffset
        {
            get
            {
                return m_shadowOffset;
            }
            set
            {
                if ( m_shadowOffset == value )
                {
                    return;
                }

                m_shadowOffset = value;
            }
        }

        /// <summary>
        /// Color of shadow
        /// </summary>
        public Color ShadowColor
        {
            get
            {
                return m_shadowColor;
            }
            set
            {
                if ( m_shadowColor == value )
                    return;
                m_shadowColor = value;
            }
        }

        /// <summary>
        /// Type of movement to perform
        /// </summary>
        public MovementMode MovementMode
        {
            get
            {
                return m_movementMode;
            }
            set
            {
                if ( m_movementMode == value )
                {
                    return;
                }

                m_movementMode = value;
            }
        }


        /// <summary>
        /// Direction of movement vector - in degrees
        /// </summary>
        public float MovementVectorAngle
        {
            get
            {
                return m_movementVectorAngle;
            }
            set
            {
                if ( m_movementVectorAngle == value )
                {
                    return;
                }

                m_movementVectorAngle = value;
            }
        }

        /// <summary>
        /// Amount of times the effect moves - Each cycle is back and forth
        /// </summary>
        public float MovementCycles
        {
            get
            {
                return m_movementCycles;
            }
            set
            {
                if ( m_movementCycles == value )
                {
                    return;
                }

                m_movementCycles = value;
            }
        }

        /// <summary>
        /// Amplitude of movement - how far the effect moves in one cycle
        /// </summary>
        public float MovementAmplitude
        {
            get
            {
                return m_movementAmplitude;
            }
            set
            {
                if ( m_movementAmplitude == value )
                {
                    return;
                }

                m_movementAmplitude = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starting showing the effect
        /// </summary>
        /// <param name="activatorForm"></param>
        /// <param name="locusScreenBounds"></param>
        public virtual void ShowEffect( Form activatorForm, Rectangle locusScreenBounds )
        {
            lock ( this )
            {
                m_runTimeData = new EffectRuntimeData();
                this.SetInitialRunTimeData();

                m_runTimeData.ActivatorForm = activatorForm;
                m_runTimeData.LocusScreenBounds = locusScreenBounds;

                // Create and start animation thread
                m_runTimeData.AnimationThread = new System.Threading.Thread( new System.Threading.ThreadStart( DoAnimation ) );
                m_runTimeData.AnimationThread.IsBackground = true;
                m_runTimeData.AnimationThread.Priority = ThreadPriority.AboveNormal;
                m_runTimeData.AnimationThread.Name = "LoucsEffects_ShowEffect_Thread";
                m_runTimeData.AnimationThread.Start();
            }
        }

        /// <summary>
        /// Stop showing the effect and hide it
        /// </summary>
        public virtual void StopEffect()
        {
            // Nothing to stop since nothing is running
            if ( m_runTimeData == null )
            {
                return;
            }

            EffectRuntimeData runTimeData = m_runTimeData;

            lock ( this )
            {
                #region Diagnostics
                CommonData.WriteDiagnosticsLine( "StopEffect - StopRequested" );
                #endregion

                runTimeData.StopRequested = true;

                if ( m_owner.EffectWindow != null )
                {
                    m_owner.EffectWindow.Hide();
                }
            }

            if ( runTimeData.AnimationThread != null )
            {
                // Let thread die gracefully
                runTimeData.AnimationThread.Join( MaxAnimationThreadJoin );

                lock ( this )
                {
                    // If thread is still alive, kill it
                    if ( runTimeData != null &&
                         runTimeData.AnimationThread != null &&
                         runTimeData.AnimationThread.IsAlive )
                    {
                        #region Diagnostics
                        CommonData.WriteDiagnosticsLine( "StopEffect - Abort animation thread" );
                        #endregion

                        runTimeData.AnimationThread.Abort();
                        runTimeData = null;
                    }
                }
            }
        }


        #endregion

        #region Public events

        public event System.EventHandler EffectFinished;

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        #endregion

        #region Internal-Protected Properties

        /// <summary>
        /// Gets the locus screen bounds.
        /// </summary>
        /// <value>The locus screen bounds.</value>
        internal protected Rectangle LocusScreenBounds
        {
            get
            {
                return m_runTimeData.LocusScreenBounds;
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        internal LocusEffectsProvider Owner
        {
            get
            {
                return m_owner;
            }
            set
            {
                m_owner = value;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Animates the effect.
        /// </summary>
        protected abstract void AnimateEffect();

        /// <summary>
        /// Sets the initial run time data.
        /// </summary>
        protected virtual void SetInitialRunTimeData()
        {
        }

        /// <summary>
        /// Cleans up the effect.
        /// </summary>
        protected virtual void CleanUpEffect()
        {
            // Clean up
            lock ( this )
            {
                m_runTimeData.IsAnimating = false;
                if ( m_runTimeData.EffectBitmap != null )
                {
                    m_runTimeData.EffectBitmap.Dispose();
                    m_runTimeData.EffectBitmap = null;
                }
                this.UnsubscribeActivatorFormEvents();

                if ( m_owner.EffectWindow != null )
                {
                    #region Diagnostics
                    CommonData.WriteDiagnosticsLine( "End of animation - Hide effect form" );
                    #endregion

                    m_owner.EffectWindow.SetEffect( null );

                    m_owner.EffectWindow.Hide();
                }

                m_runTimeData.AnimationThread = null;
            }
        }

        /// <summary>
        /// Subscribes to the activator form events.
        /// </summary>
        private void SubscribeActivatorFormEvents()
        {
            m_runTimeData.ActivatorForm.LocationChanged += new EventHandler( activatorForm_LocationChanged );
            m_runTimeData.ActivatorForm.Closed += new EventHandler( activatorForm_Closed );
            m_runTimeData.ActivatorForm.VisibleChanged += new EventHandler( activatorForm_VisibleChanged );
            m_runTimeData.ActivatorForm.Deactivate += new EventHandler( ActivatorForm_Deactivate );
        }

        /// <summary>
        /// Unsubscribes from the activator form events.
        /// </summary>
        private void UnsubscribeActivatorFormEvents()
        {
            m_runTimeData.ActivatorForm.LocationChanged -= new EventHandler( activatorForm_LocationChanged );
            m_runTimeData.ActivatorForm.Closed -= new EventHandler( activatorForm_Closed );
            m_runTimeData.ActivatorForm.VisibleChanged -= new EventHandler( activatorForm_VisibleChanged );
            m_runTimeData.ActivatorForm.Deactivate -= new EventHandler( ActivatorForm_Deactivate );
        }

        /// <summary>
        /// Thread method -
        ///		Performs the animation sequence
        /// </summary>
        protected virtual void DoAnimation()
        {
            try
            {
                m_owner.EffectWindow.SetEffect( this );

                m_runTimeData.LastActivatorFormBounds = m_runTimeData.ActivatorForm.Bounds;
                m_runTimeData.StepMaxDuration = 1000.0f / m_owner.FramesPerSecond;

                this.SubscribeActivatorFormEvents();

                m_runTimeData.IsAnimating = true;
                try
                {
                    // Do the actual animation of the effect
                    this.AnimateEffect();
                }
                finally
                {
                    this.CleanUpEffect();
                }

                // Fire event - EffectFinished
                if ( EffectFinished != null )
                {
                    EffectFinished( this, System.EventArgs.Empty );
                }

                m_runTimeData = null;
            }
            catch ( ThreadAbortException )
            {
            }
        }

        #endregion

        #region Protected Members

        protected string m_name;
        protected int m_initialOpacity = 70; // %
        protected AnchoringMode m_anchoringMode = AnchoringMode.AutoCorner;
        protected Point m_anchoringOffset = Point.Empty;
        protected const int SleepInterval = 1; // msec
        protected bool m_bodyFadeOut = false;
        protected bool m_showShadow = true;
        protected int m_shadowOpacity = 20; // %
        protected Point m_shadowOffset = new Point( 5, 2 );
        protected Color m_shadowColor = Color.Black;
        protected LocusEffectsProvider m_owner = null;
        protected float m_movementVectorAngle = 45;
        protected MovementMode m_movementMode = MovementMode.None;
        protected float m_movementCycles = 3;
        protected float m_movementAmplitude = 30;

        // Runtime data
        protected EffectRuntimeData m_runTimeData = null;

        #endregion

        #region Private Event Handlers

        /// <summary>
        /// LocationChanged event handler for activator form -
        ///		Makes sure the effect form tracks the location of the activator form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void activatorForm_LocationChanged( object sender, EventArgs e )
        {
            if ( m_runTimeData == null ||
                 !m_runTimeData.IsAnimating ||
                 m_owner.EffectWindow == null )
            {
                return;
            }

            // Calculate delta location
            Point deltaLocation =
                new Point( m_runTimeData.ActivatorForm.Left - m_runTimeData.LastActivatorFormBounds.Left,
                m_runTimeData.ActivatorForm.Top - m_runTimeData.LastActivatorFormBounds.Top );
            // Update last activator form bounds
            m_runTimeData.LastActivatorFormBounds = m_runTimeData.ActivatorForm.Bounds;

            // Recalc (offset) locus screen bounds
            m_runTimeData.LocusScreenBounds.Offset( deltaLocation );
        }

        /// <summary>
        /// Handles the Closed event of the activatorForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void activatorForm_Closed( object sender, EventArgs e )
        {
            this.StopEffect();
        }

        /// <summary>
        /// Handles the VisibleChanged event of the activatorForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void activatorForm_VisibleChanged( object sender, EventArgs e )
        {
            if ( !m_runTimeData.ActivatorForm.Visible )
            {
                this.StopEffect();
            }
        }

        /// <summary>
        /// Handles the Deactivate event of the ActivatorForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ActivatorForm_Deactivate( object sender, EventArgs e )
        {
            this.StopEffect();
        }


        #endregion

        #region Constants

        private const int MaxAnimationThreadJoin = 2000; // msec

        #endregion
    }
}
