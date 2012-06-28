namespace BigMansStuff.TestLocusEffects
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.customAnimatedArrowRadioButton = new System.Windows.Forms.RadioButton();
            this.animatedImageRadioButton = new System.Windows.Forms.RadioButton();
            this.activeEffectLabel = new System.Windows.Forms.Label();
            this.bitmapRadioButton = new System.Windows.Forms.RadioButton();
            this.customBulbRadioButton = new System.Windows.Forms.RadioButton();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.exampleTextBox = new System.Windows.Forms.RichTextBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchForLabel = new System.Windows.Forms.Label();
            this.customArrow2RadioButton = new System.Windows.Forms.RadioButton();
            this.customBeacon2RadioButton = new System.Windows.Forms.RadioButton();
            this.customBeaconRadioButton = new System.Windows.Forms.RadioButton();
            this.customArrowRadioButton = new System.Windows.Forms.RadioButton();
            this.tabControlPanel = new System.Windows.Forms.Panel();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.stateTabPage = new System.Windows.Forms.TabPage();
            this.statePanel = new System.Windows.Forms.Panel();
            this.whereButton = new System.Windows.Forms.Button();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.usaMapPanel = new System.Windows.Forms.Panel();
            this.usaMapPictureBox = new System.Windows.Forms.PictureBox();
            this.searchTabPage = new System.Windows.Forms.TabPage();
            this.textBoxPanel = new System.Windows.Forms.Panel();
            this.screenTabPage = new System.Windows.Forms.TabPage();
            this.demoControl = new System.Windows.Forms.TextBox();
            this.showForControlButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.showScreenButton = new System.Windows.Forms.Button();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.beaconRadioButton = new System.Windows.Forms.RadioButton();
            this.arrowRadioButton = new System.Windows.Forms.RadioButton();
            this.customLabel = new System.Windows.Forms.Label();
            this.predefinedLabel = new System.Windows.Forms.Label();
            this.customBeacon3RadioButton = new System.Windows.Forms.RadioButton();
            this.effectPanel = new System.Windows.Forms.Panel();
            this.openGLRadioButton = new System.Windows.Forms.RadioButton();
            this.loadOpenGLEffectsButton = new System.Windows.Forms.Button();
            this.customTextBox = new System.Windows.Forms.TextBox();
            this.locusArea = new System.Windows.Forms.PictureBox();
            this.showLocusEffectButton = new System.Windows.Forms.Button();
            this.returnLabel = new System.Windows.Forms.Label();
            this.textRadioButton = new System.Windows.Forms.RadioButton();
            this.customTextRadioButton = new System.Windows.Forms.RadioButton();
            this.locusEffectsProvider = new BigMansStuff.LocusEffects.LocusEffectsProvider(this.components);
            this.hideButton = new System.Windows.Forms.Button();
            this.searchPanel.SuspendLayout();
            this.tabControlPanel.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.stateTabPage.SuspendLayout();
            this.statePanel.SuspendLayout();
            this.usaMapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usaMapPictureBox)).BeginInit();
            this.searchTabPage.SuspendLayout();
            this.textBoxPanel.SuspendLayout();
            this.screenTabPage.SuspendLayout();
            this.effectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.locusArea)).BeginInit();
            this.SuspendLayout();
            // 
            // customAnimatedArrowRadioButton
            // 
            this.customAnimatedArrowRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customAnimatedArrowRadioButton.Location = new System.Drawing.Point(40, 258);
            this.customAnimatedArrowRadioButton.Name = "customAnimatedArrowRadioButton";
            this.customAnimatedArrowRadioButton.Size = new System.Drawing.Size(176, 24);
            this.customAnimatedArrowRadioButton.TabIndex = 15;
            this.customAnimatedArrowRadioButton.Text = "Custom animated arrow";
            this.customAnimatedArrowRadioButton.Click += new System.EventHandler(this.customAnimatedArrowRadioButton_CheckedChanged);
            // 
            // animatedImageRadioButton
            // 
            this.animatedImageRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.animatedImageRadioButton.Location = new System.Drawing.Point(40, 120);
            this.animatedImageRadioButton.Name = "animatedImageRadioButton";
            this.animatedImageRadioButton.Size = new System.Drawing.Size(112, 24);
            this.animatedImageRadioButton.TabIndex = 6;
            this.animatedImageRadioButton.Text = "Animated Image";
            this.animatedImageRadioButton.Click += new System.EventHandler(this.animatedImageRadioButton_CheckedChanged);
            // 
            // activeEffectLabel
            // 
            this.activeEffectLabel.Location = new System.Drawing.Point(8, 8);
            this.activeEffectLabel.Name = "activeEffectLabel";
            this.activeEffectLabel.Size = new System.Drawing.Size(144, 23);
            this.activeEffectLabel.TabIndex = 0;
            this.activeEffectLabel.Text = "Select a Locus Effect:";
            // 
            // bitmapRadioButton
            // 
            this.bitmapRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bitmapRadioButton.Location = new System.Drawing.Point(40, 72);
            this.bitmapRadioButton.Name = "bitmapRadioButton";
            this.bitmapRadioButton.Size = new System.Drawing.Size(112, 24);
            this.bitmapRadioButton.TabIndex = 4;
            this.bitmapRadioButton.Text = "Bitmap";
            this.bitmapRadioButton.CheckedChanged += new System.EventHandler(this.bitmapRadioButton_CheckedChanged);
            // 
            // customBulbRadioButton
            // 
            this.customBulbRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBulbRadioButton.Location = new System.Drawing.Point(151, 217);
            this.customBulbRadioButton.Name = "customBulbRadioButton";
            this.customBulbRadioButton.Size = new System.Drawing.Size(120, 24);
            this.customBulbRadioButton.TabIndex = 13;
            this.customBulbRadioButton.Text = "Bulb";
            this.customBulbRadioButton.Click += new System.EventHandler(this.customBulbRadioButton_CheckedChanged);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(120, 8);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(136, 20);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.Text = "CodeProject";
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            // 
            // findButton
            // 
            this.findButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.findButton.Location = new System.Drawing.Point(264, 8);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(48, 20);
            this.findButton.TabIndex = 2;
            this.findButton.Text = "&Find";
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // exampleTextBox
            // 
            this.exampleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exampleTextBox.Location = new System.Drawing.Point(0, 0);
            this.exampleTextBox.Name = "exampleTextBox";
            this.exampleTextBox.Size = new System.Drawing.Size(476, 448);
            this.exampleTextBox.TabIndex = 3;
            this.exampleTextBox.Text = resources.GetString("exampleTextBox.Text");
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.findButton);
            this.searchPanel.Controls.Add(this.searchForLabel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(476, 32);
            this.searchPanel.TabIndex = 4;
            // 
            // searchForLabel
            // 
            this.searchForLabel.Location = new System.Drawing.Point(8, 8);
            this.searchForLabel.Name = "searchForLabel";
            this.searchForLabel.Size = new System.Drawing.Size(100, 23);
            this.searchForLabel.TabIndex = 0;
            this.searchForLabel.Text = "&Search for:";
            // 
            // customArrow2RadioButton
            // 
            this.customArrow2RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customArrow2RadioButton.Location = new System.Drawing.Point(40, 175);
            this.customArrow2RadioButton.Name = "customArrow2RadioButton";
            this.customArrow2RadioButton.Size = new System.Drawing.Size(160, 24);
            this.customArrow2RadioButton.TabIndex = 9;
            this.customArrow2RadioButton.Text = "Custom arrow (Robin Hood)";
            this.customArrow2RadioButton.Click += new System.EventHandler(this.customArrow2RadioButton_CheckedChanged);
            // 
            // customBeacon2RadioButton
            // 
            this.customBeacon2RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBeacon2RadioButton.Location = new System.Drawing.Point(151, 197);
            this.customBeacon2RadioButton.Name = "customBeacon2RadioButton";
            this.customBeacon2RadioButton.Size = new System.Drawing.Size(120, 24);
            this.customBeacon2RadioButton.TabIndex = 11;
            this.customBeacon2RadioButton.Text = "Laundry beacon";
            this.customBeacon2RadioButton.Click += new System.EventHandler(this.customBeacon2RadioButton_CheckedChanged);
            // 
            // customBeaconRadioButton
            // 
            this.customBeaconRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBeaconRadioButton.Location = new System.Drawing.Point(40, 196);
            this.customBeaconRadioButton.Name = "customBeaconRadioButton";
            this.customBeaconRadioButton.Size = new System.Drawing.Size(104, 24);
            this.customBeaconRadioButton.TabIndex = 10;
            this.customBeaconRadioButton.Text = "Shrinking beacon";
            this.customBeaconRadioButton.Click += new System.EventHandler(this.customBeaconRadioButton_CheckedChanged);
            // 
            // customArrowRadioButton
            // 
            this.customArrowRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customArrowRadioButton.Location = new System.Drawing.Point(40, 155);
            this.customArrowRadioButton.Name = "customArrowRadioButton";
            this.customArrowRadioButton.Size = new System.Drawing.Size(152, 24);
            this.customArrowRadioButton.TabIndex = 8;
            this.customArrowRadioButton.Text = "Custom arrow (Curved)";
            this.customArrowRadioButton.Click += new System.EventHandler(this.customArrowRadioButton_CheckedChanged);
            // 
            // tabControlPanel
            // 
            this.tabControlPanel.Controls.Add(this.mainTabControl);
            this.tabControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel.Location = new System.Drawing.Point(264, 0);
            this.tabControlPanel.Name = "tabControlPanel";
            this.tabControlPanel.Size = new System.Drawing.Size(484, 506);
            this.tabControlPanel.TabIndex = 4;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.stateTabPage);
            this.mainTabControl.Controls.Add(this.searchTabPage);
            this.mainTabControl.Controls.Add(this.screenTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(484, 506);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // stateTabPage
            // 
            this.stateTabPage.Controls.Add(this.statePanel);
            this.stateTabPage.Controls.Add(this.usaMapPanel);
            this.stateTabPage.Location = new System.Drawing.Point(4, 22);
            this.stateTabPage.Name = "stateTabPage";
            this.stateTabPage.Size = new System.Drawing.Size(476, 480);
            this.stateTabPage.TabIndex = 3;
            this.stateTabPage.Text = "Example - Search state in map";
            // 
            // statePanel
            // 
            this.statePanel.Controls.Add(this.whereButton);
            this.statePanel.Controls.Add(this.stateComboBox);
            this.statePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statePanel.Location = new System.Drawing.Point(0, 0);
            this.statePanel.Name = "statePanel";
            this.statePanel.Size = new System.Drawing.Size(476, 32);
            this.statePanel.TabIndex = 0;
            // 
            // whereButton
            // 
            this.whereButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.whereButton.Location = new System.Drawing.Point(232, 7);
            this.whereButton.Name = "whereButton";
            this.whereButton.Size = new System.Drawing.Size(80, 20);
            this.whereButton.TabIndex = 1;
            this.whereButton.Text = "&Where is it?";
            this.whereButton.Click += new System.EventHandler(this.whereButton_Click);
            // 
            // stateComboBox
            // 
            this.stateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateComboBox.Location = new System.Drawing.Point(56, 6);
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size(168, 21);
            this.stateComboBox.Sorted = true;
            this.stateComboBox.TabIndex = 0;
            // 
            // usaMapPanel
            // 
            this.usaMapPanel.Controls.Add(this.usaMapPictureBox);
            this.usaMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usaMapPanel.Location = new System.Drawing.Point(0, 0);
            this.usaMapPanel.Name = "usaMapPanel";
            this.usaMapPanel.Size = new System.Drawing.Size(476, 480);
            this.usaMapPanel.TabIndex = 1;
            // 
            // usaMapPictureBox
            // 
            this.usaMapPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.usaMapPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("usaMapPictureBox.Image")));
            this.usaMapPictureBox.Location = new System.Drawing.Point(3, 33);
            this.usaMapPictureBox.Name = "usaMapPictureBox";
            this.usaMapPictureBox.Size = new System.Drawing.Size(470, 445);
            this.usaMapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.usaMapPictureBox.TabIndex = 0;
            this.usaMapPictureBox.TabStop = false;
            this.usaMapPictureBox.Resize += new System.EventHandler(this.usaMapPictureBox_Resize);
            // 
            // searchTabPage
            // 
            this.searchTabPage.Controls.Add(this.textBoxPanel);
            this.searchTabPage.Controls.Add(this.searchPanel);
            this.searchTabPage.Location = new System.Drawing.Point(4, 22);
            this.searchTabPage.Name = "searchTabPage";
            this.searchTabPage.Size = new System.Drawing.Size(476, 480);
            this.searchTabPage.TabIndex = 2;
            this.searchTabPage.Text = "Example - Text search";
            // 
            // textBoxPanel
            // 
            this.textBoxPanel.Controls.Add(this.exampleTextBox);
            this.textBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPanel.Location = new System.Drawing.Point(0, 32);
            this.textBoxPanel.Name = "textBoxPanel";
            this.textBoxPanel.Size = new System.Drawing.Size(476, 448);
            this.textBoxPanel.TabIndex = 5;
            // 
            // screenTabPage
            // 
            this.screenTabPage.Controls.Add(this.hideButton);
            this.screenTabPage.Controls.Add(this.demoControl);
            this.screenTabPage.Controls.Add(this.showForControlButton);
            this.screenTabPage.Controls.Add(this.label3);
            this.screenTabPage.Controls.Add(this.showScreenButton);
            this.screenTabPage.Controls.Add(this.yTextBox);
            this.screenTabPage.Controls.Add(this.xTextBox);
            this.screenTabPage.Controls.Add(this.label2);
            this.screenTabPage.Controls.Add(this.label1);
            this.screenTabPage.Location = new System.Drawing.Point(4, 22);
            this.screenTabPage.Name = "screenTabPage";
            this.screenTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.screenTabPage.Size = new System.Drawing.Size(476, 480);
            this.screenTabPage.TabIndex = 4;
            this.screenTabPage.Text = "Example - Misc";
            this.screenTabPage.UseVisualStyleBackColor = true;
            // 
            // demoControl
            // 
            this.demoControl.Location = new System.Drawing.Point(67, 137);
            this.demoControl.Name = "demoControl";
            this.demoControl.Size = new System.Drawing.Size(100, 20);
            this.demoControl.TabIndex = 8;
            this.demoControl.Text = "Demo Control";
            // 
            // showForControlButton
            // 
            this.showForControlButton.Location = new System.Drawing.Point(209, 133);
            this.showForControlButton.Name = "showForControlButton";
            this.showForControlButton.Size = new System.Drawing.Size(109, 23);
            this.showForControlButton.TabIndex = 7;
            this.showForControlButton.Text = "Show for Control";
            this.showForControlButton.UseVisualStyleBackColor = true;
            this.showForControlButton.Click += new System.EventHandler(this.showForControlButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Screen Coordinates:";
            // 
            // showScreenButton
            // 
            this.showScreenButton.Location = new System.Drawing.Point(209, 36);
            this.showScreenButton.Name = "showScreenButton";
            this.showScreenButton.Size = new System.Drawing.Size(75, 23);
            this.showScreenButton.TabIndex = 4;
            this.showScreenButton.Text = "Show";
            this.showScreenButton.UseVisualStyleBackColor = true;
            this.showScreenButton.Click += new System.EventHandler(this.showScreenButton_Click);
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(88, 59);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(100, 20);
            this.yTextBox.TabIndex = 3;
            this.yTextBox.Text = "100";
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(88, 37);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(100, 20);
            this.xTextBox.TabIndex = 2;
            this.xTextBox.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // beaconRadioButton
            // 
            this.beaconRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.beaconRadioButton.Location = new System.Drawing.Point(122, 48);
            this.beaconRadioButton.Name = "beaconRadioButton";
            this.beaconRadioButton.Size = new System.Drawing.Size(104, 24);
            this.beaconRadioButton.TabIndex = 3;
            this.beaconRadioButton.Text = "Radar beacon";
            this.beaconRadioButton.Click += new System.EventHandler(this.beaconRadioButton_CheckedChanged);
            // 
            // arrowRadioButton
            // 
            this.arrowRadioButton.Checked = true;
            this.arrowRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.arrowRadioButton.Location = new System.Drawing.Point(40, 48);
            this.arrowRadioButton.Name = "arrowRadioButton";
            this.arrowRadioButton.Size = new System.Drawing.Size(104, 24);
            this.arrowRadioButton.TabIndex = 2;
            this.arrowRadioButton.TabStop = true;
            this.arrowRadioButton.Text = "Arrow";
            this.arrowRadioButton.CheckedChanged += new System.EventHandler(this.arrowRadioButton_CheckedChanged);
            // 
            // customLabel
            // 
            this.customLabel.Location = new System.Drawing.Point(17, 140);
            this.customLabel.Name = "customLabel";
            this.customLabel.Size = new System.Drawing.Size(100, 16);
            this.customLabel.TabIndex = 7;
            this.customLabel.Text = "Custom:";
            // 
            // predefinedLabel
            // 
            this.predefinedLabel.Location = new System.Drawing.Point(16, 32);
            this.predefinedLabel.Name = "predefinedLabel";
            this.predefinedLabel.Size = new System.Drawing.Size(100, 16);
            this.predefinedLabel.TabIndex = 1;
            this.predefinedLabel.Text = "Predefined:";
            // 
            // customBeacon3RadioButton
            // 
            this.customBeacon3RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBeacon3RadioButton.Location = new System.Drawing.Point(40, 216);
            this.customBeacon3RadioButton.Name = "customBeacon3RadioButton";
            this.customBeacon3RadioButton.Size = new System.Drawing.Size(120, 24);
            this.customBeacon3RadioButton.TabIndex = 12;
            this.customBeacon3RadioButton.Text = "Heartbeat beacon";
            this.customBeacon3RadioButton.Click += new System.EventHandler(this.customBeacon3RadioButton_CheckedChanged);
            // 
            // effectPanel
            // 
            this.effectPanel.Controls.Add(this.openGLRadioButton);
            this.effectPanel.Controls.Add(this.loadOpenGLEffectsButton);
            this.effectPanel.Controls.Add(this.customTextBox);
            this.effectPanel.Controls.Add(this.customAnimatedArrowRadioButton);
            this.effectPanel.Controls.Add(this.animatedImageRadioButton);
            this.effectPanel.Controls.Add(this.activeEffectLabel);
            this.effectPanel.Controls.Add(this.customBeacon2RadioButton);
            this.effectPanel.Controls.Add(this.customBeaconRadioButton);
            this.effectPanel.Controls.Add(this.customArrowRadioButton);
            this.effectPanel.Controls.Add(this.beaconRadioButton);
            this.effectPanel.Controls.Add(this.arrowRadioButton);
            this.effectPanel.Controls.Add(this.bitmapRadioButton);
            this.effectPanel.Controls.Add(this.customBulbRadioButton);
            this.effectPanel.Controls.Add(this.customArrow2RadioButton);
            this.effectPanel.Controls.Add(this.customLabel);
            this.effectPanel.Controls.Add(this.predefinedLabel);
            this.effectPanel.Controls.Add(this.customBeacon3RadioButton);
            this.effectPanel.Controls.Add(this.locusArea);
            this.effectPanel.Controls.Add(this.showLocusEffectButton);
            this.effectPanel.Controls.Add(this.returnLabel);
            this.effectPanel.Controls.Add(this.textRadioButton);
            this.effectPanel.Controls.Add(this.customTextRadioButton);
            this.effectPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.effectPanel.Location = new System.Drawing.Point(0, 0);
            this.effectPanel.Name = "effectPanel";
            this.effectPanel.Size = new System.Drawing.Size(264, 506);
            this.effectPanel.TabIndex = 3;
            // 
            // openGLRadioButton
            // 
            this.openGLRadioButton.Enabled = false;
            this.openGLRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.openGLRadioButton.Location = new System.Drawing.Point(40, 329);
            this.openGLRadioButton.Name = "openGLRadioButton";
            this.openGLRadioButton.Size = new System.Drawing.Size(123, 24);
            this.openGLRadioButton.TabIndex = 20;
            this.openGLRadioButton.Text = "Open GL Demo";
            this.openGLRadioButton.CheckedChanged += new System.EventHandler(this.openGLRadioButton_CheckedChanged);
            // 
            // loadOpenGLEffectsButton
            // 
            this.loadOpenGLEffectsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadOpenGLEffectsButton.Location = new System.Drawing.Point(8, 300);
            this.loadOpenGLEffectsButton.Name = "loadOpenGLEffectsButton";
            this.loadOpenGLEffectsButton.Size = new System.Drawing.Size(128, 23);
            this.loadOpenGLEffectsButton.TabIndex = 19;
            this.loadOpenGLEffectsButton.Text = "Load OpenGL Effects";
            this.loadOpenGLEffectsButton.UseVisualStyleBackColor = true;
            this.loadOpenGLEffectsButton.Click += new System.EventHandler(this.loadOpenGLEffectButton_Click);
            // 
            // customTextBox
            // 
            this.customTextBox.Location = new System.Drawing.Point(122, 98);
            this.customTextBox.Name = "customTextBox";
            this.customTextBox.Size = new System.Drawing.Size(92, 20);
            this.customTextBox.TabIndex = 18;
            this.customTextBox.Text = "Hey!";
            this.customTextBox.TextChanged += new System.EventHandler(this.customTextBox_TextChanged);
            // 
            // locusArea
            // 
            this.locusArea.BackColor = System.Drawing.SystemColors.Control;
            this.locusArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locusArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.locusArea.Image = ((System.Drawing.Image)(resources.GetObject("locusArea.Image")));
            this.locusArea.Location = new System.Drawing.Point(48, 405);
            this.locusArea.Name = "locusArea";
            this.locusArea.Size = new System.Drawing.Size(96, 96);
            this.locusArea.TabIndex = 14;
            this.locusArea.TabStop = false;
            this.locusArea.Click += new System.EventHandler(this.locusArea_Click);
            // 
            // showLocusEffectButton
            // 
            this.showLocusEffectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.showLocusEffectButton.Location = new System.Drawing.Point(8, 373);
            this.showLocusEffectButton.Name = "showLocusEffectButton";
            this.showLocusEffectButton.Size = new System.Drawing.Size(128, 23);
            this.showLocusEffectButton.TabIndex = 16;
            this.showLocusEffectButton.Text = "Show Locus Effect";
            this.showLocusEffectButton.Click += new System.EventHandler(this.showLocusEffectButton_Click);
            // 
            // returnLabel
            // 
            this.returnLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.returnLabel.Location = new System.Drawing.Point(144, 381);
            this.returnLabel.Name = "returnLabel";
            this.returnLabel.Size = new System.Drawing.Size(70, 16);
            this.returnLabel.TabIndex = 17;
            this.returnLabel.Text = "(or hit F12)";
            // 
            // textRadioButton
            // 
            this.textRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.textRadioButton.Location = new System.Drawing.Point(40, 96);
            this.textRadioButton.Name = "textRadioButton";
            this.textRadioButton.Size = new System.Drawing.Size(76, 24);
            this.textRadioButton.TabIndex = 5;
            this.textRadioButton.Text = "Text";
            this.textRadioButton.Click += new System.EventHandler(this.textRadioButton_CheckedChanged);
            // 
            // customTextRadioButton
            // 
            this.customTextRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customTextRadioButton.Location = new System.Drawing.Point(40, 237);
            this.customTextRadioButton.Name = "customTextRadioButton";
            this.customTextRadioButton.Size = new System.Drawing.Size(176, 24);
            this.customTextRadioButton.TabIndex = 14;
            this.customTextRadioButton.Text = "Full Screen Text (Center Monitor)";
            this.customTextRadioButton.Click += new System.EventHandler(this.customTextRadioButton_CheckedChanged);
            // 
            // hideButton
            // 
            this.hideButton.Location = new System.Drawing.Point(209, 65);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(120, 23);
            this.hideButton.TabIndex = 9;
            this.hideButton.Text = "Hide and Show Effect";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 506);
            this.Controls.Add(this.tabControlPanel);
            this.Controls.Add(this.effectPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "LocusEffects Tester";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.tabControlPanel.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.stateTabPage.ResumeLayout(false);
            this.statePanel.ResumeLayout(false);
            this.usaMapPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usaMapPictureBox)).EndInit();
            this.searchTabPage.ResumeLayout(false);
            this.textBoxPanel.ResumeLayout(false);
            this.screenTabPage.ResumeLayout(false);
            this.screenTabPage.PerformLayout();
            this.effectPanel.ResumeLayout(false);
            this.effectPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.locusArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton customAnimatedArrowRadioButton;
        private System.Windows.Forms.RadioButton animatedImageRadioButton;
        private System.Windows.Forms.Label activeEffectLabel;
        private System.Windows.Forms.RadioButton bitmapRadioButton;
        private System.Windows.Forms.RadioButton customBulbRadioButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.RichTextBox exampleTextBox;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label searchForLabel;
        private System.Windows.Forms.RadioButton customArrow2RadioButton;
        private System.Windows.Forms.RadioButton customBeacon2RadioButton;
        private System.Windows.Forms.RadioButton customBeaconRadioButton;
        private System.Windows.Forms.RadioButton customArrowRadioButton;
        private System.Windows.Forms.Panel tabControlPanel;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage stateTabPage;
        private System.Windows.Forms.Panel statePanel;
        private System.Windows.Forms.Button whereButton;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.Panel usaMapPanel;
        private System.Windows.Forms.PictureBox usaMapPictureBox;
        private System.Windows.Forms.TabPage searchTabPage;
        private System.Windows.Forms.Panel textBoxPanel;
        private System.Windows.Forms.RadioButton beaconRadioButton;
        private System.Windows.Forms.RadioButton arrowRadioButton;
        private System.Windows.Forms.Label customLabel;
        private System.Windows.Forms.Label predefinedLabel;
        private System.Windows.Forms.RadioButton customBeacon3RadioButton;
        private System.Windows.Forms.Panel effectPanel;
        private System.Windows.Forms.PictureBox locusArea;
        private System.Windows.Forms.Button showLocusEffectButton;
        private System.Windows.Forms.Label returnLabel;
        private System.Windows.Forms.RadioButton textRadioButton;
        private System.Windows.Forms.RadioButton customTextRadioButton;
        private BigMansStuff.LocusEffects.LocusEffectsProvider locusEffectsProvider;
        private System.Windows.Forms.TextBox customTextBox;
        private System.Windows.Forms.RadioButton openGLRadioButton;
        private System.Windows.Forms.Button loadOpenGLEffectsButton;
        private System.Windows.Forms.TabPage screenTabPage;
        private System.Windows.Forms.Button showScreenButton;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button showForControlButton;
        private System.Windows.Forms.TextBox demoControl;
        private System.Windows.Forms.Button hideButton;
    }
}