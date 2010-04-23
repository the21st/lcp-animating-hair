namespace AnimatingHair
{
    partial class ControlsWindow
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
            if ( disposing && (components != null) )
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
            this.glControl = new OpenTK.GLControl();
            this.restartGroupBox = new System.Windows.Forms.GroupBox();
            this.textBoxMaxRootDepth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarMaxRootDepth = new System.Windows.Forms.TrackBar();
            this.textBoxHairLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarHairLength = new System.Windows.Forms.TrackBar();
            this.textBoxNumberOfParticles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarNumberOfParticles = new System.Windows.Forms.TrackBar();
            this.buttonRestartWithRandomSeed = new System.Windows.Forms.Button();
            this.numericUpDownSeed = new System.Windows.Forms.NumericUpDown();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fpsDisplayTimer = new System.Windows.Forms.Timer( this.components );
            this.FPSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.textBoxCollisionDamping = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBarCollisionDamping = new System.Windows.Forms.TrackBar();
            this.textBoxAverageDensity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.trackBarAverageDensity = new System.Windows.Forms.TrackBar();
            this.textBoxAttractionRepulsion = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.trackBarAttractionRepulsion = new System.Windows.Forms.TrackBar();
            this.textBoxFrictionDamping = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.trackBarFrictionDamping = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonColor = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxNumberOfAirParticles = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.trackBarNumberOfAirParticles = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBarGravity = new System.Windows.Forms.TrackBar();
            this.textBoxGravity = new System.Windows.Forms.TextBox();
            this.trackBarAirFriction = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAirFriction = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxAverageDensityAir = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDrag = new System.Windows.Forms.TextBox();
            this.trackBarAverageDensityAir = new System.Windows.Forms.TrackBar();
            this.trackBarAttractionRepulsionAir = new System.Windows.Forms.TrackBar();
            this.textBoxAttractionRepulsionAir = new System.Windows.Forms.TextBox();
            this.trackBarDrag = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.propertyGridRenderer = new System.Windows.Forms.PropertyGrid();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.trackBarLightIntensity = new System.Windows.Forms.TrackBar();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.diffuseDialog = new System.Windows.Forms.ColorDialog();
            this.ambientDialog = new System.Windows.Forms.ColorDialog();
            this.specularDialog = new System.Windows.Forms.ColorDialog();
            this.restartGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxRootDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHairLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfParticles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCollisionDamping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAverageDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAttractionRepulsion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFrictionDamping)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfAirParticles)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGravity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAirFriction)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAverageDensityAir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAttractionRepulsionAir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDrag)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightIntensity)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Location = new System.Drawing.Point( 12, 36 );
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size( 800, 600 );
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler( this.glControl_MouseMove );
            // 
            // restartGroupBox
            // 
            this.restartGroupBox.Controls.Add( this.textBoxMaxRootDepth );
            this.restartGroupBox.Controls.Add( this.label4 );
            this.restartGroupBox.Controls.Add( this.trackBarMaxRootDepth );
            this.restartGroupBox.Controls.Add( this.textBoxHairLength );
            this.restartGroupBox.Controls.Add( this.label3 );
            this.restartGroupBox.Controls.Add( this.trackBarHairLength );
            this.restartGroupBox.Controls.Add( this.textBoxNumberOfParticles );
            this.restartGroupBox.Controls.Add( this.label2 );
            this.restartGroupBox.Controls.Add( this.trackBarNumberOfParticles );
            this.restartGroupBox.Location = new System.Drawing.Point( 6, 82 );
            this.restartGroupBox.Name = "restartGroupBox";
            this.restartGroupBox.Size = new System.Drawing.Size( 291, 113 );
            this.restartGroupBox.TabIndex = 1;
            this.restartGroupBox.TabStop = false;
            this.restartGroupBox.Text = "Hair";
            // 
            // textBoxMaxRootDepth
            // 
            this.textBoxMaxRootDepth.Enabled = false;
            this.textBoxMaxRootDepth.Location = new System.Drawing.Point( 240, 78 );
            this.textBoxMaxRootDepth.Name = "textBoxMaxRootDepth";
            this.textBoxMaxRootDepth.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxMaxRootDepth.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 6, 78 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 78, 13 );
            this.label4.TabIndex = 10;
            this.label4.Text = "Max root depth";
            // 
            // trackBarMaxRootDepth
            // 
            this.trackBarMaxRootDepth.AutoSize = false;
            this.trackBarMaxRootDepth.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarMaxRootDepth.Location = new System.Drawing.Point( 80, 78 );
            this.trackBarMaxRootDepth.Maximum = 154;
            this.trackBarMaxRootDepth.Name = "trackBarMaxRootDepth";
            this.trackBarMaxRootDepth.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarMaxRootDepth.TabIndex = 9;
            this.trackBarMaxRootDepth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxRootDepth.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // textBoxHairLength
            // 
            this.textBoxHairLength.Enabled = false;
            this.textBoxHairLength.Location = new System.Drawing.Point( 240, 52 );
            this.textBoxHairLength.Name = "textBoxHairLength";
            this.textBoxHairLength.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxHairLength.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 6, 52 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 58, 13 );
            this.label3.TabIndex = 7;
            this.label3.Text = "Hair length";
            // 
            // trackBarHairLength
            // 
            this.trackBarHairLength.AutoSize = false;
            this.trackBarHairLength.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarHairLength.Location = new System.Drawing.Point( 80, 52 );
            this.trackBarHairLength.Maximum = 154;
            this.trackBarHairLength.Name = "trackBarHairLength";
            this.trackBarHairLength.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarHairLength.TabIndex = 6;
            this.trackBarHairLength.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarHairLength.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // textBoxNumberOfParticles
            // 
            this.textBoxNumberOfParticles.Enabled = false;
            this.textBoxNumberOfParticles.Location = new System.Drawing.Point( 240, 26 );
            this.textBoxNumberOfParticles.Name = "textBoxNumberOfParticles";
            this.textBoxNumberOfParticles.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxNumberOfParticles.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 6, 26 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 68, 13 );
            this.label2.TabIndex = 4;
            this.label2.Text = "# of particles";
            // 
            // trackBarNumberOfParticles
            // 
            this.trackBarNumberOfParticles.AutoSize = false;
            this.trackBarNumberOfParticles.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarNumberOfParticles.LargeChange = 30;
            this.trackBarNumberOfParticles.Location = new System.Drawing.Point( 80, 26 );
            this.trackBarNumberOfParticles.Maximum = 154;
            this.trackBarNumberOfParticles.Name = "trackBarNumberOfParticles";
            this.trackBarNumberOfParticles.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarNumberOfParticles.SmallChange = 5;
            this.trackBarNumberOfParticles.TabIndex = 3;
            this.trackBarNumberOfParticles.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarNumberOfParticles.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // buttonRestartWithRandomSeed
            // 
            this.buttonRestartWithRandomSeed.Location = new System.Drawing.Point( 143, 412 );
            this.buttonRestartWithRandomSeed.Name = "buttonRestartWithRandomSeed";
            this.buttonRestartWithRandomSeed.Size = new System.Drawing.Size( 149, 30 );
            this.buttonRestartWithRandomSeed.TabIndex = 21;
            this.buttonRestartWithRandomSeed.Text = "Restart with random seed";
            this.buttonRestartWithRandomSeed.UseVisualStyleBackColor = true;
            this.buttonRestartWithRandomSeed.Click += new System.EventHandler( this.buttonRestartWithRandomSeed_Click );
            // 
            // numericUpDownSeed
            // 
            this.numericUpDownSeed.Location = new System.Drawing.Point( 81, 42 );
            this.numericUpDownSeed.Maximum = new decimal( new int[] {
            1000,
            0,
            0,
            0} );
            this.numericUpDownSeed.Name = "numericUpDownSeed";
            this.numericUpDownSeed.Size = new System.Drawing.Size( 75, 20 );
            this.numericUpDownSeed.TabIndex = 19;
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point( 6, 412 );
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size( 125, 30 );
            this.buttonRestart.TabIndex = 18;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler( this.button1_Click );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 20, 44 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 32, 13 );
            this.label1.TabIndex = 1;
            this.label1.Text = "Seed";
            // 
            // fpsDisplayTimer
            // 
            this.fpsDisplayTimer.Interval = 1000;
            this.fpsDisplayTimer.Tick += new System.EventHandler( this.fpsDisplayTimer_Tick );
            // 
            // FPSLabel
            // 
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size( 38, 17 );
            this.FPSLabel.Text = "FPS: 0";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.FPSLabel} );
            this.statusStrip1.Location = new System.Drawing.Point( 0, 639 );
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size( 1141, 22 );
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // textBoxCollisionDamping
            // 
            this.textBoxCollisionDamping.Enabled = false;
            this.textBoxCollisionDamping.Location = new System.Drawing.Point( 240, 25 );
            this.textBoxCollisionDamping.Name = "textBoxCollisionDamping";
            this.textBoxCollisionDamping.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxCollisionDamping.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point( 6, 25 );
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size( 74, 13 );
            this.label9.TabIndex = 16;
            this.label9.Text = "Collision damp";
            // 
            // trackBarCollisionDamping
            // 
            this.trackBarCollisionDamping.AutoSize = false;
            this.trackBarCollisionDamping.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarCollisionDamping.Location = new System.Drawing.Point( 80, 25 );
            this.trackBarCollisionDamping.Maximum = 154;
            this.trackBarCollisionDamping.Name = "trackBarCollisionDamping";
            this.trackBarCollisionDamping.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarCollisionDamping.TabIndex = 15;
            this.trackBarCollisionDamping.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarCollisionDamping.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // textBoxAverageDensity
            // 
            this.textBoxAverageDensity.Enabled = false;
            this.textBoxAverageDensity.Location = new System.Drawing.Point( 240, 132 );
            this.textBoxAverageDensity.Name = "textBoxAverageDensity";
            this.textBoxAverageDensity.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxAverageDensity.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point( 6, 116 );
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size( 115, 13 );
            this.label11.TabIndex = 25;
            this.label11.Text = "Average density of hair";
            // 
            // trackBarAverageDensity
            // 
            this.trackBarAverageDensity.AutoSize = false;
            this.trackBarAverageDensity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarAverageDensity.Location = new System.Drawing.Point( 80, 132 );
            this.trackBarAverageDensity.Maximum = 154;
            this.trackBarAverageDensity.Name = "trackBarAverageDensity";
            this.trackBarAverageDensity.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarAverageDensity.TabIndex = 24;
            this.trackBarAverageDensity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAverageDensity.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // textBoxAttractionRepulsion
            // 
            this.textBoxAttractionRepulsion.Enabled = false;
            this.textBoxAttractionRepulsion.Location = new System.Drawing.Point( 240, 93 );
            this.textBoxAttractionRepulsion.Name = "textBoxAttractionRepulsion";
            this.textBoxAttractionRepulsion.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxAttractionRepulsion.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point( 6, 77 );
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size( 149, 13 );
            this.label12.TabIndex = 22;
            this.label12.Text = "Attraction-repulsion magnitude";
            // 
            // trackBarAttractionRepulsion
            // 
            this.trackBarAttractionRepulsion.AutoSize = false;
            this.trackBarAttractionRepulsion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarAttractionRepulsion.Location = new System.Drawing.Point( 80, 93 );
            this.trackBarAttractionRepulsion.Maximum = 154;
            this.trackBarAttractionRepulsion.Name = "trackBarAttractionRepulsion";
            this.trackBarAttractionRepulsion.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarAttractionRepulsion.TabIndex = 21;
            this.trackBarAttractionRepulsion.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAttractionRepulsion.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // textBoxFrictionDamping
            // 
            this.textBoxFrictionDamping.Enabled = false;
            this.textBoxFrictionDamping.Location = new System.Drawing.Point( 240, 51 );
            this.textBoxFrictionDamping.Name = "textBoxFrictionDamping";
            this.textBoxFrictionDamping.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxFrictionDamping.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point( 6, 51 );
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size( 70, 13 );
            this.label13.TabIndex = 19;
            this.label13.Text = "Friction damp";
            // 
            // trackBarFrictionDamping
            // 
            this.trackBarFrictionDamping.AutoSize = false;
            this.trackBarFrictionDamping.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarFrictionDamping.Location = new System.Drawing.Point( 80, 51 );
            this.trackBarFrictionDamping.Maximum = 154;
            this.trackBarFrictionDamping.Name = "trackBarFrictionDamping";
            this.trackBarFrictionDamping.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarFrictionDamping.TabIndex = 18;
            this.trackBarFrictionDamping.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarFrictionDamping.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add( this.buttonColor );
            this.groupBox2.Controls.Add( this.textBoxAverageDensity );
            this.groupBox2.Controls.Add( this.label11 );
            this.groupBox2.Controls.Add( this.trackBarCollisionDamping );
            this.groupBox2.Controls.Add( this.trackBarAverageDensity );
            this.groupBox2.Controls.Add( this.label9 );
            this.groupBox2.Controls.Add( this.textBoxAttractionRepulsion );
            this.groupBox2.Controls.Add( this.textBoxCollisionDamping );
            this.groupBox2.Controls.Add( this.label12 );
            this.groupBox2.Controls.Add( this.trackBarFrictionDamping );
            this.groupBox2.Controls.Add( this.trackBarAttractionRepulsion );
            this.groupBox2.Controls.Add( this.label13 );
            this.groupBox2.Controls.Add( this.textBoxFrictionDamping );
            this.groupBox2.Location = new System.Drawing.Point( 6, 6 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 291, 252 );
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hair properties";
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point( 60, 175 );
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size( 151, 39 );
            this.buttonColor.TabIndex = 27;
            this.buttonColor.Text = "Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler( this.buttonColor_Click );
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point( 818, 583 );
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size( 91, 53 );
            this.buttonPause.TabIndex = 31;
            this.buttonPause.Text = "PAUSE SIMULATION";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler( this.buttonPause_Click );
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.fileToolStripMenuItem} );
            this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size( 1141, 24 );
            this.menuStrip1.TabIndex = 32;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem} );
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size( 37, 20 );
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size( 92, 22 );
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler( this.exitToolStripMenuItem_Click );
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.saveConfigurationToolStripMenuItem,
            this.loadConfigurationToolStripMenuItem,
            this.loadToolStripMenuItem} );
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size( 93, 20 );
            this.fileToolStripMenuItem.Text = "Configuration";
            // 
            // saveConfigurationToolStripMenuItem
            // 
            this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
            this.saveConfigurationToolStripMenuItem.Size = new System.Drawing.Size( 114, 22 );
            this.saveConfigurationToolStripMenuItem.Text = "Save";
            this.saveConfigurationToolStripMenuItem.Click += new System.EventHandler( this.saveConfigurationToolStripMenuItem_Click );
            // 
            // loadConfigurationToolStripMenuItem
            // 
            this.loadConfigurationToolStripMenuItem.Name = "loadConfigurationToolStripMenuItem";
            this.loadConfigurationToolStripMenuItem.Size = new System.Drawing.Size( 114, 22 );
            this.loadConfigurationToolStripMenuItem.Text = "Save As";
            this.loadConfigurationToolStripMenuItem.Click += new System.EventHandler( this.saveAsConfigurationToolStripMenuItem_Click );
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size( 114, 22 );
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler( this.loadToolStripMenuItem_Click );
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "hair";
            this.saveFileDialog.Filter = "Hair configuration files|*.hair";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "hair";
            this.openFileDialog.Filter = "Hair configuration files|*.hair";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add( this.tabPage2 );
            this.tabControl1.Controls.Add( this.tabPage5 );
            this.tabControl1.Controls.Add( this.tabPage1 );
            this.tabControl1.Controls.Add( this.tabPage4 );
            this.tabControl1.Controls.Add( this.tabPage3 );
            this.tabControl1.Controls.Add( this.tabPage6 );
            this.tabControl1.Location = new System.Drawing.Point( 818, 36 );
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size( 311, 541 );
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add( this.groupBox3 );
            this.tabPage2.Controls.Add( this.numericUpDownSeed );
            this.tabPage2.Controls.Add( this.label5 );
            this.tabPage2.Controls.Add( this.buttonRestartWithRandomSeed );
            this.tabPage2.Controls.Add( this.restartGroupBox );
            this.tabPage2.Controls.Add( this.buttonRestart );
            this.tabPage2.Controls.Add( this.label1 );
            this.tabPage2.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage2.Size = new System.Drawing.Size( 303, 488 );
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Initialization";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add( this.textBoxNumberOfAirParticles );
            this.groupBox3.Controls.Add( this.label14 );
            this.groupBox3.Controls.Add( this.trackBarNumberOfAirParticles );
            this.groupBox3.Location = new System.Drawing.Point( 6, 201 );
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size( 291, 67 );
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Air";
            // 
            // textBoxNumberOfAirParticles
            // 
            this.textBoxNumberOfAirParticles.Enabled = false;
            this.textBoxNumberOfAirParticles.Location = new System.Drawing.Point( 240, 26 );
            this.textBoxNumberOfAirParticles.Name = "textBoxNumberOfAirParticles";
            this.textBoxNumberOfAirParticles.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxNumberOfAirParticles.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point( 6, 26 );
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size( 68, 13 );
            this.label14.TabIndex = 4;
            this.label14.Text = "# of particles";
            // 
            // trackBarNumberOfAirParticles
            // 
            this.trackBarNumberOfAirParticles.AutoSize = false;
            this.trackBarNumberOfAirParticles.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarNumberOfAirParticles.LargeChange = 30;
            this.trackBarNumberOfAirParticles.Location = new System.Drawing.Point( 80, 26 );
            this.trackBarNumberOfAirParticles.Maximum = 154;
            this.trackBarNumberOfAirParticles.Name = "trackBarNumberOfAirParticles";
            this.trackBarNumberOfAirParticles.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarNumberOfAirParticles.SmallChange = 5;
            this.trackBarNumberOfAirParticles.TabIndex = 3;
            this.trackBarNumberOfAirParticles.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarNumberOfAirParticles.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point( 6, 12 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size( 137, 13 );
            this.label5.TabIndex = 22;
            this.label5.Text = "Changes that require restart";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add( this.groupBox1 );
            this.tabPage5.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size( 303, 488 );
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Environment";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.label7 );
            this.groupBox1.Controls.Add( this.trackBarGravity );
            this.groupBox1.Controls.Add( this.textBoxGravity );
            this.groupBox1.Controls.Add( this.trackBarAirFriction );
            this.groupBox1.Controls.Add( this.label8 );
            this.groupBox1.Controls.Add( this.textBoxAirFriction );
            this.groupBox1.Location = new System.Drawing.Point( 3, 3 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 297, 103 );
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Environment";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point( 6, 25 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size( 40, 13 );
            this.label7.TabIndex = 7;
            this.label7.Text = "Gravity";
            // 
            // trackBarGravity
            // 
            this.trackBarGravity.AutoSize = false;
            this.trackBarGravity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarGravity.Location = new System.Drawing.Point( 80, 25 );
            this.trackBarGravity.Maximum = 154;
            this.trackBarGravity.Name = "trackBarGravity";
            this.trackBarGravity.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarGravity.TabIndex = 6;
            this.trackBarGravity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarGravity.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // textBoxGravity
            // 
            this.textBoxGravity.Enabled = false;
            this.textBoxGravity.Location = new System.Drawing.Point( 240, 25 );
            this.textBoxGravity.Name = "textBoxGravity";
            this.textBoxGravity.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxGravity.TabIndex = 8;
            // 
            // trackBarAirFriction
            // 
            this.trackBarAirFriction.AutoSize = false;
            this.trackBarAirFriction.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarAirFriction.Location = new System.Drawing.Point( 80, 51 );
            this.trackBarAirFriction.Maximum = 154;
            this.trackBarAirFriction.Name = "trackBarAirFriction";
            this.trackBarAirFriction.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarAirFriction.TabIndex = 9;
            this.trackBarAirFriction.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAirFriction.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point( 6, 51 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size( 53, 13 );
            this.label8.TabIndex = 10;
            this.label8.Text = "Air friction";
            // 
            // textBoxAirFriction
            // 
            this.textBoxAirFriction.Enabled = false;
            this.textBoxAirFriction.Location = new System.Drawing.Point( 240, 51 );
            this.textBoxAirFriction.Name = "textBoxAirFriction";
            this.textBoxAirFriction.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxAirFriction.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add( this.groupBox2 );
            this.tabPage1.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage1.Size = new System.Drawing.Size( 303, 488 );
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hair";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add( this.groupBox4 );
            this.tabPage4.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage4.Size = new System.Drawing.Size( 303, 488 );
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Air";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add( this.button1 );
            this.groupBox4.Controls.Add( this.textBoxAverageDensityAir );
            this.groupBox4.Controls.Add( this.label15 );
            this.groupBox4.Controls.Add( this.label6 );
            this.groupBox4.Controls.Add( this.textBoxDrag );
            this.groupBox4.Controls.Add( this.trackBarAverageDensityAir );
            this.groupBox4.Controls.Add( this.trackBarAttractionRepulsionAir );
            this.groupBox4.Controls.Add( this.textBoxAttractionRepulsionAir );
            this.groupBox4.Controls.Add( this.trackBarDrag );
            this.groupBox4.Controls.Add( this.label10 );
            this.groupBox4.Location = new System.Drawing.Point( 6, 6 );
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size( 291, 206 );
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Air properties";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 57, 152 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 185, 38 );
            this.button1.TabIndex = 36;
            this.button1.Text = "FAN ON/OFF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler( this.button1_Click_1 );
            // 
            // textBoxAverageDensityAir
            // 
            this.textBoxAverageDensityAir.Enabled = false;
            this.textBoxAverageDensityAir.Location = new System.Drawing.Point( 240, 107 );
            this.textBoxAverageDensityAir.Name = "textBoxAverageDensityAir";
            this.textBoxAverageDensityAir.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxAverageDensityAir.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point( 6, 26 );
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size( 60, 13 );
            this.label15.TabIndex = 28;
            this.label15.Text = "Drag coeff.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point( 6, 91 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size( 109, 13 );
            this.label6.TabIndex = 34;
            this.label6.Text = "Average density of air";
            // 
            // textBoxDrag
            // 
            this.textBoxDrag.Enabled = false;
            this.textBoxDrag.Location = new System.Drawing.Point( 240, 26 );
            this.textBoxDrag.Name = "textBoxDrag";
            this.textBoxDrag.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxDrag.TabIndex = 29;
            // 
            // trackBarAverageDensityAir
            // 
            this.trackBarAverageDensityAir.AutoSize = false;
            this.trackBarAverageDensityAir.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarAverageDensityAir.Location = new System.Drawing.Point( 80, 107 );
            this.trackBarAverageDensityAir.Maximum = 154;
            this.trackBarAverageDensityAir.Name = "trackBarAverageDensityAir";
            this.trackBarAverageDensityAir.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarAverageDensityAir.TabIndex = 33;
            this.trackBarAverageDensityAir.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAverageDensityAir.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // trackBarAttractionRepulsionAir
            // 
            this.trackBarAttractionRepulsionAir.AutoSize = false;
            this.trackBarAttractionRepulsionAir.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarAttractionRepulsionAir.Location = new System.Drawing.Point( 80, 68 );
            this.trackBarAttractionRepulsionAir.Maximum = 154;
            this.trackBarAttractionRepulsionAir.Name = "trackBarAttractionRepulsionAir";
            this.trackBarAttractionRepulsionAir.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarAttractionRepulsionAir.TabIndex = 30;
            this.trackBarAttractionRepulsionAir.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAttractionRepulsionAir.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // textBoxAttractionRepulsionAir
            // 
            this.textBoxAttractionRepulsionAir.Enabled = false;
            this.textBoxAttractionRepulsionAir.Location = new System.Drawing.Point( 240, 68 );
            this.textBoxAttractionRepulsionAir.Name = "textBoxAttractionRepulsionAir";
            this.textBoxAttractionRepulsionAir.Size = new System.Drawing.Size( 46, 20 );
            this.textBoxAttractionRepulsionAir.TabIndex = 32;
            // 
            // trackBarDrag
            // 
            this.trackBarDrag.AutoSize = false;
            this.trackBarDrag.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarDrag.Location = new System.Drawing.Point( 80, 26 );
            this.trackBarDrag.Maximum = 154;
            this.trackBarDrag.Name = "trackBarDrag";
            this.trackBarDrag.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarDrag.TabIndex = 27;
            this.trackBarDrag.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarDrag.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point( 6, 52 );
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size( 149, 13 );
            this.label10.TabIndex = 31;
            this.label10.Text = "Attraction-repulsion magnitude";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add( this.propertyGridRenderer );
            this.tabPage3.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage3.Size = new System.Drawing.Size( 303, 488 );
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rendering";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // propertyGridRenderer
            // 
            this.propertyGridRenderer.Location = new System.Drawing.Point( 6, 6 );
            this.propertyGridRenderer.Name = "propertyGridRenderer";
            this.propertyGridRenderer.Size = new System.Drawing.Size( 291, 503 );
            this.propertyGridRenderer.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add( this.label16 );
            this.tabPage6.Controls.Add( this.trackBarLightIntensity );
            this.tabPage6.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size( 303, 488 );
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Light";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point( 11, 12 );
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size( 72, 13 );
            this.label16.TabIndex = 6;
            this.label16.Text = "Light Intensity";
            // 
            // trackBarLightIntensity
            // 
            this.trackBarLightIntensity.AutoSize = false;
            this.trackBarLightIntensity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarLightIntensity.LargeChange = 30;
            this.trackBarLightIntensity.Location = new System.Drawing.Point( 89, 12 );
            this.trackBarLightIntensity.Maximum = 153;
            this.trackBarLightIntensity.Name = "trackBarLightIntensity";
            this.trackBarLightIntensity.Size = new System.Drawing.Size( 154, 20 );
            this.trackBarLightIntensity.SmallChange = 5;
            this.trackBarLightIntensity.TabIndex = 5;
            this.trackBarLightIntensity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarLightIntensity.Value = 77;
            this.trackBarLightIntensity.ValueChanged += new System.EventHandler( this.updateTextBoxes );
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.White;
            // 
            // diffuseDialog
            // 
            this.diffuseDialog.Color = System.Drawing.Color.White;
            // 
            // ambientDialog
            // 
            this.ambientDialog.Color = System.Drawing.Color.White;
            // 
            // specularDialog
            // 
            this.specularDialog.Color = System.Drawing.Color.White;
            // 
            // ControlsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 1141, 661 );
            this.Controls.Add( this.tabControl1 );
            this.Controls.Add( this.buttonPause );
            this.Controls.Add( this.statusStrip1 );
            this.Controls.Add( this.menuStrip1 );
            this.Controls.Add( this.glControl );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ControlsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ControlsWindow";
            this.restartGroupBox.ResumeLayout( false );
            this.restartGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxRootDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHairLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfParticles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).EndInit();
            this.statusStrip1.ResumeLayout( false );
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCollisionDamping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAverageDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAttractionRepulsion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFrictionDamping)).EndInit();
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout( false );
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout( false );
            this.tabPage2.ResumeLayout( false );
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout( false );
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfAirParticles)).EndInit();
            this.tabPage5.ResumeLayout( false );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGravity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAirFriction)).EndInit();
            this.tabPage1.ResumeLayout( false );
            this.tabPage4.ResumeLayout( false );
            this.groupBox4.ResumeLayout( false );
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAverageDensityAir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAttractionRepulsionAir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDrag)).EndInit();
            this.tabPage3.ResumeLayout( false );
            this.tabPage6.ResumeLayout( false );
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightIntensity)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.GroupBox restartGroupBox;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.TextBox textBoxMaxRootDepth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarMaxRootDepth;
        private System.Windows.Forms.TextBox textBoxHairLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarHairLength;
        private System.Windows.Forms.TextBox textBoxNumberOfParticles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarNumberOfParticles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer fpsDisplayTimer;
        private System.Windows.Forms.ToolStripStatusLabel FPSLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textBoxCollisionDamping;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trackBarCollisionDamping;
        private System.Windows.Forms.TextBox textBoxAverageDensity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar trackBarAverageDensity;
        private System.Windows.Forms.TextBox textBoxAttractionRepulsion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar trackBarAttractionRepulsion;
        private System.Windows.Forms.TextBox textBoxFrictionDamping;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TrackBar trackBarFrictionDamping;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownSeed;
        private System.Windows.Forms.Button buttonRestartWithRandomSeed;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PropertyGrid propertyGridRenderer;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBarGravity;
        private System.Windows.Forms.TextBox textBoxGravity;
        private System.Windows.Forms.TrackBar trackBarAirFriction;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxAirFriction;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxNumberOfAirParticles;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TrackBar trackBarNumberOfAirParticles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxAverageDensityAir;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDrag;
        private System.Windows.Forms.TrackBar trackBarAverageDensityAir;
        private System.Windows.Forms.TrackBar trackBarAttractionRepulsionAir;
        private System.Windows.Forms.TextBox textBoxAttractionRepulsionAir;
        private System.Windows.Forms.TrackBar trackBarDrag;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ColorDialog diffuseDialog;
        private System.Windows.Forms.ColorDialog ambientDialog;
        private System.Windows.Forms.ColorDialog specularDialog;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TrackBar trackBarLightIntensity;
    }
}