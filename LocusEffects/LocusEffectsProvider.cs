#region � Copyright 2005, 2009 Yuval Naveh, Locus Effects. LGPL.
/* Locus Effects
 
    � Copyright 2005, 2009, Yuval Naveh.
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
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using BigMansStuff.Common;

namespace BigMansStuff.LocusEffects
{
    /// <summary>
    /// LocusEffectsProvider -
    ///		Provider of Locus Effects.
    ///		Manages the repository of locus effects and provides a Facade for controlling them.
    /// </summary>
    [ToolboxBitmap( typeof( LocusEffectsProvider ), "LocusEffectsProvider.bmp" )]
    public class LocusEffectsProvider : System.ComponentModel.Component
    {
        #region Constructors

        /// <summary>
        /// Default constructor - needed for VS.NET IDE
        /// </summary>
        public LocusEffectsProvider()
        {
            m_registeredEffects = new Dictionary<string, BaseLocusEffect>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocusEffectsProvider"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public LocusEffectsProvider( System.ComponentModel.IContainer container )
            : this()
        {
            // Required for Windows.Forms Class Composition Designer support
            container.Add( this );
            InitializeComponent();
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        #endregion
        
        #region Initialization

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            if ( m_initialized )
            {
                throw new ApplicationException( "LocusEffectsProvider already initialized" );
            }

            m_initialized = true;

            // Add default (built-in) locus effects
            ArrowLocusEffect defaultLocusEffectArrow = new ArrowLocusEffect();
            defaultLocusEffectArrow.Name = DefaultLocusEffectArrow;
            InternalAddLocusEffect( defaultLocusEffectArrow );

            BeaconLocusEffect defaultLocusEffectBeacon = new BeaconLocusEffect();
            defaultLocusEffectBeacon.Name = DefaultLocusEffectBeacon;
            InternalAddLocusEffect( defaultLocusEffectBeacon );

            BitmapLocusEffect defaultLocusEffectBitmap = new BitmapLocusEffect();
            defaultLocusEffectBitmap.Name = DefaultLocusEffectBitmap;
            InternalAddLocusEffect( defaultLocusEffectBitmap );

            TextLocusEffect defaultLocusEffectText = new TextLocusEffect();
            defaultLocusEffectText.Name = DefaultLocusEffectText;
            InternalAddLocusEffect( defaultLocusEffectText );

            AnimatedImageLocusEffect defaultLocusEffectAnimatedImage = new AnimatedImageLocusEffect();
            defaultLocusEffectAnimatedImage.Name = DefaultLocusEffectAnimatedImage;
            InternalAddLocusEffect( defaultLocusEffectAnimatedImage );

            EffectWindow effectWindow = new EffectWindow();
            effectWindow.Initialize();
            m_effectWindow = effectWindow;
        }

        #endregion

        #region Dispose
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/>
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                #region Diagnostics

                CommonData.WriteDiagnosticsLine( "LocusEffectsProvider.Dispose" );

                #endregion

                // Stop the currently active effect
                if ( m_activeEffect != null )
                {
                    this.InternalStopActiveLocusEffect();
                }

                // Dispose the effect window
                if ( m_effectWindow != null )
                {
                    m_effectWindow.Dispose();
                    m_effectWindow = null;
                }

                // Dispose all registered effects
                foreach ( BaseLocusEffect effect in m_registeredEffects.Values )
                {
                    effect.Dispose();
                }
                m_registeredEffects.Clear();
            }

            base.Dispose( disposing );
        }

        #endregion

        #region Public constants

        public static readonly string DefaultLocusEffectArrow = "DefaultArrow";
        public static readonly string DefaultLocusEffectBeacon = "DefaultBeacon";
        public static readonly string DefaultLocusEffectBitmap = "DefaultBitmap";
        public static readonly string DefaultLocusEffectText = "DefaultText";
        public static readonly string DefaultLocusEffectAnimatedImage = "DefaultAnimatedImage";

        #endregion

        #region Properties

        /// <summary>
        /// Frames per second
        /// </summary>
        [Category( "Behavior" )]
        [Description( "Frames per second - controls animation quality" )]
        [DefaultValue( 25 )]
        public int FramesPerSecond
        {
            get
            {
                return m_framesPerSecond;
            }
            set
            {
                m_framesPerSecond = value;
            }
        }

        /// <summary>
        /// Predicate - Is the component initialized
        /// </summary>
        [Browsable( false )]
        public bool IsInitialized
        {
            get
            {
                return m_initialized;
            }
        }

        /// <summary>
        /// Predicate - Is a LocusEffect currently animating
        /// </summary>
        [Browsable( false )]
        public bool IsAnimating
        {
            get
            {
                return
                    m_initialized &&
                    m_activeEffect != null &&
                    m_activeEffect.IsAnimating;
            }
        }

        #endregion 

        #region Public methods - Locus operations Facade

        /// <summary>
        /// Show a locus effect for a given locus effect instance
        /// </summary>
        /// <param name="activatorForm"></param>
        /// <param name="locusScreenBounds"></param>
        /// <param name="locusEffect"></param>
        public void ShowLocusEffect( Form activatorForm, Rectangle locusScreenBounds, BaseLocusEffect locusEffect )
        {
            if ( locusEffect == null )
            {
                throw new ArgumentNullException( "locusEffect" );
            }

            CheckInitialized();

            InternalShowLocusEffect( activatorForm, locusScreenBounds, locusEffect.Name );
        }

        /// <summary>
        /// Show a locus effect for a given locus effect instance
        /// </summary>
        /// <param name="activatorForm"></param>
        /// <param name="locusScreenPoint"></param>
        /// <param name="locusEffect"></param>
        public void ShowLocusEffect( Form activatorForm, Point locusScreenPoint, BaseLocusEffect locusEffect )
        {
            if ( locusEffect == null )
            {
                throw new ArgumentNullException( "locusEffect" );
            }

            CheckInitialized();

            InternalShowLocusEffect( activatorForm, new Rectangle( locusScreenPoint, new Size( 1, 1 ) ), locusEffect.Name );
        }

        /// <summary>
        /// Show a locus effect for a given locus effect name
        /// </summary>
        /// <param name="activatorForm"></param>
        /// <param name="locusScreenBounds"></param>
        /// <param name="locusEffectName"></param>
        public void ShowLocusEffect( Form activatorForm, Rectangle locusScreenBounds, string locusEffectName )
        {
            if ( locusEffectName == null )
            {
                throw new ArgumentNullException( "locusEffectName" );
            }

            CheckInitialized();

            InternalShowLocusEffect( activatorForm, locusScreenBounds, locusEffectName );
        }

        /// <summary>
        /// Show a locus effect for a given locus effect name &amp; screen point
        /// </summary>
        /// <param name="activatorForm"></param>
        /// <param name="locusScreenPoint"></param>
        /// <param name="locusEffectName"></param>
        public void ShowLocusEffect( Form activatorForm, Point locusScreenPoint, string locusEffectName )
        {
            if ( locusEffectName == null )
            {
                throw new ArgumentNullException( "locusEffectName" );
            }

            CheckInitialized();

            InternalShowLocusEffect( activatorForm, new Rectangle( locusScreenPoint, new Size( 1, 1 ) ), locusEffectName );
        }

        /// <summary>
        /// Show a certain locus effect on a control
        /// </summary>
        /// <param name="locusControl">Control to show locus effect on</param>
        /// <param name="locusEffectName"></param>
        /// <remarks>Helper function - it is redundant, and exists for ease-of-use</remarks>
        public void ShowLocusEffect(Control locusControl, string locusEffectName)
        {
            if ( locusControl == null )
            {
                throw new ArgumentNullException("locusControl");
            }

            Form activatorForm = locusControl.FindForm();
            if ( activatorForm == null )
            {
                throw new ArgumentException( "locusControl does not have a parent form", "locusControl" );
            }


            // Calculate the locus bounds in screen coordinates
            Rectangle controlRect = locusControl.Parent.RectangleToScreen( locusControl.Bounds );
            Rectangle locusRect = new Rectangle(controlRect.X + controlRect.Width / 2, controlRect.Y + controlRect.Height / 2, 1, 1);

            // Show the locus effect on the control 
            this.ShowLocusEffect( activatorForm, locusRect, locusEffectName );
        }




        /// <summary>
        /// Stops the currently active Locus effect
        /// </summary>
        public void StopActiveLocusEffect()
        {
            CheckInitialized();

            InternalStopActiveLocusEffect();
        }

        /// <summary>
        /// Add and register a new locus effect instance 
        /// </summary>
        /// <param name="locusEffect"></param>
        public void AddLocusEffect( BaseLocusEffect locusEffect )
        {
            CheckInitialized();

            InternalAddLocusEffect( locusEffect );
        }

        /// <summary>
        /// Remove and unregister an existing locus effect instance
        /// </summary>
        /// <param name="locusEffect"></param>
        public void RemoveLocusEffect( BaseLocusEffect locusEffect )
        {
            CheckInitialized();

            if ( locusEffect == null )
            {
                throw new ArgumentNullException( "locusEffect" );
            }

            InternalRemoveLocusEffect( locusEffect.Name );
        }

        /// <summary>
        /// Remove and unregister an existing locus effect instance, by name
        /// </summary>
        /// <param name="locusEffectName"></param>
        public void RemoveLocusEffect( string locusEffectName )
        {
            CheckInitialized();

            if ( locusEffectName == string.Empty )
            {
                throw new ArgumentException( "locusEffect" );
            }


            InternalRemoveLocusEffect( locusEffectName );
        }


        /// <summary>
        /// Gets the locus effect by name.
        /// </summary>
        /// <param name="locusEffectName">Name of the locus effect.</param>
        /// <returns></returns>
        public BaseLocusEffect GetLocusEffect( string locusEffectName )
        {
            CheckInitialized();

            return InternalGetLocusEffect( locusEffectName );
        }


        /// <summary>
        /// Gets all locus effects.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, BaseLocusEffect> GetLocusEffects()
        {
            CheckInitialized();

            return InternalGetLocusEffects();
        }


        #endregion

        #region Internal Properties

        /// <summary>
        /// Reference to Effect Window instance
        /// </summary>
        internal EffectWindow EffectWindow
        {
            get
            {
                CheckInitialized();

                return m_effectWindow;
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Checks that the instance is initialized.
        /// </summary>
        private void CheckInitialized()
        {
            if ( !m_initialized )
            {
                throw new ApplicationException( "LocusEffect was not initialized. Make sure Initialize() is called once" );
            }
        }

        /// <summary>
        /// Checks that the locus effect exists.
        /// </summary>
        /// <param name="locusEffectName">Name of the locus effect.</param>
        /// <returns></returns>
        private BaseLocusEffect CheckLocusEffectExists( string locusEffectName )
        {
            BaseLocusEffect locusEffect = this.InternalGetLocusEffect( locusEffectName );

            if ( locusEffect == null )
            {
                throw new ApplicationException( string.Format( "LocusEffect '{0}' is not registered", locusEffectName ) );
            }

            return locusEffect;
        }

        /// <summary>
        /// Internally stops active locus effect.
        /// </summary>
        private void InternalStopActiveLocusEffect()
        {
            // Stop active effect
            if ( m_activeEffect != null )
            {
                m_activeEffect.StopEffect();
            }
            m_activeEffect = null;
        }

        /// <summary>
        /// Internally shows locus effect.
        /// </summary>
        /// <param name="activatorForm">The activator form.</param>
        /// <param name="locusScreenBounds">The locus screen bounds.</param>
        /// <param name="locusEffectName">Name of the locus effect.</param>
        private void InternalShowLocusEffect( Form activatorForm, Rectangle locusScreenBounds, string locusEffectName )
        {
            BaseLocusEffect locusEffect = InternalGetLocusEffect( locusEffectName );

            if ( locusEffect == null )
            {
                throw new ApplicationException( string.Format( "Could not show locus effect, '{0}' is not registered", locusEffectName ) );
            }

            this.InternalStopActiveLocusEffect();

            m_activeEffect = locusEffect;

            locusEffect.ShowEffect( activatorForm, locusScreenBounds );
        }

        /// <summary>
        /// Internally adds the locus effect.
        /// </summary>
        /// <param name="locusEffect">The locus effect.</param>
        private void InternalAddLocusEffect( BaseLocusEffect locusEffect )
        {
            locusEffect.Owner = this;
            locusEffect.EffectFinished += new EventHandler( activeEffect_EffectFinished );

            if ( this.InternalGetLocusEffect( locusEffect.Name ) != null )
            {
                throw new ApplicationException( string.Format( "Could not add locus effect '{0}', it is already registered", locusEffect.Name ) );
            }

            // Register locus effect
            m_registeredEffects.Add( locusEffect.Name, locusEffect );
        }

        /// <summary>
        /// Internally removes the locus effect.
        /// </summary>
        /// <param name="locusEffect">The locus effect.</param>
        private void InternalRemoveLocusEffect( BaseLocusEffect locusEffect )
        {
            this.CheckLocusEffectExists( locusEffect.Name );

            this.UnregisterLocusEffect( locusEffect );
        }

        /// <summary>
        /// Internally removes the locus effect by name.
        /// </summary>
        /// <param name="locusEffectName">Name of the locus effect.</param>
        private void InternalRemoveLocusEffect( string locusEffectName )
        {
            BaseLocusEffect locusEffect = this.CheckLocusEffectExists( locusEffectName );

            this.UnregisterLocusEffect( locusEffect );
        }


        /// <summary>
        /// Internally gets all registerd locus effects.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, BaseLocusEffect> InternalGetLocusEffects()
        {
            return new Dictionary<string, BaseLocusEffect>( m_registeredEffects );
        }

        /// <summary>
        /// Unregisters the locus effect.
        /// </summary>
        /// <param name="locusEffect">The locus effect.</param>
        private void UnregisterLocusEffect( BaseLocusEffect locusEffect )
        {
            // Unsubscribe from EffectFinished event
            locusEffect.EffectFinished -= new EventHandler( activeEffect_EffectFinished );

            // Remove from registered effects collection
            m_registeredEffects.Remove( locusEffect.Name );
        }

        /// <summary>
        /// Internally gets the locus effect by name.
        /// </summary>
        /// <param name="locusEffectName">Name of the locus effect.</param>
        /// <returns></returns>
        private BaseLocusEffect InternalGetLocusEffect( string locusEffectName )
        {
            BaseLocusEffect locusEffect;
            m_registeredEffects.TryGetValue( locusEffectName, out locusEffect );

            return locusEffect;
        }


        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the EffectFinished event of the activeEffect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void activeEffect_EffectFinished( object sender, EventArgs e )
        {
            // Effect is finished - Reset active effect
            m_activeEffect = null;
        }

        #endregion

        #region Protected Members

        protected int m_framesPerSecond = 25;

        #endregion

        #region Private Members

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private bool m_initialized = false;

        private Dictionary<string, BaseLocusEffect> m_registeredEffects;
        private BaseLocusEffect m_activeEffect = null;

        private EffectWindow m_effectWindow = null;

        #endregion
    }
}
