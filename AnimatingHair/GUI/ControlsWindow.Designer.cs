namespace AnimatingHair.GUI
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
            this.buttonRestartWithRandomSeed = new System.Windows.Forms.Button();
            this.numericUpDownSeed = new System.Windows.Forms.NumericUpDown();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fpsDisplayTimer = new System.Windows.Forms.Timer( this.components );
            this.FPSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarAverageHairDensity = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarHairDensityForceMagnitude = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarFrictionDamp = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarCollisionDamp = new AnimatingHair.GUI.VisualTrackBar();
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarAirParticleCount = new AnimatingHair.GUI.VisualTrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarMaxRootDepth = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarHairLength = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarHairParticleCount = new AnimatingHair.GUI.VisualTrackBar();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarTimeStep = new AnimatingHair.GUI.VisualTrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarAirFriction = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarGravity = new AnimatingHair.GUI.VisualTrackBar();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarReflect = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarTransmit = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarShininess = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarAmbient = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarSpecular = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarDiffuse = new AnimatingHair.GUI.VisualTrackBar();
            this.buttonCancelCut = new System.Windows.Forms.Button();
            this.buttonCutting = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarAirDensityForceMagnitude = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarAverageAirDensity = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarDragCoefficient = new AnimatingHair.GUI.VisualTrackBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowDebugAir = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.checkBoxOnlyShowOccupiedVoxels = new System.Windows.Forms.CheckBox();
            this.checkBoxShowVoxelGrid = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowMetaBust = new System.Windows.Forms.CheckBox();
            this.checkBoxShowBust = new System.Windows.Forms.CheckBox();
            this.checkBoxShowHair = new System.Windows.Forms.CheckBox();
            this.groupBoxHairRendering = new System.Windows.Forms.GroupBox();
            this.visualTrackBarBillboardWidth = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarBillboardLength = new AnimatingHair.GUI.VisualTrackBar();
            this.checkBoxDirectionalOpacity = new System.Windows.Forms.CheckBox();
            this.checkBoxShowConnections = new System.Windows.Forms.CheckBox();
            this.checkBoxDebugHair = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDeepOpacityMapResolution = new System.Windows.Forms.ComboBox();
            this.visualTrackBarDeepOpacityMapDistance = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarAlphaTreshold = new AnimatingHair.GUI.VisualTrackBar();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarLightIntensity = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarLightDistance = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarLightCruiseSpeed = new AnimatingHair.GUI.VisualTrackBar();
            this.checkBoxCruisingLight = new System.Windows.Forms.CheckBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarAirMassFactor = new AnimatingHair.GUI.VisualTrackBar();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarElasticModulus = new AnimatingHair.GUI.VisualTrackBar();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.visualTrackBarDensityOfHairMaterial = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarMaxNeighbors = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarHairMassFactor = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarNeighborAlignmentTreshold = new AnimatingHair.GUI.VisualTrackBar();
            this.visualTrackBarSecondMomentOfArea = new AnimatingHair.GUI.VisualTrackBar();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.diffuseDialog = new System.Windows.Forms.ColorDialog();
            this.ambientDialog = new System.Windows.Forms.ColorDialog();
            this.specularDialog = new System.Windows.Forms.ColorDialog();
            this.checkBoxParallel = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBoxHairRendering.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
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
            // buttonRestartWithRandomSeed
            // 
            this.buttonRestartWithRandomSeed.Location = new System.Drawing.Point( 1022, 604 );
            this.buttonRestartWithRandomSeed.Name = "buttonRestartWithRandomSeed";
            this.buttonRestartWithRandomSeed.Size = new System.Drawing.Size( 107, 32 );
            this.buttonRestartWithRandomSeed.TabIndex = 21;
            this.buttonRestartWithRandomSeed.Text = "with random seed";
            this.buttonRestartWithRandomSeed.UseVisualStyleBackColor = true;
            this.buttonRestartWithRandomSeed.Click += new System.EventHandler( this.buttonRestartWithRandomSeed_Click );
            // 
            // numericUpDownSeed
            // 
            this.numericUpDownSeed.Location = new System.Drawing.Point( 47, 6 );
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
            this.buttonRestart.Location = new System.Drawing.Point( 1022, 578 );
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size( 107, 32 );
            this.buttonRestart.TabIndex = 18;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler( this.button1_Click );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 9, 8 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 32, 13 );
            this.label1.TabIndex = 1;
            this.label1.Text = "Seed";
            // 
            // fpsDisplayTimer
            // 
            this.fpsDisplayTimer.Interval = 500;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add( this.visualTrackBarAverageHairDensity );
            this.groupBox2.Controls.Add( this.visualTrackBarHairDensityForceMagnitude );
            this.groupBox2.Controls.Add( this.visualTrackBarFrictionDamp );
            this.groupBox2.Controls.Add( this.visualTrackBarCollisionDamp );
            this.groupBox2.Location = new System.Drawing.Point( 6, 6 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 291, 188 );
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Properties";
            // 
            // visualTrackBarAverageHairDensity
            // 
            this.visualTrackBarAverageHairDensity.Label = "Average Hair Density";
            this.visualTrackBarAverageHairDensity.Location = new System.Drawing.Point( 6, 103 );
            this.visualTrackBarAverageHairDensity.Name = "visualTrackBarAverageHairDensity";
            this.visualTrackBarAverageHairDensity.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarAverageHairDensity.TabIndex = 5;
            // 
            // visualTrackBarHairDensityForceMagnitude
            // 
            this.visualTrackBarHairDensityForceMagnitude.Label = "Hair Density Force Magnitude";
            this.visualTrackBarHairDensityForceMagnitude.Location = new System.Drawing.Point( 6, 145 );
            this.visualTrackBarHairDensityForceMagnitude.Name = "visualTrackBarHairDensityForceMagnitude";
            this.visualTrackBarHairDensityForceMagnitude.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarHairDensityForceMagnitude.TabIndex = 4;
            // 
            // visualTrackBarFrictionDamp
            // 
            this.visualTrackBarFrictionDamp.Label = "Friction Damp";
            this.visualTrackBarFrictionDamp.Location = new System.Drawing.Point( 6, 61 );
            this.visualTrackBarFrictionDamp.Name = "visualTrackBarFrictionDamp";
            this.visualTrackBarFrictionDamp.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarFrictionDamp.TabIndex = 3;
            // 
            // visualTrackBarCollisionDamp
            // 
            this.visualTrackBarCollisionDamp.Label = "Collision Damp";
            this.visualTrackBarCollisionDamp.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarCollisionDamp.Name = "visualTrackBarCollisionDamp";
            this.visualTrackBarCollisionDamp.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarCollisionDamp.TabIndex = 2;
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point( 85, 145 );
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size( 121, 37 );
            this.buttonColor.TabIndex = 27;
            this.buttonColor.Text = "Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler( this.buttonColor_Click );
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point( 818, 578 );
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size( 91, 58 );
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
            this.tabControl1.Controls.Add( this.tabPage7 );
            this.tabControl1.Location = new System.Drawing.Point( 818, 36 );
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size( 311, 540 );
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add( this.groupBox6 );
            this.tabPage2.Controls.Add( this.groupBox3 );
            this.tabPage2.Controls.Add( this.numericUpDownSeed );
            this.tabPage2.Controls.Add( this.label1 );
            this.tabPage2.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage2.Size = new System.Drawing.Size( 303, 487 );
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Initialization";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add( this.visualTrackBarAirParticleCount );
            this.groupBox6.Location = new System.Drawing.Point( 6, 183 );
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size( 291, 62 );
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Air";
            // 
            // visualTrackBarAirParticleCount
            // 
            this.visualTrackBarAirParticleCount.Label = "Number of Particles";
            this.visualTrackBarAirParticleCount.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarAirParticleCount.Name = "visualTrackBarAirParticleCount";
            this.visualTrackBarAirParticleCount.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarAirParticleCount.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add( this.visualTrackBarMaxRootDepth );
            this.groupBox3.Controls.Add( this.visualTrackBarHairLength );
            this.groupBox3.Controls.Add( this.visualTrackBarHairParticleCount );
            this.groupBox3.Location = new System.Drawing.Point( 6, 32 );
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size( 291, 145 );
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hair";
            // 
            // visualTrackBarMaxRootDepth
            // 
            this.visualTrackBarMaxRootDepth.Label = "Max Root Depth";
            this.visualTrackBarMaxRootDepth.Location = new System.Drawing.Point( 6, 103 );
            this.visualTrackBarMaxRootDepth.Name = "visualTrackBarMaxRootDepth";
            this.visualTrackBarMaxRootDepth.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarMaxRootDepth.TabIndex = 2;
            // 
            // visualTrackBarHairLength
            // 
            this.visualTrackBarHairLength.Label = "Hair Length";
            this.visualTrackBarHairLength.Location = new System.Drawing.Point( 6, 61 );
            this.visualTrackBarHairLength.Name = "visualTrackBarHairLength";
            this.visualTrackBarHairLength.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarHairLength.TabIndex = 1;
            // 
            // visualTrackBarHairParticleCount
            // 
            this.visualTrackBarHairParticleCount.Label = "Number of Particles";
            this.visualTrackBarHairParticleCount.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarHairParticleCount.Name = "visualTrackBarHairParticleCount";
            this.visualTrackBarHairParticleCount.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarHairParticleCount.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add( this.groupBox5 );
            this.tabPage5.Controls.Add( this.groupBox1 );
            this.tabPage5.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size( 303, 487 );
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Global";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add( this.visualTrackBarTimeStep );
            this.groupBox5.Location = new System.Drawing.Point( 6, 118 );
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size( 291, 63 );
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Simulation";
            // 
            // visualTrackBarTimeStep
            // 
            this.visualTrackBarTimeStep.Label = "Time Step";
            this.visualTrackBarTimeStep.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarTimeStep.Name = "visualTrackBarTimeStep";
            this.visualTrackBarTimeStep.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarTimeStep.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.visualTrackBarAirFriction );
            this.groupBox1.Controls.Add( this.visualTrackBarGravity );
            this.groupBox1.Location = new System.Drawing.Point( 6, 6 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 291, 106 );
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Environment";
            // 
            // visualTrackBarAirFriction
            // 
            this.visualTrackBarAirFriction.Label = "Air Friction";
            this.visualTrackBarAirFriction.Location = new System.Drawing.Point( 6, 61 );
            this.visualTrackBarAirFriction.Name = "visualTrackBarAirFriction";
            this.visualTrackBarAirFriction.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarAirFriction.TabIndex = 2;
            // 
            // visualTrackBarGravity
            // 
            this.visualTrackBarGravity.Label = "Gravity";
            this.visualTrackBarGravity.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarGravity.Name = "visualTrackBarGravity";
            this.visualTrackBarGravity.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarGravity.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add( this.groupBox15 );
            this.tabPage1.Controls.Add( this.buttonCancelCut );
            this.tabPage1.Controls.Add( this.buttonCutting );
            this.tabPage1.Controls.Add( this.groupBox2 );
            this.tabPage1.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage1.Size = new System.Drawing.Size( 303, 487 );
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hair";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add( this.visualTrackBarReflect );
            this.groupBox15.Controls.Add( this.buttonColor );
            this.groupBox15.Controls.Add( this.visualTrackBarTransmit );
            this.groupBox15.Controls.Add( this.visualTrackBarShininess );
            this.groupBox15.Controls.Add( this.visualTrackBarAmbient );
            this.groupBox15.Controls.Add( this.visualTrackBarSpecular );
            this.groupBox15.Controls.Add( this.visualTrackBarDiffuse );
            this.groupBox15.Location = new System.Drawing.Point( 6, 200 );
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size( 291, 225 );
            this.groupBox15.TabIndex = 31;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Appearance";
            // 
            // visualTrackBarReflect
            // 
            this.visualTrackBarReflect.Label = "Reflect";
            this.visualTrackBarReflect.Location = new System.Drawing.Point( 149, 103 );
            this.visualTrackBarReflect.Name = "visualTrackBarReflect";
            this.visualTrackBarReflect.Size = new System.Drawing.Size( 136, 36 );
            this.visualTrackBarReflect.TabIndex = 11;
            // 
            // visualTrackBarTransmit
            // 
            this.visualTrackBarTransmit.Label = "Transmit";
            this.visualTrackBarTransmit.Location = new System.Drawing.Point( 6, 103 );
            this.visualTrackBarTransmit.Name = "visualTrackBarTransmit";
            this.visualTrackBarTransmit.Size = new System.Drawing.Size( 136, 36 );
            this.visualTrackBarTransmit.TabIndex = 10;
            // 
            // visualTrackBarShininess
            // 
            this.visualTrackBarShininess.Label = "Shininess";
            this.visualTrackBarShininess.Location = new System.Drawing.Point( 149, 61 );
            this.visualTrackBarShininess.Name = "visualTrackBarShininess";
            this.visualTrackBarShininess.Size = new System.Drawing.Size( 136, 36 );
            this.visualTrackBarShininess.TabIndex = 9;
            // 
            // visualTrackBarAmbient
            // 
            this.visualTrackBarAmbient.Label = "Ambient";
            this.visualTrackBarAmbient.Location = new System.Drawing.Point( 6, 61 );
            this.visualTrackBarAmbient.Name = "visualTrackBarAmbient";
            this.visualTrackBarAmbient.Size = new System.Drawing.Size( 136, 36 );
            this.visualTrackBarAmbient.TabIndex = 8;
            // 
            // visualTrackBarSpecular
            // 
            this.visualTrackBarSpecular.Label = "Specular";
            this.visualTrackBarSpecular.Location = new System.Drawing.Point( 149, 19 );
            this.visualTrackBarSpecular.Name = "visualTrackBarSpecular";
            this.visualTrackBarSpecular.Size = new System.Drawing.Size( 136, 36 );
            this.visualTrackBarSpecular.TabIndex = 7;
            // 
            // visualTrackBarDiffuse
            // 
            this.visualTrackBarDiffuse.Label = "Diffuse";
            this.visualTrackBarDiffuse.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarDiffuse.Name = "visualTrackBarDiffuse";
            this.visualTrackBarDiffuse.Size = new System.Drawing.Size( 136, 36 );
            this.visualTrackBarDiffuse.TabIndex = 6;
            // 
            // buttonCancelCut
            // 
            this.buttonCancelCut.Location = new System.Drawing.Point( 196, 443 );
            this.buttonCancelCut.Name = "buttonCancelCut";
            this.buttonCancelCut.Size = new System.Drawing.Size( 104, 38 );
            this.buttonCancelCut.TabIndex = 30;
            this.buttonCancelCut.Text = "Cancel Cut";
            this.buttonCancelCut.UseVisualStyleBackColor = true;
            this.buttonCancelCut.Visible = false;
            this.buttonCancelCut.Click += new System.EventHandler( this.buttonCancelCut_Click );
            // 
            // buttonCutting
            // 
            this.buttonCutting.Location = new System.Drawing.Point( 6, 431 );
            this.buttonCutting.Name = "buttonCutting";
            this.buttonCutting.Size = new System.Drawing.Size( 184, 50 );
            this.buttonCutting.TabIndex = 29;
            this.buttonCutting.Text = "Start Cutting";
            this.buttonCutting.UseVisualStyleBackColor = true;
            this.buttonCutting.Click += new System.EventHandler( this.buttonCutting_Click );
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add( this.button1 );
            this.tabPage4.Controls.Add( this.groupBox4 );
            this.tabPage4.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage4.Size = new System.Drawing.Size( 303, 487 );
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Air";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 59, 158 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 185, 38 );
            this.button1.TabIndex = 36;
            this.button1.Text = "FAN ON/OFF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler( this.button1_Click_1 );
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add( this.visualTrackBarAirDensityForceMagnitude );
            this.groupBox4.Controls.Add( this.visualTrackBarAverageAirDensity );
            this.groupBox4.Controls.Add( this.visualTrackBarDragCoefficient );
            this.groupBox4.Location = new System.Drawing.Point( 6, 6 );
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size( 291, 146 );
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Air properties";
            // 
            // visualTrackBarAirDensityForceMagnitude
            // 
            this.visualTrackBarAirDensityForceMagnitude.Label = "Air Density Force Magnitude";
            this.visualTrackBarAirDensityForceMagnitude.Location = new System.Drawing.Point( 6, 103 );
            this.visualTrackBarAirDensityForceMagnitude.Name = "visualTrackBarAirDensityForceMagnitude";
            this.visualTrackBarAirDensityForceMagnitude.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarAirDensityForceMagnitude.TabIndex = 5;
            // 
            // visualTrackBarAverageAirDensity
            // 
            this.visualTrackBarAverageAirDensity.Label = "Average Air Density";
            this.visualTrackBarAverageAirDensity.Location = new System.Drawing.Point( 6, 61 );
            this.visualTrackBarAverageAirDensity.Name = "visualTrackBarAverageAirDensity";
            this.visualTrackBarAverageAirDensity.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarAverageAirDensity.TabIndex = 4;
            // 
            // visualTrackBarDragCoefficient
            // 
            this.visualTrackBarDragCoefficient.Label = "Drag Coefficient";
            this.visualTrackBarDragCoefficient.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarDragCoefficient.Name = "visualTrackBarDragCoefficient";
            this.visualTrackBarDragCoefficient.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarDragCoefficient.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add( this.groupBox12 );
            this.tabPage3.Controls.Add( this.groupBox11 );
            this.tabPage3.Controls.Add( this.groupBox10 );
            this.tabPage3.Controls.Add( this.checkBoxShowHair );
            this.tabPage3.Controls.Add( this.groupBoxHairRendering );
            this.tabPage3.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage3.Size = new System.Drawing.Size( 303, 487 );
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rendering";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add( this.checkBoxShowDebugAir );
            this.groupBox12.Location = new System.Drawing.Point( 7, 283 );
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size( 290, 43 );
            this.groupBox12.TabIndex = 6;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Air";
            // 
            // checkBoxShowDebugAir
            // 
            this.checkBoxShowDebugAir.AutoSize = true;
            this.checkBoxShowDebugAir.Location = new System.Drawing.Point( 7, 19 );
            this.checkBoxShowDebugAir.Name = "checkBoxShowDebugAir";
            this.checkBoxShowDebugAir.Size = new System.Drawing.Size( 103, 17 );
            this.checkBoxShowDebugAir.TabIndex = 0;
            this.checkBoxShowDebugAir.Text = "Show Debug Air";
            this.checkBoxShowDebugAir.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add( this.checkBoxOnlyShowOccupiedVoxels );
            this.groupBox11.Controls.Add( this.checkBoxShowVoxelGrid );
            this.groupBox11.Location = new System.Drawing.Point( 7, 234 );
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size( 290, 43 );
            this.groupBox11.TabIndex = 5;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Voxel Grid";
            // 
            // checkBoxOnlyShowOccupiedVoxels
            // 
            this.checkBoxOnlyShowOccupiedVoxels.AutoSize = true;
            this.checkBoxOnlyShowOccupiedVoxels.Location = new System.Drawing.Point( 117, 19 );
            this.checkBoxOnlyShowOccupiedVoxels.Name = "checkBoxOnlyShowOccupiedVoxels";
            this.checkBoxOnlyShowOccupiedVoxels.Size = new System.Drawing.Size( 160, 17 );
            this.checkBoxOnlyShowOccupiedVoxels.TabIndex = 2;
            this.checkBoxOnlyShowOccupiedVoxels.Text = "Only Show Occupied Voxels";
            this.checkBoxOnlyShowOccupiedVoxels.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowVoxelGrid
            // 
            this.checkBoxShowVoxelGrid.AutoSize = true;
            this.checkBoxShowVoxelGrid.Location = new System.Drawing.Point( 7, 19 );
            this.checkBoxShowVoxelGrid.Name = "checkBoxShowVoxelGrid";
            this.checkBoxShowVoxelGrid.Size = new System.Drawing.Size( 104, 17 );
            this.checkBoxShowVoxelGrid.TabIndex = 1;
            this.checkBoxShowVoxelGrid.Text = "Show Voxel Grid";
            this.checkBoxShowVoxelGrid.UseVisualStyleBackColor = true;
            this.checkBoxShowVoxelGrid.CheckedChanged += new System.EventHandler( this.checkBoxShowVoxelGrid_CheckedChanged );
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add( this.checkBoxShowMetaBust );
            this.groupBox10.Controls.Add( this.checkBoxShowBust );
            this.groupBox10.Location = new System.Drawing.Point( 6, 185 );
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size( 291, 43 );
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Bust";
            // 
            // checkBoxShowMetaBust
            // 
            this.checkBoxShowMetaBust.AutoSize = true;
            this.checkBoxShowMetaBust.Location = new System.Drawing.Point( 97, 19 );
            this.checkBoxShowMetaBust.Name = "checkBoxShowMetaBust";
            this.checkBoxShowMetaBust.Size = new System.Drawing.Size( 104, 17 );
            this.checkBoxShowMetaBust.TabIndex = 1;
            this.checkBoxShowMetaBust.Text = "Show Meta Bust";
            this.checkBoxShowMetaBust.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowBust
            // 
            this.checkBoxShowBust.AutoSize = true;
            this.checkBoxShowBust.Location = new System.Drawing.Point( 6, 19 );
            this.checkBoxShowBust.Name = "checkBoxShowBust";
            this.checkBoxShowBust.Size = new System.Drawing.Size( 77, 17 );
            this.checkBoxShowBust.TabIndex = 0;
            this.checkBoxShowBust.Text = "Show Bust";
            this.checkBoxShowBust.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowHair
            // 
            this.checkBoxShowHair.AutoSize = true;
            this.checkBoxShowHair.Location = new System.Drawing.Point( 6, 6 );
            this.checkBoxShowHair.Name = "checkBoxShowHair";
            this.checkBoxShowHair.Size = new System.Drawing.Size( 75, 17 );
            this.checkBoxShowHair.TabIndex = 0;
            this.checkBoxShowHair.Text = "Show Hair";
            this.checkBoxShowHair.UseVisualStyleBackColor = true;
            this.checkBoxShowHair.CheckedChanged += new System.EventHandler( this.checkBoxShowHair_CheckedChanged );
            // 
            // groupBoxHairRendering
            // 
            this.groupBoxHairRendering.Controls.Add( this.visualTrackBarBillboardWidth );
            this.groupBoxHairRendering.Controls.Add( this.visualTrackBarBillboardLength );
            this.groupBoxHairRendering.Controls.Add( this.checkBoxDirectionalOpacity );
            this.groupBoxHairRendering.Controls.Add( this.checkBoxShowConnections );
            this.groupBoxHairRendering.Controls.Add( this.checkBoxDebugHair );
            this.groupBoxHairRendering.Location = new System.Drawing.Point( 6, 29 );
            this.groupBoxHairRendering.Name = "groupBoxHairRendering";
            this.groupBoxHairRendering.Size = new System.Drawing.Size( 291, 150 );
            this.groupBoxHairRendering.TabIndex = 3;
            this.groupBoxHairRendering.TabStop = false;
            this.groupBoxHairRendering.Text = "Hair";
            // 
            // visualTrackBarBillboardWidth
            // 
            this.visualTrackBarBillboardWidth.Label = "Billboard Width";
            this.visualTrackBarBillboardWidth.Location = new System.Drawing.Point( 6, 107 );
            this.visualTrackBarBillboardWidth.Name = "visualTrackBarBillboardWidth";
            this.visualTrackBarBillboardWidth.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarBillboardWidth.TabIndex = 5;
            // 
            // visualTrackBarBillboardLength
            // 
            this.visualTrackBarBillboardLength.Label = "Billboard Length";
            this.visualTrackBarBillboardLength.Location = new System.Drawing.Point( 6, 65 );
            this.visualTrackBarBillboardLength.Name = "visualTrackBarBillboardLength";
            this.visualTrackBarBillboardLength.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarBillboardLength.TabIndex = 4;
            // 
            // checkBoxDirectionalOpacity
            // 
            this.checkBoxDirectionalOpacity.AutoSize = true;
            this.checkBoxDirectionalOpacity.Location = new System.Drawing.Point( 6, 42 );
            this.checkBoxDirectionalOpacity.Name = "checkBoxDirectionalOpacity";
            this.checkBoxDirectionalOpacity.Size = new System.Drawing.Size( 115, 17 );
            this.checkBoxDirectionalOpacity.TabIndex = 3;
            this.checkBoxDirectionalOpacity.Text = "Directional Opacity";
            this.checkBoxDirectionalOpacity.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowConnections
            // 
            this.checkBoxShowConnections.AutoSize = true;
            this.checkBoxShowConnections.Location = new System.Drawing.Point( 70, 19 );
            this.checkBoxShowConnections.Name = "checkBoxShowConnections";
            this.checkBoxShowConnections.Size = new System.Drawing.Size( 115, 17 );
            this.checkBoxShowConnections.TabIndex = 2;
            this.checkBoxShowConnections.Text = "Show Connections";
            this.checkBoxShowConnections.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebugHair
            // 
            this.checkBoxDebugHair.AutoSize = true;
            this.checkBoxDebugHair.Location = new System.Drawing.Point( 6, 19 );
            this.checkBoxDebugHair.Name = "checkBoxDebugHair";
            this.checkBoxDebugHair.Size = new System.Drawing.Size( 58, 17 );
            this.checkBoxDebugHair.TabIndex = 1;
            this.checkBoxDebugHair.Text = "Debug";
            this.checkBoxDebugHair.UseVisualStyleBackColor = true;
            this.checkBoxDebugHair.CheckedChanged += new System.EventHandler( this.checkBoxDebugHair_CheckedChanged );
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add( this.groupBox14 );
            this.tabPage6.Controls.Add( this.groupBox13 );
            this.tabPage6.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size( 303, 487 );
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Light and Shadows";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add( this.label2 );
            this.groupBox14.Controls.Add( this.comboBoxDeepOpacityMapResolution );
            this.groupBox14.Controls.Add( this.visualTrackBarDeepOpacityMapDistance );
            this.groupBox14.Controls.Add( this.visualTrackBarAlphaTreshold );
            this.groupBox14.Location = new System.Drawing.Point( 6, 182 );
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size( 291, 131 );
            this.groupBox14.TabIndex = 4;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Shadows";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 9, 106 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 198, 13 );
            this.label2.TabIndex = 11;
            this.label2.Text = "Opacity Map Resolution (requires restart)";
            // 
            // comboBoxDeepOpacityMapResolution
            // 
            this.comboBoxDeepOpacityMapResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDeepOpacityMapResolution.FormattingEnabled = true;
            this.comboBoxDeepOpacityMapResolution.Items.AddRange( new object[] {
            "2048",
            "1024",
            "512",
            "256",
            "128"} );
            this.comboBoxDeepOpacityMapResolution.Location = new System.Drawing.Point( 225, 103 );
            this.comboBoxDeepOpacityMapResolution.Name = "comboBoxDeepOpacityMapResolution";
            this.comboBoxDeepOpacityMapResolution.Size = new System.Drawing.Size( 60, 21 );
            this.comboBoxDeepOpacityMapResolution.TabIndex = 10;
            // 
            // visualTrackBarDeepOpacityMapDistance
            // 
            this.visualTrackBarDeepOpacityMapDistance.Label = "Deep Opacity Map Distance";
            this.visualTrackBarDeepOpacityMapDistance.Location = new System.Drawing.Point( 6, 61 );
            this.visualTrackBarDeepOpacityMapDistance.Name = "visualTrackBarDeepOpacityMapDistance";
            this.visualTrackBarDeepOpacityMapDistance.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarDeepOpacityMapDistance.TabIndex = 9;
            // 
            // visualTrackBarAlphaTreshold
            // 
            this.visualTrackBarAlphaTreshold.Label = "Alpha Treshold";
            this.visualTrackBarAlphaTreshold.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarAlphaTreshold.Name = "visualTrackBarAlphaTreshold";
            this.visualTrackBarAlphaTreshold.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarAlphaTreshold.TabIndex = 8;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add( this.visualTrackBarLightIntensity );
            this.groupBox13.Controls.Add( this.visualTrackBarLightDistance );
            this.groupBox13.Controls.Add( this.visualTrackBarLightCruiseSpeed );
            this.groupBox13.Controls.Add( this.checkBoxCruisingLight );
            this.groupBox13.Location = new System.Drawing.Point( 6, 6 );
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size( 291, 170 );
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Light";
            // 
            // visualTrackBarLightIntensity
            // 
            this.visualTrackBarLightIntensity.Label = "Light Intensity";
            this.visualTrackBarLightIntensity.Location = new System.Drawing.Point( 6, 126 );
            this.visualTrackBarLightIntensity.Name = "visualTrackBarLightIntensity";
            this.visualTrackBarLightIntensity.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarLightIntensity.TabIndex = 3;
            // 
            // visualTrackBarLightDistance
            // 
            this.visualTrackBarLightDistance.Label = "Light Distance";
            this.visualTrackBarLightDistance.Location = new System.Drawing.Point( 6, 84 );
            this.visualTrackBarLightDistance.Name = "visualTrackBarLightDistance";
            this.visualTrackBarLightDistance.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarLightDistance.TabIndex = 2;
            // 
            // visualTrackBarLightCruiseSpeed
            // 
            this.visualTrackBarLightCruiseSpeed.Label = "Light Cruise Speed";
            this.visualTrackBarLightCruiseSpeed.Location = new System.Drawing.Point( 6, 42 );
            this.visualTrackBarLightCruiseSpeed.Name = "visualTrackBarLightCruiseSpeed";
            this.visualTrackBarLightCruiseSpeed.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarLightCruiseSpeed.TabIndex = 1;
            // 
            // checkBoxCruisingLight
            // 
            this.checkBoxCruisingLight.AutoSize = true;
            this.checkBoxCruisingLight.Location = new System.Drawing.Point( 6, 19 );
            this.checkBoxCruisingLight.Name = "checkBoxCruisingLight";
            this.checkBoxCruisingLight.Size = new System.Drawing.Size( 86, 17 );
            this.checkBoxCruisingLight.TabIndex = 0;
            this.checkBoxCruisingLight.Text = "LightCruising";
            this.checkBoxCruisingLight.UseVisualStyleBackColor = true;
            this.checkBoxCruisingLight.CheckedChanged += new System.EventHandler( this.checkBoxCruisingLight_CheckedChanged );
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add( this.groupBox9 );
            this.tabPage7.Controls.Add( this.groupBox8 );
            this.tabPage7.Controls.Add( this.groupBox7 );
            this.tabPage7.Location = new System.Drawing.Point( 4, 49 );
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size( 303, 487 );
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Advanced";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add( this.visualTrackBarAirMassFactor );
            this.groupBox9.Location = new System.Drawing.Point( 3, 310 );
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size( 297, 64 );
            this.groupBox9.TabIndex = 13;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Air Initialization Properties";
            // 
            // visualTrackBarAirMassFactor
            // 
            this.visualTrackBarAirMassFactor.Label = "Air Mass Factor";
            this.visualTrackBarAirMassFactor.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarAirMassFactor.Name = "visualTrackBarAirMassFactor";
            this.visualTrackBarAirMassFactor.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarAirMassFactor.TabIndex = 13;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add( this.visualTrackBarElasticModulus );
            this.groupBox8.Location = new System.Drawing.Point( 3, 240 );
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size( 297, 64 );
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Hair Physical Properties";
            // 
            // visualTrackBarElasticModulus
            // 
            this.visualTrackBarElasticModulus.Label = "Elastic Modulus";
            this.visualTrackBarElasticModulus.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarElasticModulus.Name = "visualTrackBarElasticModulus";
            this.visualTrackBarElasticModulus.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarElasticModulus.TabIndex = 8;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add( this.visualTrackBarDensityOfHairMaterial );
            this.groupBox7.Controls.Add( this.visualTrackBarMaxNeighbors );
            this.groupBox7.Controls.Add( this.visualTrackBarHairMassFactor );
            this.groupBox7.Controls.Add( this.visualTrackBarNeighborAlignmentTreshold );
            this.groupBox7.Controls.Add( this.visualTrackBarSecondMomentOfArea );
            this.groupBox7.Location = new System.Drawing.Point( 3, 3 );
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size( 297, 231 );
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Hair Initialization Properties";
            // 
            // visualTrackBarDensityOfHairMaterial
            // 
            this.visualTrackBarDensityOfHairMaterial.Label = "Density of Hair Material";
            this.visualTrackBarDensityOfHairMaterial.Location = new System.Drawing.Point( 6, 187 );
            this.visualTrackBarDensityOfHairMaterial.Name = "visualTrackBarDensityOfHairMaterial";
            this.visualTrackBarDensityOfHairMaterial.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarDensityOfHairMaterial.TabIndex = 7;
            // 
            // visualTrackBarMaxNeighbors
            // 
            this.visualTrackBarMaxNeighbors.Label = "Max Neigbors";
            this.visualTrackBarMaxNeighbors.Location = new System.Drawing.Point( 6, 19 );
            this.visualTrackBarMaxNeighbors.Name = "visualTrackBarMaxNeighbors";
            this.visualTrackBarMaxNeighbors.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarMaxNeighbors.TabIndex = 5;
            // 
            // visualTrackBarHairMassFactor
            // 
            this.visualTrackBarHairMassFactor.Label = "Hair Mass Factor";
            this.visualTrackBarHairMassFactor.Location = new System.Drawing.Point( 6, 145 );
            this.visualTrackBarHairMassFactor.Name = "visualTrackBarHairMassFactor";
            this.visualTrackBarHairMassFactor.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarHairMassFactor.TabIndex = 10;
            // 
            // visualTrackBarNeighborAlignmentTreshold
            // 
            this.visualTrackBarNeighborAlignmentTreshold.Label = "Neighbor Alignment Treshold";
            this.visualTrackBarNeighborAlignmentTreshold.Location = new System.Drawing.Point( 6, 61 );
            this.visualTrackBarNeighborAlignmentTreshold.Name = "visualTrackBarNeighborAlignmentTreshold";
            this.visualTrackBarNeighborAlignmentTreshold.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarNeighborAlignmentTreshold.TabIndex = 6;
            // 
            // visualTrackBarSecondMomentOfArea
            // 
            this.visualTrackBarSecondMomentOfArea.Label = "Second Moment Of Area";
            this.visualTrackBarSecondMomentOfArea.Location = new System.Drawing.Point( 6, 103 );
            this.visualTrackBarSecondMomentOfArea.Name = "visualTrackBarSecondMomentOfArea";
            this.visualTrackBarSecondMomentOfArea.Size = new System.Drawing.Size( 279, 36 );
            this.visualTrackBarSecondMomentOfArea.TabIndex = 9;
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
            // checkBoxParallel
            // 
            this.checkBoxParallel.AutoSize = true;
            this.checkBoxParallel.Checked = true;
            this.checkBoxParallel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxParallel.Location = new System.Drawing.Point( 915, 600 );
            this.checkBoxParallel.Name = "checkBoxParallel";
            this.checkBoxParallel.Size = new System.Drawing.Size( 75, 17 );
            this.checkBoxParallel.TabIndex = 34;
            this.checkBoxParallel.Text = "Parallelism";
            this.checkBoxParallel.UseVisualStyleBackColor = true;
            // 
            // ControlsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 1141, 661 );
            this.Controls.Add( this.checkBoxParallel );
            this.Controls.Add( this.buttonRestart );
            this.Controls.Add( this.tabControl1 );
            this.Controls.Add( this.buttonPause );
            this.Controls.Add( this.statusStrip1 );
            this.Controls.Add( this.buttonRestartWithRandomSeed );
            this.Controls.Add( this.menuStrip1 );
            this.Controls.Add( this.glControl );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ControlsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ControlsWindow";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).EndInit();
            this.statusStrip1.ResumeLayout( false );
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout( false );
            this.menuStrip1.ResumeLayout( false );
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout( false );
            this.tabPage2.ResumeLayout( false );
            this.tabPage2.PerformLayout();
            this.groupBox6.ResumeLayout( false );
            this.groupBox3.ResumeLayout( false );
            this.tabPage5.ResumeLayout( false );
            this.groupBox5.ResumeLayout( false );
            this.groupBox1.ResumeLayout( false );
            this.tabPage1.ResumeLayout( false );
            this.groupBox15.ResumeLayout( false );
            this.tabPage4.ResumeLayout( false );
            this.groupBox4.ResumeLayout( false );
            this.tabPage3.ResumeLayout( false );
            this.tabPage3.PerformLayout();
            this.groupBox12.ResumeLayout( false );
            this.groupBox12.PerformLayout();
            this.groupBox11.ResumeLayout( false );
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout( false );
            this.groupBox10.PerformLayout();
            this.groupBoxHairRendering.ResumeLayout( false );
            this.groupBoxHairRendering.PerformLayout();
            this.tabPage6.ResumeLayout( false );
            this.groupBox14.ResumeLayout( false );
            this.groupBox14.PerformLayout();
            this.groupBox13.ResumeLayout( false );
            this.groupBox13.PerformLayout();
            this.tabPage7.ResumeLayout( false );
            this.groupBox9.ResumeLayout( false );
            this.groupBox8.ResumeLayout( false );
            this.groupBox7.ResumeLayout( false );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer fpsDisplayTimer;
        private System.Windows.Forms.ToolStripStatusLabel FPSLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
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
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ColorDialog diffuseDialog;
        private System.Windows.Forms.ColorDialog ambientDialog;
        private System.Windows.Forms.ColorDialog specularDialog;
        private System.Windows.Forms.GroupBox groupBox3;
        private VisualTrackBar visualTrackBarMaxRootDepth;
        private VisualTrackBar visualTrackBarHairLength;
        private VisualTrackBar visualTrackBarHairParticleCount;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TabPage tabPage7;
        private VisualTrackBar visualTrackBarNeighborAlignmentTreshold;
        private VisualTrackBar visualTrackBarMaxNeighbors;
        private VisualTrackBar visualTrackBarHairMassFactor;
        private VisualTrackBar visualTrackBarSecondMomentOfArea;
        private VisualTrackBar visualTrackBarElasticModulus;
        private VisualTrackBar visualTrackBarDensityOfHairMaterial;
        private System.Windows.Forms.GroupBox groupBox7;
        private VisualTrackBar visualTrackBarAirParticleCount;
        private System.Windows.Forms.GroupBox groupBox8;
        private VisualTrackBar visualTrackBarAirMassFactor;
        private VisualTrackBar visualTrackBarAirFriction;
        private VisualTrackBar visualTrackBarGravity;
        private VisualTrackBar visualTrackBarAverageHairDensity;
        private VisualTrackBar visualTrackBarHairDensityForceMagnitude;
        private VisualTrackBar visualTrackBarFrictionDamp;
        private VisualTrackBar visualTrackBarCollisionDamp;
        private VisualTrackBar visualTrackBarAirDensityForceMagnitude;
        private VisualTrackBar visualTrackBarAverageAirDensity;
        private VisualTrackBar visualTrackBarDragCoefficient;
        private System.Windows.Forms.GroupBox groupBox5;
        private VisualTrackBar visualTrackBarTimeStep;
        private System.Windows.Forms.CheckBox checkBoxShowConnections;
        private System.Windows.Forms.CheckBox checkBoxDebugHair;
        private System.Windows.Forms.CheckBox checkBoxShowHair;
        private System.Windows.Forms.GroupBox groupBoxHairRendering;
        private System.Windows.Forms.CheckBox checkBoxDirectionalOpacity;
        private VisualTrackBar visualTrackBarBillboardLength;
        private VisualTrackBar visualTrackBarBillboardWidth;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox checkBoxShowBust;
        private System.Windows.Forms.CheckBox checkBoxShowMetaBust;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.CheckBox checkBoxOnlyShowOccupiedVoxels;
        private System.Windows.Forms.CheckBox checkBoxShowVoxelGrid;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.CheckBox checkBoxShowDebugAir;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.CheckBox checkBoxCruisingLight;
        private VisualTrackBar visualTrackBarLightDistance;
        private VisualTrackBar visualTrackBarLightCruiseSpeed;
        private VisualTrackBar visualTrackBarLightIntensity;
        private System.Windows.Forms.GroupBox groupBox14;
        private VisualTrackBar visualTrackBarDeepOpacityMapDistance;
        private VisualTrackBar visualTrackBarAlphaTreshold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDeepOpacityMapResolution;
        private System.Windows.Forms.Button buttonCutting;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button buttonCancelCut;
        private System.Windows.Forms.GroupBox groupBox15;
        private VisualTrackBar visualTrackBarDiffuse;
        private VisualTrackBar visualTrackBarSpecular;
        private VisualTrackBar visualTrackBarReflect;
        private VisualTrackBar visualTrackBarTransmit;
        private VisualTrackBar visualTrackBarShininess;
        private VisualTrackBar visualTrackBarAmbient;
        private System.Windows.Forms.CheckBox checkBoxParallel;
    }
}