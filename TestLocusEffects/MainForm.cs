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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BigMansStuff.LocusEffects;
using System.Reflection;
using System.Resources;

namespace BigMansStuff.TestLocusEffects
{
    public partial class MainForm : Form
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load( object sender, System.EventArgs e )
        {
            // Initialize LocusEffects
            this.InitializeLocusEffects();

            // Initialize map example
            this.InitializeMapExample();
        }

        /// <summary>
        /// Handles the Click event of the showLocusEffectButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void showLocusEffectButton_Click( object sender, System.EventArgs e )
        {
            System.Drawing.Rectangle locusRect = locusArea.Parent.RectangleToScreen( locusArea.Bounds );

            // Show the selected locus effect
            locusEffectsProvider.ShowLocusEffect( this, locusRect, m_activeLocusEffectName );
        }

        /// <summary>
        /// Handles the KeyUp event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MainForm_KeyUp( object sender, System.Windows.Forms.KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.F12 )
            {
                showLocusEffectButton_Click( this, System.EventArgs.Empty );
            }
        }

        /// <summary>
        /// Handles the Click event of the findButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void findButton_Click( object sender, System.EventArgs e )
        {
            int nextMatchIndex = exampleTextBox.Text.IndexOf( searchTextBox.Text, exampleTextBox.SelectionStart + 1, StringComparison.InvariantCultureIgnoreCase );

            if ( nextMatchIndex != -1 )
            {
                this.ShowEffectOnTextPosition( nextMatchIndex );
            }
            else
            {
                locusEffectsProvider.StopActiveLocusEffect();
                exampleTextBox.Focus();
                exampleTextBox.SelectionLength = 0;
                exampleTextBox.SelectionStart = 0;
                exampleTextBox.ScrollToCaret();

                nextMatchIndex = exampleTextBox.Text.IndexOf( searchTextBox.Text, exampleTextBox.SelectionStart + 1, StringComparison.InvariantCultureIgnoreCase );
                if ( nextMatchIndex != -1 )
                {
                    this.ShowEffectOnTextPosition( nextMatchIndex );
                }
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the arrowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void arrowRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            if ( arrowRadioButton.Checked )
            {
                m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectArrow;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the beaconRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void beaconRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectBeacon;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the bitmapRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void bitmapRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            if ( bitmapRadioButton.Checked )
            {
                m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectBitmap;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customArrowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customArrowRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_customArrowLocusEffect.Name;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customArrow2RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customArrow2RadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_customArrowLocusEffect2.Name;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customBeaconRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customBeaconRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_customBeaconLocusEffect.Name;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customBeacon2RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customBeacon2RadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_customBeaconLocusEffect2.Name;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customBeacon3RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customBeacon3RadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_customBeaconLocusEffect3.Name;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customBulbRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customBulbRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_customBulbEffect.Name;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the animatedImageRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void animatedImageRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectAnimatedImage;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customAnimatedArrowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customAnimatedArrowRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_customAnimatedImageEffect.Name;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the textRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectText;
            // Apply custom text
            customTextBox_TextChanged( this, EventArgs.Empty );
        }

        /// <summary>
        /// Handles the TextChanged event of the customTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void customTextBox_TextChanged( object sender, EventArgs e )
        {
            TextLocusEffect textLocusEffect = locusEffectsProvider.GetLocusEffect( LocusEffectsProvider.DefaultLocusEffectText ) as TextLocusEffect;
            textLocusEffect.Text = customTextBox.Text;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customTextRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customTextRadioButton_CheckedChanged( object sender, System.EventArgs e )
        {
            m_activeLocusEffectName = m_fullScreenTextEffect.Name;
        }

        /// <summary>
        /// Handles the Click event of the locusArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void locusArea_Click( object sender, System.EventArgs e )
        {
            // Get assembly version
            Assembly assembly = Assembly.GetAssembly( typeof( LocusEffectsProvider ) );
            System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo( assembly.Location );

            string assemblyVersion = string.Format( "{0}.{1}.{2}.{3}",
                fileVersionInfo.FileMajorPart,
                fileVersionInfo.FileMinorPart,
                fileVersionInfo.FileBuildPart,
                fileVersionInfo.FilePrivatePart );

            // Show about dialog
            MessageBox.Show( this,
                "© Copyright 2005, BigMan's Stuff - Yuval Naveh" + "\r\n\r\n" +
                "LocusEffects version: " + assemblyVersion,
                "LocusEffects",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information );
        }


        /// <summary>
        /// Handles the Click event of the whereButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void whereButton_Click( object sender, System.EventArgs e )
        {
            // -- Show state on map --
            USAState state = stateComboBox.SelectedItem as USAState;

            Point stateClientPoint = state.StateCoordinate;
            stateClientPoint.X = ( int ) ( ( float ) stateClientPoint.X * usaMapPictureBox.Width / usaMapPictureBox.Image.Width );
            stateClientPoint.Y = ( int ) ( ( float ) stateClientPoint.Y * usaMapPictureBox.Height / usaMapPictureBox.Image.Height );

            Point stateScreenPoint = usaMapPictureBox.PointToScreen( stateClientPoint );

            // Show the selected locus effect
            locusEffectsProvider.ShowLocusEffect( this, stateScreenPoint, m_activeLocusEffectName );
        }

        /// <summary>
        /// Handles the Resize event of the usaMapPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void usaMapPictureBox_Resize( object sender, System.EventArgs e )
        {
            if ( locusEffectsProvider.IsAnimating )
            {
                locusEffectsProvider.StopActiveLocusEffect();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the mainTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mainTabControl_SelectedIndexChanged( object sender, System.EventArgs e )
        {
            if ( locusEffectsProvider.IsAnimating )
            {
                locusEffectsProvider.StopActiveLocusEffect();
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the searchTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void searchTextBox_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Return )
            {
                e.SuppressKeyPress = true;
                findButton.PerformClick();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// LocusEffects initialization
        /// </summary>
        private void InitializeLocusEffects()
        {
            // Initialize the LocusEffects component
            locusEffectsProvider.Initialize();

            // Create custom Locus Effects
            this.CreateCustomLocusEffects();
        }

        /// <summary>
        /// Creates and registers custom locus effects (Demo effects)
        /// </summary>
        private void CreateCustomLocusEffects()
        {
            ResourceManager rm = new ResourceManager( "BigMansStuff.TestLocusEffects.Images.CustomImages", Assembly.GetExecutingAssembly() );

            #region Custom Arrow - Curved
            m_customArrowLocusEffect = new ArrowLocusEffect();
            m_customArrowLocusEffect.Name = "CustomArrow_Curved";
            m_customArrowLocusEffect.AnimationStartColor = Color.Red;
            m_customArrowLocusEffect.AnimationEndColor = Color.Yellow;
            m_customArrowLocusEffect.Bitmap = rm.GetObject( "CustomCurvedArrowBitmap" ) as Bitmap;

            locusEffectsProvider.AddLocusEffect( m_customArrowLocusEffect );

            #endregion

            #region Custom Arrow - Robin Hood
            m_customArrowLocusEffect2 = new ArrowLocusEffect();
            m_customArrowLocusEffect2.Name = "CustomArrow_RobinHood";
            m_customArrowLocusEffect2.AnimationStartColor = Color.Green;
            m_customArrowLocusEffect2.AnimationEndColor = Color.FromArgb( 40, 20, 10 );
            m_customArrowLocusEffect2.Bitmap = rm.GetObject( "CustomRobinHoodArrowBitmap" ) as Bitmap;
            m_customArrowLocusEffect2.ShowShadow = false;
            m_customArrowLocusEffect2.MovementMode = MovementMode.OneWayAlongVector;
            m_customArrowLocusEffect2.MovementCycles = 4;
            m_customArrowLocusEffect2.MovementAmplitude = 50;
            m_customArrowLocusEffect2.MovementVectorAngle = 45; //degrees
            m_customArrowLocusEffect2.LeadInTime = 0; //msec
            m_customArrowLocusEffect2.AnimationTime = 2000; //msec

            locusEffectsProvider.AddLocusEffect( m_customArrowLocusEffect2 );
            #endregion

            #region Custom Beacon - Shrinking

            m_customBeaconLocusEffect = new BeaconLocusEffect();
            m_customBeaconLocusEffect.Name = "CustomBeacon_Shrinking";
            m_customBeaconLocusEffect.InitialSize = new Size( 100, 100 );
            m_customBeaconLocusEffect.AnimationTime = 1000;
            m_customBeaconLocusEffect.AnimationStartColor = Color.LightBlue;
            m_customBeaconLocusEffect.AnimationEndColor = Color.LightBlue;
            m_customBeaconLocusEffect.AnimationOuterColor = Color.BlueViolet;
            m_customBeaconLocusEffect.BrokenRing = true;
            m_customBeaconLocusEffect.RingWidth = 6;
            m_customBeaconLocusEffect.OuterRingWidth = 3;
            m_customBeaconLocusEffect.Rotate = true;
            m_customBeaconLocusEffect.RotatationSpeed = 90;
            m_customBeaconLocusEffect.ShowShadow = true;

            locusEffectsProvider.AddLocusEffect( m_customBeaconLocusEffect );
            #endregion

            #region Custom Beacon - Laundry
            m_customBeaconLocusEffect2 = new BeaconLocusEffect();
            m_customBeaconLocusEffect2.Name = "CustomBeacon2";
            m_customBeaconLocusEffect2.InitialSize = new Size( 100, 100 );
            m_customBeaconLocusEffect2.AnimationTime = 2000;
            m_customBeaconLocusEffect2.LeadOutTime = 0;
            m_customBeaconLocusEffect2.AnimationStartColor = Color.Green;
            m_customBeaconLocusEffect2.AnimationEndColor = Color.DarkGreen;
            m_customBeaconLocusEffect2.AnimationOuterColor = Color.LightGreen;
            m_customBeaconLocusEffect2.BrokenRing = true;
            m_customBeaconLocusEffect2.RingWidth = 6;
            m_customBeaconLocusEffect2.OuterRingWidth = 3;
            m_customBeaconLocusEffect2.Rotate = true;
            m_customBeaconLocusEffect2.RotatationSpeed = 180;
            m_customBeaconLocusEffect2.RotateLaundry = true;
            m_customBeaconLocusEffect2.Style = BeaconEffectStyles.None;
            m_customBeaconLocusEffect2.ShowShadow = true;

            locusEffectsProvider.AddLocusEffect( m_customBeaconLocusEffect2 );
            #endregion

            #region Beacon - heart beat
            m_customBeaconLocusEffect3 = new BeaconLocusEffect();
            m_customBeaconLocusEffect3.Name = "CustomBeacon3";
            m_customBeaconLocusEffect3.InitialSize = new Size( 100, 100 );
            m_customBeaconLocusEffect3.AnimationTime = 2500;
            m_customBeaconLocusEffect3.LeadInTime = 0;
            m_customBeaconLocusEffect3.LeadOutTime = 0;
            m_customBeaconLocusEffect3.AnimationStartColor = Color.HotPink;
            m_customBeaconLocusEffect3.AnimationEndColor = Color.HotPink;
            m_customBeaconLocusEffect3.AnimationOuterColor = Color.Pink;
            m_customBeaconLocusEffect3.RingWidth = 6;
            m_customBeaconLocusEffect3.OuterRingWidth = 3;
            m_customBeaconLocusEffect3.BodyFadeOut = true;
            m_customBeaconLocusEffect3.Style = BeaconEffectStyles.HeartBeat;

            locusEffectsProvider.AddLocusEffect( m_customBeaconLocusEffect3 );
            #endregion

            #region Bulb
            m_customBulbEffect = new BitmapLocusEffect();
            m_customBulbEffect.Name = "CustomArrow_Bulb";
            m_customBulbEffect.AnimationStartColor = Color.DarkGray;
            m_customBulbEffect.AnimationEndColor = Color.Yellow;
            m_customBulbEffect.AnimationTime = 1000; // msec
            m_customBulbEffect.Bitmap = rm.GetObject( "CustomBulbBitmap" ) as Bitmap;
            m_customBulbEffect.ShadowOpacity = 40; // %
            m_customBulbEffect.ShadowOffset = new Point( 1, 1 ); // %
            m_customBulbEffect.AnchoringMode = AnchoringMode.CenterOffset;
            m_customBulbEffect.AnchoringOffset = new Point( 0, -locusArea.Height );
            m_customBulbEffect.MovementMode = MovementMode.Buzz;
            m_customBulbEffect.MovementAmplitude = 10;

            locusEffectsProvider.AddLocusEffect( m_customBulbEffect );
            #endregion

            #region Full Screen Text

            m_fullScreenTextEffect = new TextLocusEffect();
            m_fullScreenTextEffect.Name = "FullScreenText";
            m_fullScreenTextEffect.AnimationStartColor = Color.DarkRed;
            m_fullScreenTextEffect.AnimationEndColor = Color.Red;
            m_fullScreenTextEffect.AnchoringMode = AnchoringMode.CenterMonitor;
            m_fullScreenTextEffect.Text = "Operation\r\nCancelled!";
            m_fullScreenTextEffect.Font = new Font( "Verdana", 80 );
            m_fullScreenTextEffect.RotationAngle = -45;

            locusEffectsProvider.AddLocusEffect( m_fullScreenTextEffect );

            #endregion

            #region Custom animated image
            m_customAnimatedImageEffect = new AnimatedImageLocusEffect();
            m_customAnimatedImageEffect.Name = "CustomAnimatedImage";
            m_customAnimatedImageEffect.AddImageFrames( rm.GetObject( "AnimatedArrow" ) as Image );
            m_customAnimatedImageEffect.FocusPoint = new Point( -30, 0 );
            m_customAnimatedImageEffect.AnimationLoops = 1;

            locusEffectsProvider.AddLocusEffect( m_customAnimatedImageEffect );
            #endregion
        }

        /// <summary>
        /// Initializes the map example.
        /// </summary>
        public void InitializeMapExample()
        {
            // I only added some states..its only a demo..
            // Coordinates where measured by hand using a simple bitmap editor
            stateComboBox.Items.Add( new USAState( "NY", "New York", new Point( 453, 170 ) ) );
            stateComboBox.Items.Add( new USAState( "CA", "California", new Point( 140, 222 ) ) );
            stateComboBox.Items.Add( new USAState( "TX", "Texas", new Point( 290, 292 ) ) );
            stateComboBox.Items.Add( new USAState( "AK", "Alaska", new Point( 113, 46 ) ) );
            stateComboBox.Items.Add( new USAState( "GA", "Georgia", new Point( 414, 272 ) ) );
            stateComboBox.Items.Add( new USAState( "WY", "Wyoming", new Point( 234, 182 ) ) );

            // Select first state
            stateComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Shows the effect on a given text position.
        /// </summary>
        /// <param name="nextMatchIndex">Index of the next match.</param>
        private void ShowEffectOnTextPosition( int nextMatchIndex )
        {
            exampleTextBox.Focus();
            exampleTextBox.SelectionLength = 0;
            exampleTextBox.SelectionStart = nextMatchIndex;

            exampleTextBox.ScrollToCaret();

            Point textPoint = Point.Empty;
            GetCaretPos( ref textPoint );

            Point locusPoint = exampleTextBox.PointToScreen( textPoint );
            locusPoint.X += 1; // dirty but this is only a demo..
            locusPoint.Y += 6; // dirty but this is only a demo..

            locusEffectsProvider.ShowLocusEffect( this, locusPoint, m_activeLocusEffectName );
        }

        [System.Runtime.InteropServices.DllImport( "user32" )]
        private static extern int GetCaretPos( ref Point lpPoint );

        #endregion

        #region Private Members

        private ArrowLocusEffect m_customArrowLocusEffect = null;
        private ArrowLocusEffect m_customArrowLocusEffect2 = null;
        private BeaconLocusEffect m_customBeaconLocusEffect = null;
        private BeaconLocusEffect m_customBeaconLocusEffect2 = null;
        private BeaconLocusEffect m_customBeaconLocusEffect3 = null;
        private BitmapLocusEffect m_customBulbEffect = null;
        private TextLocusEffect m_fullScreenTextEffect = null;
        private AnimatedImageLocusEffect m_customAnimatedImageEffect = null;

        private string m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectArrow;

        #endregion

        /// <summary>
        /// Handles the Click event of the loadOpenGLEffectButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void loadOpenGLEffectButton_Click( object sender, EventArgs e )
        {
            string assemblyPath;

            try
            {
                // In run-time, load the OpenGL locus effect from a DLL
                assemblyPath = "BigMansStuff.LocusEffects.OpenGL.dll";
                assemblyPath = Path.GetFullPath( assemblyPath );
                if ( !File.Exists( assemblyPath ) )
                {
    #if DEBUG
                    assemblyPath = @"..\..\..\OpenGLLocusEffect\bin\Debug\BigMansStuff.LocusEffects.OpenGL.dll";
    #else
                    assemblyPath = @"..\..\..\OpenGLLocusEffect\bin\Release\BigMansStuff.LocusEffects.OpenGL.dll";
    #endif
                    assemblyPath = Path.GetFullPath( assemblyPath );
                }
                
                Assembly assembly = Assembly.LoadFile( assemblyPath );
                BaseLocusEffect openGLLocusEffect = assembly.CreateInstance( "BigMansStuff.LocusEffects.OpenGL.DemoOpenGLLocusEffect", true ) as BaseLocusEffect;
                // Add OpenGL LocusEffect
                locusEffectsProvider.AddLocusEffect( openGLLocusEffect );

                loadOpenGLEffectsButton.Enabled = false;
                openGLRadioButton.Enabled = true;
                openGLRadioButton.Checked = true;
            }
            catch ( Exception ex )
            {
                MessageBox.Show( "Failed loading OpenGL Demo LocusEffect\r\n\r\n" + ex.ToString(), "Error" );
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the openGLRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void openGLRadioButton_CheckedChanged( object sender, EventArgs e )
        {
            if ( openGLRadioButton.Checked )
            {
                m_activeLocusEffectName = "DemoOpenGLLocusEffect";
            }
        }

        /// <summary>
        /// Handles the Click event of the showScreenButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void showScreenButton_Click( object sender, EventArgs e )
        {
            int x = Convert.ToInt32( xTextBox.Text );
            int y = Convert.ToInt32( yTextBox.Text );
            System.Drawing.Rectangle locusRect = new Rectangle( x, y, 1, 1 );

            // Show the selected locus effect
            locusEffectsProvider.ShowLocusEffect( this, locusRect, m_activeLocusEffectName );
        }

        /// <summary>
        /// Demonstrates the overloaded helper function that just takes a control parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showForControlButton_Click(object sender, EventArgs e)
        {
            locusEffectsProvider.ShowLocusEffect(demoControl, m_activeLocusEffectName);
        }      
      
    }

    /// <summary>
    /// Class for holding a USA State informationon the map
    /// </summary>
    internal class USAState
    {
        public USAState( string stateSymbol, string stateName, Point stateCoordinate )
        {
            StateSymbol = stateSymbol;
            StateName = stateName;
            StateCoordinate = stateCoordinate;
        }

        public string StateSymbol;
        public string StateName;
        public Point StateCoordinate;

        public override string ToString()
        {
            return StateName + " (" + StateSymbol + ")";
        }
    }
}