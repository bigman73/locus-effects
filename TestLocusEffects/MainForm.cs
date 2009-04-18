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
using System.Collections ;
using System.ComponentModel ;
using System.Windows.Forms ;
using System.Data ;
using System.Resources ;
using System.Reflection ;

using BigMansStuff.LocusEffects ;
using BigMansStuff.Common ;

namespace BigMansStuff.TestLocusEffects
{
	/// <summary>
	/// MainForm of LocusEffects tester
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		#region Auto-Generated code

		private System.Windows.Forms.RadioButton arrowRadioButton;
		private System.Windows.Forms.RadioButton beaconRadioButton;
		private System.Windows.Forms.Button showLocusEffectButton;
		private System.Windows.Forms.RadioButton customArrowRadioButton;
		private System.Windows.Forms.RadioButton customBeaconRadioButton;
		private System.Windows.Forms.RadioButton customBeacon2RadioButton;
		private System.Windows.Forms.RadioButton customBeacon3RadioButton;
		private System.Windows.Forms.Label predefinedLabel;
		private System.Windows.Forms.Label customLabel;
		private System.Windows.Forms.RadioButton customArrow2RadioButton;
		private System.Windows.Forms.RadioButton customBulbRadioButton;
		private System.Windows.Forms.RadioButton bitmapRadioButton;
		private System.Windows.Forms.PictureBox locusArea;
		private BigMansStuff.LocusEffects.LocusEffectsProvider locusEffectsProvider;
		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage searchTabPage;
		private System.Windows.Forms.RichTextBox exampleTextBox;
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.Label searchForLabel;
		private System.Windows.Forms.Button findButton;
		private System.Windows.Forms.Panel effectPanel;
		private System.Windows.Forms.Panel tabControlPanel;
		private System.Windows.Forms.Panel searchPanel;
		private System.Windows.Forms.Panel textBoxPanel;
		private System.Windows.Forms.Label activeEffectLabel;
		private System.Windows.Forms.Label returnLabel;
        private System.Windows.Forms.RadioButton textRadioButton;
        private System.Windows.Forms.RadioButton customTextRadioButton;
        private System.Windows.Forms.PictureBox usaMapPictureBox;
        private System.Windows.Forms.Panel usaMapPanel;
        private System.Windows.Forms.Panel statePanel;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.Button whereButton;
        private System.Windows.Forms.TabPage stateTabPage;
        private System.Windows.Forms.RadioButton animatedImageRadioButton;
        private System.Windows.Forms.RadioButton customAnimatedArrowRadioButton;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
            this.showLocusEffectButton = new System.Windows.Forms.Button();
            this.arrowRadioButton = new System.Windows.Forms.RadioButton();
            this.beaconRadioButton = new System.Windows.Forms.RadioButton();
            this.customArrowRadioButton = new System.Windows.Forms.RadioButton();
            this.customBeaconRadioButton = new System.Windows.Forms.RadioButton();
            this.customBeacon2RadioButton = new System.Windows.Forms.RadioButton();
            this.locusArea = new System.Windows.Forms.PictureBox();
            this.customBeacon3RadioButton = new System.Windows.Forms.RadioButton();
            this.predefinedLabel = new System.Windows.Forms.Label();
            this.customLabel = new System.Windows.Forms.Label();
            this.customArrow2RadioButton = new System.Windows.Forms.RadioButton();
            this.customBulbRadioButton = new System.Windows.Forms.RadioButton();
            this.bitmapRadioButton = new System.Windows.Forms.RadioButton();
            this.locusEffectsProvider = new BigMansStuff.LocusEffects.LocusEffectsProvider(this.components);
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.stateTabPage = new System.Windows.Forms.TabPage();
            this.statePanel = new System.Windows.Forms.Panel();
            this.whereButton = new System.Windows.Forms.Button();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.usaMapPanel = new System.Windows.Forms.Panel();
            this.usaMapPictureBox = new System.Windows.Forms.PictureBox();
            this.searchTabPage = new System.Windows.Forms.TabPage();
            this.textBoxPanel = new System.Windows.Forms.Panel();
            this.exampleTextBox = new System.Windows.Forms.RichTextBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.searchForLabel = new System.Windows.Forms.Label();
            this.effectPanel = new System.Windows.Forms.Panel();
            this.customAnimatedArrowRadioButton = new System.Windows.Forms.RadioButton();
            this.animatedImageRadioButton = new System.Windows.Forms.RadioButton();
            this.activeEffectLabel = new System.Windows.Forms.Label();
            this.returnLabel = new System.Windows.Forms.Label();
            this.textRadioButton = new System.Windows.Forms.RadioButton();
            this.customTextRadioButton = new System.Windows.Forms.RadioButton();
            this.tabControlPanel = new System.Windows.Forms.Panel();
            this.mainTabControl.SuspendLayout();
            this.stateTabPage.SuspendLayout();
            this.statePanel.SuspendLayout();
            this.usaMapPanel.SuspendLayout();
            this.searchTabPage.SuspendLayout();
            this.textBoxPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.effectPanel.SuspendLayout();
            this.tabControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // showLocusEffectButton
            // 
            this.showLocusEffectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.showLocusEffectButton.Location = new System.Drawing.Point(8, 351);
            this.showLocusEffectButton.Name = "showLocusEffectButton";
            this.showLocusEffectButton.Size = new System.Drawing.Size(128, 23);
            this.showLocusEffectButton.TabIndex = 16;
            this.showLocusEffectButton.Text = "Show Locus Effect";
            this.showLocusEffectButton.Click += new System.EventHandler(this.showLocusEffectButton_Click);
            // 
            // arrowRadioButton
            // 
            this.arrowRadioButton.Checked = true;
            this.arrowRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.arrowRadioButton.Location = new System.Drawing.Point(40, 48);
            this.arrowRadioButton.Name = "arrowRadioButton";
            this.arrowRadioButton.TabIndex = 2;
            this.arrowRadioButton.TabStop = true;
            this.arrowRadioButton.Text = "Arrow";
            this.arrowRadioButton.CheckedChanged += new System.EventHandler(this.arrowRadioButton_CheckedChanged);
            // 
            // beaconRadioButton
            // 
            this.beaconRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.beaconRadioButton.Location = new System.Drawing.Point(40, 69);
            this.beaconRadioButton.Name = "beaconRadioButton";
            this.beaconRadioButton.TabIndex = 3;
            this.beaconRadioButton.Text = "Radar beacon";
            this.beaconRadioButton.CheckedChanged += new System.EventHandler(this.beaconRadioButton_CheckedChanged);
            // 
            // customArrowRadioButton
            // 
            this.customArrowRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customArrowRadioButton.Location = new System.Drawing.Point(41, 182);
            this.customArrowRadioButton.Name = "customArrowRadioButton";
            this.customArrowRadioButton.Size = new System.Drawing.Size(152, 24);
            this.customArrowRadioButton.TabIndex = 8;
            this.customArrowRadioButton.Text = "Custom arrow (Curved)";
            this.customArrowRadioButton.CheckedChanged += new System.EventHandler(this.customArrowRadioButton_CheckedChanged);
            // 
            // customBeaconRadioButton
            // 
            this.customBeaconRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBeaconRadioButton.Location = new System.Drawing.Point(41, 223);
            this.customBeaconRadioButton.Name = "customBeaconRadioButton";
            this.customBeaconRadioButton.TabIndex = 10;
            this.customBeaconRadioButton.Text = "Shrinking beacon";
            this.customBeaconRadioButton.CheckedChanged += new System.EventHandler(this.customBeaconRadioButton_CheckedChanged);
            // 
            // customBeacon2RadioButton
            // 
            this.customBeacon2RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBeacon2RadioButton.Location = new System.Drawing.Point(41, 243);
            this.customBeacon2RadioButton.Name = "customBeacon2RadioButton";
            this.customBeacon2RadioButton.Size = new System.Drawing.Size(120, 24);
            this.customBeacon2RadioButton.TabIndex = 11;
            this.customBeacon2RadioButton.Text = "Laundry beacon";
            this.customBeacon2RadioButton.CheckedChanged += new System.EventHandler(this.customBeacon2RadioButton_CheckedChanged);
            // 
            // locusArea
            // 
            this.locusArea.BackColor = System.Drawing.SystemColors.Control;
            this.locusArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locusArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.locusArea.Image = ((System.Drawing.Image)(resources.GetObject("locusArea.Image")));
            this.locusArea.Location = new System.Drawing.Point(48, 383);
            this.locusArea.Name = "locusArea";
            this.locusArea.Size = new System.Drawing.Size(96, 96);
            this.locusArea.TabIndex = 14;
            this.locusArea.TabStop = false;
            this.locusArea.Click += new System.EventHandler(this.locusArea_Click);
            // 
            // customBeacon3RadioButton
            // 
            this.customBeacon3RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBeacon3RadioButton.Location = new System.Drawing.Point(41, 263);
            this.customBeacon3RadioButton.Name = "customBeacon3RadioButton";
            this.customBeacon3RadioButton.Size = new System.Drawing.Size(120, 24);
            this.customBeacon3RadioButton.TabIndex = 12;
            this.customBeacon3RadioButton.Text = "Heartbeat beacon";
            this.customBeacon3RadioButton.CheckedChanged += new System.EventHandler(this.customBeacon3RadioButton_CheckedChanged);
            // 
            // predefinedLabel
            // 
            this.predefinedLabel.Location = new System.Drawing.Point(16, 32);
            this.predefinedLabel.Name = "predefinedLabel";
            this.predefinedLabel.Size = new System.Drawing.Size(100, 16);
            this.predefinedLabel.TabIndex = 1;
            this.predefinedLabel.Text = "Predefined:";
            // 
            // customLabel
            // 
            this.customLabel.Location = new System.Drawing.Point(17, 166);
            this.customLabel.Name = "customLabel";
            this.customLabel.Size = new System.Drawing.Size(100, 16);
            this.customLabel.TabIndex = 7;
            this.customLabel.Text = "Custom:";
            // 
            // customArrow2RadioButton
            // 
            this.customArrow2RadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customArrow2RadioButton.Location = new System.Drawing.Point(41, 202);
            this.customArrow2RadioButton.Name = "customArrow2RadioButton";
            this.customArrow2RadioButton.Size = new System.Drawing.Size(160, 24);
            this.customArrow2RadioButton.TabIndex = 9;
            this.customArrow2RadioButton.Text = "Custom arrow (Robin Hood)";
            this.customArrow2RadioButton.CheckedChanged += new System.EventHandler(this.customArrow2RadioButton_CheckedChanged);
            // 
            // customBulbRadioButton
            // 
            this.customBulbRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customBulbRadioButton.Location = new System.Drawing.Point(41, 282);
            this.customBulbRadioButton.Name = "customBulbRadioButton";
            this.customBulbRadioButton.Size = new System.Drawing.Size(120, 24);
            this.customBulbRadioButton.TabIndex = 13;
            this.customBulbRadioButton.Text = "Bulb";
            this.customBulbRadioButton.CheckedChanged += new System.EventHandler(this.customBulbRadioButton_CheckedChanged);
            // 
            // bitmapRadioButton
            // 
            this.bitmapRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bitmapRadioButton.Location = new System.Drawing.Point(40, 90);
            this.bitmapRadioButton.Name = "bitmapRadioButton";
            this.bitmapRadioButton.Size = new System.Drawing.Size(112, 24);
            this.bitmapRadioButton.TabIndex = 4;
            this.bitmapRadioButton.Text = "Bitmap";
            this.bitmapRadioButton.CheckedChanged += new System.EventHandler(this.bitmapRadioButton_CheckedChanged);
            // 
            // locusEffectsProvider
            // 
            this.locusEffectsProvider.FramesPerSecond = 25;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.stateTabPage);
            this.mainTabControl.Controls.Add(this.searchTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(554, 488);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // stateTabPage
            // 
            this.stateTabPage.Controls.Add(this.statePanel);
            this.stateTabPage.Controls.Add(this.usaMapPanel);
            this.stateTabPage.Location = new System.Drawing.Point(4, 22);
            this.stateTabPage.Name = "stateTabPage";
            this.stateTabPage.Size = new System.Drawing.Size(546, 462);
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
            this.statePanel.Size = new System.Drawing.Size(546, 32);
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
            this.usaMapPanel.Size = new System.Drawing.Size(546, 462);
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
            this.usaMapPictureBox.Size = new System.Drawing.Size(540, 427);
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
            this.searchTabPage.Size = new System.Drawing.Size(546, 462);
            this.searchTabPage.TabIndex = 2;
            this.searchTabPage.Text = "Example - Text search";
            // 
            // textBoxPanel
            // 
            this.textBoxPanel.Controls.Add(this.exampleTextBox);
            this.textBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPanel.Location = new System.Drawing.Point(0, 32);
            this.textBoxPanel.Name = "textBoxPanel";
            this.textBoxPanel.Size = new System.Drawing.Size(546, 430);
            this.textBoxPanel.TabIndex = 5;
            // 
            // exampleTextBox
            // 
            this.exampleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exampleTextBox.Location = new System.Drawing.Point(0, 0);
            this.exampleTextBox.Name = "exampleTextBox";
            this.exampleTextBox.Size = new System.Drawing.Size(546, 430);
            this.exampleTextBox.TabIndex = 3;
            this.exampleTextBox.Text = "Guide to the Code Project\nSo what is \'The Code Project\'?\n\nThe Code Project is a c" +
                "ommunity of Visual Studio .NET developers joined together with a common goal: To" +
                " learn, to teach, to have fun programming. Developers from all over the world co" +
                "me together to share source code, tutorials and knowledge for free to help their" +
                " fellow programmers.\n\nIt is our deepest hope that you find CodeProject.com a wea" +
                "lth of information and a valuable resource all we ask is that if you find the Co" +
                "de Project useful then we encourage you to share what source code or knowledge y" +
                "ou can to give back to the community.\n\nAbove and beyond articles and code snippe" +
                "ts CodeProject gives developers a voice. We have over 800,000 people reading Cod" +
                "eProject each month including those from companies such as Microsoft, HP, Develo" +
                "pMentor and Wintellect. CodeProject brings industry and the developer community " +
                "together. CodeProject is proud to be a Microsoft CodeWise community member.\n\nSig" +
                "n up to become a member for free here.\nContent\nArticles\n\n    \tThe Code Project c" +
                "omprises thousands of top quality programming articles and tutorials. The articl" +
                "es contain hands-on information and are created by guys and girls in the same si" +
                "tuations as you. They tackle real world situations to save you valuable time. Hu" +
                "ndreds of new articles are posted each month as more and more developers join to" +
                "gether to share.\n\n    Some quick links for articles:\n\n        * Site map\n       " +
                " * Beginners articles\n        * Most viewed\n        * Most popular\n        * Sub" +
                "mission guidelines\n\nBeginners Tutorials\n\n    For those just starting out we have" +
                " a comprehensive selection of tutorials to get you started.\n\nDiscussion boards\n\n" +
                "    If you\'re looking for some answers, looking to help out fellow programmers o" +
                "r just want to hang out and catch up with everyone then try the Discussion Board" +
                "s\n\nThe Lounge\n\n    Caution: Seriously addictive. This is for the exclusive use o" +
                "f CodeProject members and gives them a chance to kick back and socialise. If you" +
                " want to know what\'s happening at CodeProject then this is the place. Please - m" +
                "ake yourself at home, and dress rules do apply.\n\nThe Newsletter\n\n    Each week w" +
                "e send out our weekly newsletter covering the latest articles, site news and vie" +
                "ws and lists of the most popular articles from the previous week. As a CodeProje" +
                "ct member you can customise your newsletter so that of hundreds of new articles " +
                "posted monthly, you only need to see the ones that interest you.\n\nCommunity Surv" +
                "eys\n\n    Ever wondered what everyone else is thinking? Our weekly polls take a q" +
                "uick peek into the thoughts of developers\n\nIndustry News\n\n    Check out the News" +
                " archives to keep up with the latest going\'s on in the industry.\n\nIndustry Conta" +
                "cts\n\n    If you need to get in touch with a company but are unable to find their" +
                " details then check our list of industry contacts contains information on hundre" +
                "ds of companies to help you stay in touch. We also regularly interview those in " +
                "the industry to get their thoughts and views.\n\nCodeProject Stuff\n\n    Looking fo" +
                "r CodeProject icons and graphics, PocketPC themes, wallpaper or even a WAP feed?" +
                " Then look no further.\n\nBeing a member of the Code Project community.\n\nThe Code " +
                "Project community come from all countries and all walks of life. Students, compa" +
                "ny CEOs and developers from Australia to Yugoslavia. If you\'re interested in som" +
                "e demographics then try this page.\n\nWhen you join Code Project you can:\n\n    * G" +
                "et your own page to let others know what you are up to, your interests, where yo" +
                "u live and what you look like. Readers can also get a listing of all your articl" +
                "es posted.\n    * Post articles, and vote on other author\'s articles\n    * Partic" +
                "ipate in The Lounge. This is an area exclusively for CodeProject members to get " +
                "together and ask advice, tell bad jokes and keep up with the latest industry gos" +
                "sip.\n    * Bookmark your favourite articles\n    * Subscribe to the free weekly n" +
                "ewsletter to keep up with the latest articles posted on CodeProject. The newslet" +
                "ter is fully customisable so you only read about the articles you are interested" +
                " in.\n    * Have your signature, name and contact details filled in automatically" +
                " when posting to the discussion boards. It doesn\'t sound like much but it saves " +
                "a ton of typing!\n    * Reserve your name. Once your name has been taken no one e" +
                "lse can pose as you on the discussion boards. Grab your name now before someone " +
                "else grabs it first!\n\nYou can sign up to become a member for free here.";
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.findButton);
            this.searchPanel.Controls.Add(this.searchForLabel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(546, 32);
            this.searchPanel.TabIndex = 4;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(120, 8);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(136, 20);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.Text = "CodeProject";
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
            // searchForLabel
            // 
            this.searchForLabel.Location = new System.Drawing.Point(8, 8);
            this.searchForLabel.Name = "searchForLabel";
            this.searchForLabel.TabIndex = 0;
            this.searchForLabel.Text = "&Search for:";
            // 
            // effectPanel
            // 
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
            this.effectPanel.Size = new System.Drawing.Size(224, 488);
            this.effectPanel.TabIndex = 0;
            // 
            // customAnimatedArrowRadioButton
            // 
            this.customAnimatedArrowRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customAnimatedArrowRadioButton.Location = new System.Drawing.Point(41, 323);
            this.customAnimatedArrowRadioButton.Name = "customAnimatedArrowRadioButton";
            this.customAnimatedArrowRadioButton.Size = new System.Drawing.Size(176, 24);
            this.customAnimatedArrowRadioButton.TabIndex = 15;
            this.customAnimatedArrowRadioButton.Text = "Custom animated arrow";
            this.customAnimatedArrowRadioButton.CheckedChanged += new System.EventHandler(this.customAnimatedArrowRadioButton_CheckedChanged);
            // 
            // animatedImageRadioButton
            // 
            this.animatedImageRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.animatedImageRadioButton.Location = new System.Drawing.Point(40, 133);
            this.animatedImageRadioButton.Name = "animatedImageRadioButton";
            this.animatedImageRadioButton.Size = new System.Drawing.Size(112, 24);
            this.animatedImageRadioButton.TabIndex = 6;
            this.animatedImageRadioButton.Text = "Animated Image";
            this.animatedImageRadioButton.CheckedChanged += new System.EventHandler(this.animatedImageRadioButton_CheckedChanged);
            // 
            // activeEffectLabel
            // 
            this.activeEffectLabel.Location = new System.Drawing.Point(8, 8);
            this.activeEffectLabel.Name = "activeEffectLabel";
            this.activeEffectLabel.Size = new System.Drawing.Size(144, 23);
            this.activeEffectLabel.TabIndex = 0;
            this.activeEffectLabel.Text = "Select a Locus Effect:";
            // 
            // returnLabel
            // 
            this.returnLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.returnLabel.Location = new System.Drawing.Point(144, 359);
            this.returnLabel.Name = "returnLabel";
            this.returnLabel.Size = new System.Drawing.Size(70, 16);
            this.returnLabel.TabIndex = 17;
            this.returnLabel.Text = "(or hit F12)";
            // 
            // textRadioButton
            // 
            this.textRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.textRadioButton.Location = new System.Drawing.Point(40, 111);
            this.textRadioButton.Name = "textRadioButton";
            this.textRadioButton.Size = new System.Drawing.Size(112, 24);
            this.textRadioButton.TabIndex = 5;
            this.textRadioButton.Text = "Text";
            this.textRadioButton.CheckedChanged += new System.EventHandler(this.textRadioButton_CheckedChanged);
            // 
            // customTextRadioButton
            // 
            this.customTextRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.customTextRadioButton.Location = new System.Drawing.Point(41, 302);
            this.customTextRadioButton.Name = "customTextRadioButton";
            this.customTextRadioButton.Size = new System.Drawing.Size(176, 24);
            this.customTextRadioButton.TabIndex = 14;
            this.customTextRadioButton.Text = "Full Screen Text (Center Monitor)";
            this.customTextRadioButton.CheckedChanged += new System.EventHandler(this.customTextRadioButton_CheckedChanged);
            // 
            // tabControlPanel
            // 
            this.tabControlPanel.Controls.Add(this.mainTabControl);
            this.tabControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel.Location = new System.Drawing.Point(224, 0);
            this.tabControlPanel.Name = "tabControlPanel";
            this.tabControlPanel.Size = new System.Drawing.Size(554, 488);
            this.tabControlPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScale = false;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(778, 488);
            this.Controls.Add(this.tabControlPanel);
            this.Controls.Add(this.effectPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "LocusEffects Tester";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.mainTabControl.ResumeLayout(false);
            this.stateTabPage.ResumeLayout(false);
            this.statePanel.ResumeLayout(false);
            this.usaMapPanel.ResumeLayout(false);
            this.searchTabPage.ResumeLayout(false);
            this.textBoxPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.effectPanel.ResumeLayout(false);
            this.tabControlPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion
		#endregion

		#region Private Event Handlers
        
        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void MainForm_Load(object sender, System.EventArgs e)
		{
            // Initialize LocusEffects
			this.InitializeLocusEffects() ;

            // Initialize map example
            this.InitializeMapExample() ;
		}

        /// <summary>
        /// Handles the Click event of the showLocusEffectButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void showLocusEffectButton_Click(object sender, System.EventArgs e)
		{
			System.Drawing.Rectangle locusRect = locusArea.Parent.RectangleToScreen( locusArea.Bounds );

            // Show the selected locus effect
			locusEffectsProvider.ShowLocusEffect( this, locusRect, m_activeLocusEffectName ) ;
		}

        /// <summary>
        /// Handles the KeyUp event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		private void MainForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.F12 )
			{
				showLocusEffectButton_Click( this, System.EventArgs.Empty ) ;
			}		
		}

        /// <summary>
        /// Handles the Click event of the findButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void findButton_Click(object sender, System.EventArgs e)
		{
			int nextMatchIndex = exampleTextBox.Text.IndexOf( searchTextBox.Text, exampleTextBox.SelectionStart + 1 ) ;

            if ( nextMatchIndex != -1 )
            {
                this.ShowEffectOnTextPosition( nextMatchIndex ) ;
            }
            else
            {
                locusEffectsProvider.StopActiveLocusEffect() ;
                exampleTextBox.Focus() ;
                exampleTextBox.SelectionLength = 0 ;
                exampleTextBox.SelectionStart = 0 ;
                exampleTextBox.ScrollToCaret() ;

                nextMatchIndex = exampleTextBox.Text.IndexOf( searchTextBox.Text, exampleTextBox.SelectionStart + 1 ) ;
                if ( nextMatchIndex != -1 )
                {
                    this.ShowEffectOnTextPosition( nextMatchIndex ) ;
                }
            }
		}

        /// <summary>
        /// Handles the CheckedChanged event of the arrowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void arrowRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectArrow ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the beaconRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void beaconRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectBeacon ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the bitmapRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void bitmapRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectBitmap ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the customArrowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void customArrowRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = m_customArrowLocusEffect.Name ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the customArrow2RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void customArrow2RadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = m_customArrowLocusEffect2.Name ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the customBeaconRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void customBeaconRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = m_customBeaconLocusEffect.Name ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the customBeacon2RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void customBeacon2RadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = m_customBeaconLocusEffect2.Name ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the customBeacon3RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void customBeacon3RadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = m_customBeaconLocusEffect3.Name ;
		}

        /// <summary>
        /// Handles the CheckedChanged event of the customBulbRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void customBulbRadioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			m_activeLocusEffectName = m_customBulbEffect.Name ;		
		}

        /// <summary>
        /// Handles the CheckedChanged event of the animatedImageRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void animatedImageRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectAnimatedImage ;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customAnimatedArrowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customAnimatedArrowRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            m_activeLocusEffectName = m_customAnimatedImageEffect.Name ;      
        }
        
        /// <summary>
        /// Handles the CheckedChanged event of the textRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectText ;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the customTextRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void customTextRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            m_activeLocusEffectName = m_fullScreenTextEffect.Name ;		       
        }

        /// <summary>
        /// Handles the Click event of the locusArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void locusArea_Click(object sender, System.EventArgs e)
		{
			// Get assembly version
			Assembly assembly = Assembly.GetAssembly( typeof( LocusEffectsProvider ) ) ;
			System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo( assembly.Location ) ;

			string assemblyVersion = string.Format( "{0}.{1}.{2}.{3}", 
				fileVersionInfo.FileMajorPart, 
				fileVersionInfo.FileMinorPart,
				fileVersionInfo.FileBuildPart,
				fileVersionInfo.FilePrivatePart ) ;

			// Show about dialog
			MessageBox.Show( this, 
				"© Copyright 2005, BigMan's Stuff - Yuval Naveh" + "\r\n\r\n" +
				"LocusEffects version: " + assemblyVersion, 
				"LocusEffects",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information ) ;
		}

        
        /// <summary>
        /// Handles the Click event of the whereButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void whereButton_Click(object sender, System.EventArgs e)
        {
            // -- Show state on map --
            USAState state = stateComboBox.SelectedItem as USAState ;

            Point stateClientPoint = state.StateCoordinate ;
            stateClientPoint.X = ( int ) ( ( float ) stateClientPoint.X * usaMapPictureBox.Width / usaMapPictureBox.Image.Width ) ;
            stateClientPoint.Y = ( int ) ( ( float ) stateClientPoint.Y * usaMapPictureBox.Height / usaMapPictureBox.Image.Height ) ;

            Point stateScreenPoint = usaMapPictureBox.PointToScreen( stateClientPoint ) ;

            // Show the selected locus effect
            locusEffectsProvider.ShowLocusEffect( this, stateScreenPoint, m_activeLocusEffectName ) ;
        }

        /// <summary>
        /// Handles the Resize event of the usaMapPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void usaMapPictureBox_Resize(object sender, System.EventArgs e)
        {
            if ( locusEffectsProvider.IsAnimating )
            {
                locusEffectsProvider.StopActiveLocusEffect() ;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the mainTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mainTabControl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if ( locusEffectsProvider.IsAnimating )
            {
                locusEffectsProvider.StopActiveLocusEffect() ;
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
			locusEffectsProvider.Initialize() ;
   
            // Create custom Locus Effects
			this.CreateCustomLocusEffects();
		}

        /// <summary>
        /// Creates and registers custom locus effects (Demo effects)
        /// </summary>
		private void CreateCustomLocusEffects()
		{
			ResourceManager rm = new ResourceManager( "BigMansStuff.TestLocusEffects.Images.CustomImages", Assembly.GetExecutingAssembly() ) ;

			#region Custom Arrow - Curved
			m_customArrowLocusEffect = new ArrowLocusEffect() ;
			m_customArrowLocusEffect.Name = "CustomArrow_Curved" ;
			m_customArrowLocusEffect.AnimationStartColor = Color.Red ;
			m_customArrowLocusEffect.AnimationEndColor = Color.Yellow ;
			m_customArrowLocusEffect.Bitmap = rm.GetObject( "CustomCurvedArrowBitmap" ) as Bitmap ;

			locusEffectsProvider.AddLocusEffect( m_customArrowLocusEffect ) ;

			#endregion

			#region Custom Arrow - Robin Hood
			m_customArrowLocusEffect2 = new ArrowLocusEffect() ;
			m_customArrowLocusEffect2.Name = "CustomArrow_RobinHood" ;
			m_customArrowLocusEffect2.AnimationStartColor = Color.Green ;
			m_customArrowLocusEffect2.AnimationEndColor = Color.FromArgb( 40, 20, 10 ) ;
			m_customArrowLocusEffect2.Bitmap = rm.GetObject( "CustomRobinHoodArrowBitmap" ) as Bitmap ;
			m_customArrowLocusEffect2.ShowShadow = false ;
            m_customArrowLocusEffect2.MovementMode = MovementMode.OneWayAlongVector ;
            m_customArrowLocusEffect2.MovementCycles = 4 ;
            m_customArrowLocusEffect2.MovementAmplitude = 50 ;
            m_customArrowLocusEffect2.MovementVectorAngle = 45 ; //degrees
            m_customArrowLocusEffect2.LeadInTime = 0 ; //msec
            m_customArrowLocusEffect2.AnimationTime = 2000 ; //msec

			locusEffectsProvider.AddLocusEffect( m_customArrowLocusEffect2 ) ;
			#endregion
			
			#region Custom Beacon - Shrinking

			m_customBeaconLocusEffect = new BeaconLocusEffect() ;
			m_customBeaconLocusEffect.Name = "CustomBeacon_Shrinking" ;
			m_customBeaconLocusEffect.InitialSize = new Size( 100, 100 ) ;
			m_customBeaconLocusEffect.AnimationTime = 1000 ;
			m_customBeaconLocusEffect.AnimationStartColor = Color.LightBlue ;
			m_customBeaconLocusEffect.AnimationEndColor = Color.LightBlue ;
			m_customBeaconLocusEffect.AnimationOuterColor = Color.BlueViolet ;
			m_customBeaconLocusEffect.BrokenRing = true ;
			m_customBeaconLocusEffect.RingWidth = 6 ;
			m_customBeaconLocusEffect.OuterRingWidth = 3 ;
			m_customBeaconLocusEffect.Rotate = true ;
			m_customBeaconLocusEffect.RotatationSpeed = 90 ;
			m_customBeaconLocusEffect.ShowShadow = true ;

			locusEffectsProvider.AddLocusEffect( m_customBeaconLocusEffect ) ;
			#endregion

			#region Custom Beacon - Laundry
			m_customBeaconLocusEffect2 = new BeaconLocusEffect() ;
			m_customBeaconLocusEffect2.Name = "CustomBeacon2" ;
			m_customBeaconLocusEffect2.InitialSize = new Size( 100, 100 ) ;
			m_customBeaconLocusEffect2.AnimationTime = 2000 ;
			m_customBeaconLocusEffect2.LeadOutTime = 0 ;
			m_customBeaconLocusEffect2.AnimationStartColor = Color.Green ;
			m_customBeaconLocusEffect2.AnimationEndColor = Color.DarkGreen ;
			m_customBeaconLocusEffect2.AnimationOuterColor = Color.LightGreen ;
			m_customBeaconLocusEffect2.BrokenRing = true ;
			m_customBeaconLocusEffect2.RingWidth = 6 ;
			m_customBeaconLocusEffect2.OuterRingWidth = 3 ;
			m_customBeaconLocusEffect2.Rotate = true ;
			m_customBeaconLocusEffect2.RotatationSpeed = 180 ;
			m_customBeaconLocusEffect2.RotateLaundry = true ;
			m_customBeaconLocusEffect2.Style = BeaconEffectStyles.None ;
			m_customBeaconLocusEffect2.ShowShadow = true ;

			locusEffectsProvider.AddLocusEffect( m_customBeaconLocusEffect2 ) ;
			#endregion

			#region Beacon - heart beat
			m_customBeaconLocusEffect3 = new BeaconLocusEffect() ;
			m_customBeaconLocusEffect3.Name = "CustomBeacon3" ;
			m_customBeaconLocusEffect3.InitialSize = new Size( 100, 100 ) ;
			m_customBeaconLocusEffect3.AnimationTime = 2500 ;
			m_customBeaconLocusEffect3.LeadInTime = 0 ;
			m_customBeaconLocusEffect3.LeadOutTime = 0 ;
			m_customBeaconLocusEffect3.AnimationStartColor = Color.HotPink ;
			m_customBeaconLocusEffect3.AnimationEndColor = Color.HotPink ;
			m_customBeaconLocusEffect3.AnimationOuterColor = Color.Pink ;
			m_customBeaconLocusEffect3.RingWidth = 6 ;
			m_customBeaconLocusEffect3.OuterRingWidth = 3 ;
			m_customBeaconLocusEffect3.BodyFadeOut = true ;
			m_customBeaconLocusEffect3.Style = BeaconEffectStyles.HeartBeat ;

			locusEffectsProvider.AddLocusEffect( m_customBeaconLocusEffect3 ) ;
			#endregion

			#region Bulb
			m_customBulbEffect = new BitmapLocusEffect() ;
			m_customBulbEffect.Name = "CustomArrow_Bulb" ;
			m_customBulbEffect.AnimationStartColor = Color.DarkGray ;
			m_customBulbEffect.AnimationEndColor = Color.Yellow ;
            m_customBulbEffect.AnimationTime = 1000 ; // msec
            m_customBulbEffect.Bitmap = rm.GetObject( "CustomBulbBitmap" ) as Bitmap ;
			m_customBulbEffect.ShadowOpacity = 40 ; // %
			m_customBulbEffect.ShadowOffset = new Point( 1, 1 ) ; // %
			m_customBulbEffect.AnchoringMode = AnchoringMode.CenterOffset ;
			m_customBulbEffect.AnchoringOffset = new Point( 0, -locusArea.Height ) ;
            m_customBulbEffect.MovementMode = MovementMode.Buzz ;
            m_customBulbEffect.MovementAmplitude = 10 ;

			locusEffectsProvider.AddLocusEffect( m_customBulbEffect ) ;
			#endregion
		
            #region Full Screen Text
            m_fullScreenTextEffect = new TextLocusEffect() ;
            m_fullScreenTextEffect.Name = "FullScreenText" ;
            m_fullScreenTextEffect.AnimationStartColor = Color.DarkRed ;
            m_fullScreenTextEffect.AnimationEndColor = Color.Red ;
            m_fullScreenTextEffect.AnchoringMode = AnchoringMode.CenterMonitor ;
            m_fullScreenTextEffect.Text = "Operation\r\nCancelled!" ;
            m_fullScreenTextEffect.Font = new Font( "Arial", 80 ) ;
            m_fullScreenTextEffect.RotationAngle = -45 ;

            locusEffectsProvider.AddLocusEffect( m_fullScreenTextEffect ) ;
            #endregion

            #region Custom animated image
            m_customAnimatedImageEffect = new AnimatedImageLocusEffect() ;
            m_customAnimatedImageEffect.Name = "CustomAnimatedImage" ;
            m_customAnimatedImageEffect.AddImageFrames( rm.GetObject( "AnimatedArrow" ) as Image ) ;
            m_customAnimatedImageEffect.FocusPoint = new Point( -30, 0 ) ;
            m_customAnimatedImageEffect.AnimationLoops = 1 ;

            locusEffectsProvider.AddLocusEffect( m_customAnimatedImageEffect ) ;
            #endregion
        }

        /// <summary>
        /// Initializes the map example.
        /// </summary>
        public void InitializeMapExample()
        {
            // I only added some states..its only a demo..
            // Coordinates where measured by hand using a simple bitmap editor
            stateComboBox.Items.Add( new USAState( "NY", "New York", new Point( 453, 170 ) ) ) ;
            stateComboBox.Items.Add( new USAState( "CA", "California", new Point( 140, 222 ) ) ) ;
            stateComboBox.Items.Add( new USAState( "TX", "Texas", new Point( 290, 292 ) ) ) ;
            stateComboBox.Items.Add( new USAState( "AK", "Alaska", new Point( 113, 46 ) ) ) ;
            stateComboBox.Items.Add( new USAState( "GA", "Georgia", new Point( 414, 272 ) ) ) ;
            stateComboBox.Items.Add( new USAState( "WY", "Wyoming", new Point( 234, 182 ) ) ) ;

            // Select first state
            stateComboBox.SelectedIndex = 0 ;
        }
        
        /// <summary>
        /// Shows the effect on a given text position.
        /// </summary>
        /// <param name="nextMatchIndex">Index of the next match.</param>
        private void ShowEffectOnTextPosition( int nextMatchIndex )
        {
            exampleTextBox.Focus() ;
            exampleTextBox.SelectionLength = 0 ;
            exampleTextBox.SelectionStart = nextMatchIndex ;
        
            exampleTextBox.ScrollToCaret() ;
        
            Point textPoint = Point.Empty ;
            GetCaretPos( ref textPoint ) ;
        
            Point locusPoint = exampleTextBox.PointToScreen( textPoint ) ;
            locusPoint.X += 1 ; // dirty but this is only a demo..
            locusPoint.Y += 6 ; // dirty but this is only a demo..
        
            locusEffectsProvider.ShowLocusEffect( this, locusPoint, m_activeLocusEffectName ) ;
        }


		[System.Runtime.InteropServices.DllImport( "user32" ) ]
		private static extern int GetCaretPos( ref Point lpPoint ) ;

		#endregion

		#region Private Members
		private ArrowLocusEffect m_customArrowLocusEffect = null ;
		private ArrowLocusEffect m_customArrowLocusEffect2 = null ;
		private BeaconLocusEffect m_customBeaconLocusEffect = null ;
		private BeaconLocusEffect m_customBeaconLocusEffect2 = null ;
		private BeaconLocusEffect m_customBeaconLocusEffect3 = null ;
		private BitmapLocusEffect m_customBulbEffect = null ;
        private TextLocusEffect m_fullScreenTextEffect = null ;
        private AnimatedImageLocusEffect m_customAnimatedImageEffect = null ;
        private string m_activeLocusEffectName = LocusEffectsProvider.DefaultLocusEffectArrow ;

		#endregion
	}

    /// <summary>
    /// Class for holding a USA State informationon the map
    /// </summary>
    internal class USAState
    {
        public USAState( string stateSymbol, string stateName, Point stateCoordinate )
        {
            StateSymbol = stateSymbol ;
            StateName = stateName ;
            StateCoordinate = stateCoordinate ;
        }

        public string StateSymbol ;
        public string StateName ;
        public Point StateCoordinate ;

        public override string ToString()
        {
            return StateName + " (" + StateSymbol + ")" ;
        }

    }
}
