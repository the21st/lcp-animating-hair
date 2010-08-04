using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AnimatingHair.Entity;
using AnimatingHair.Initialization;
using AnimatingHair.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using AnimatingHair.Auxiliary;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace AnimatingHair.GUI
{
    public partial class ControlsWindow : Form
    {
        #region Members

        private Scene scene;
        private SceneInitializer sceneInitializer;
        private Renderer renderer;
        private Camera camera;
        private float distance = 7, elevation = 0, azimuth = 180, mouseX, mouseY;
        private readonly Random r = new Random();
        private bool loaded = false;
        private bool paused = false;
        private bool cutting = false;
        private Vector3 cutMove;
        private Vector3 cutMove2;
        private CutterQuad cutter;
        private string saveFile = "";
        private readonly LinkedList<float> fpsHistory = new LinkedList<float>();
        readonly Stopwatch stopwatch = new Stopwatch();

        #endregion

        public ControlsWindow()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            checkOpenGLVersion();

            RenderingOptions.Instance.RenderWidth = glControl.Width;
            RenderingOptions.Instance.RenderHeight = glControl.Height;
            RenderingOptions.Instance.AspectRatio = (float)RenderingOptions.Instance.RenderWidth / RenderingOptions.Instance.RenderHeight;

            sceneInitializer = new SceneInitializer();
            cutter = new CutterQuad();

            try
            {
                scene = sceneInitializer.InitializeScene();
                camera = new Camera();
                renderer = new Renderer( camera, scene, cutter );
            }
            catch ( Exception exception )
            {
                MessageBox.Show( exception.Message + "\n\nAborting.", exception.GetType().ToString() );
                Close();
            }

            initControls();

            Application.Idle += applicationIdle;

            loaded = true;
        }

        private void initControls()
        {
            comboBoxDeepOpacityMapResolution.SelectedIndex = 1;

            bindData();

            glControl.KeyDown += glControl_KeyDown;
            glControl.KeyUp += glControl_KeyUp;
            glControl.Resize += glControl_Resize;
            glControl.Paint += glControl_Paint;

            glControl.MouseWheel += glControl_MouseWheel;

            colorDialog.Color = Color.FromArgb( (int)(scene.Hair.Clr[ 0 ] * 255), (int)(scene.Hair.Clr[ 1 ] * 255), (int)(scene.Hair.Clr[ 2 ] * 255) );
            buttonColor.BackColor = colorDialog.Color;

            Text =
                GL.GetString( StringName.Vendor ) + " " +
                GL.GetString( StringName.Renderer ) + " " +
                GL.GetString( StringName.Version );

            checkBoxShowHair_CheckedChanged( checkBoxShowHair, EventArgs.Empty );
            checkBoxDebugHair_CheckedChanged( checkBoxDebugHair, EventArgs.Empty );
            checkBoxShowVoxelGrid_CheckedChanged( checkBoxShowVoxelGrid, EventArgs.Empty );
            checkBoxCruisingLight_CheckedChanged( checkBoxCruisingLight, EventArgs.Empty );

            fpsDisplayTimer.Enabled = true;
        }

        private void bindData()
        {
            numericUpDownSeed.DataBindings.Add( "Text", Const.Instance, "Seed", true, DataSourceUpdateMode.OnPropertyChanged );
            visualTrackBarHairParticleCount.BindIntData( Const.Instance, "HairParticleCount", 2, 2000 );
            visualTrackBarMaxNeighbors.BindIntData( Const.Instance, "MaxNeighbors", 0, 25 );
            visualTrackBarAirParticleCount.BindIntData( Const.Instance, "AirParticleCount", 0, 1000 );
            visualTrackBarTimeStep.BindFloatData( Const.Instance, "TimeStep", 0.001f, 0.1f );
            visualTrackBarGravity.BindFloatData( Const.Instance, "Gravity", -2, 2 );
            visualTrackBarAirFriction.BindFloatData( Const.Instance, "AirFriction", 0.01f, 0.3f );
            visualTrackBarHairLength.BindFloatData( Const.Instance, "HairLength", 0.1f, 4 );
            visualTrackBarMaxRootDepth.BindFloatData( Const.Instance, "MaxRootDepth", 0, 1 );
            visualTrackBarNeighborAlignmentThreshold.BindFloatData( Const.Instance, "NeighborAlignmentThreshold", 0, 1 );
            visualTrackBarDensityOfHairMaterial.BindFloatData( Const.Instance, "DensityOfHairMaterial", 1, 300 );
            visualTrackBarElasticModulus.BindFloatData( Const.Instance, "ElasticModulus", 500, 15000 );
            visualTrackBarSecondMomentOfArea.BindFloatData( Const.Instance, "SecondMomentOfArea", 0.0005f, 0.01f );
            visualTrackBarHairMassFactor.BindFloatData( Const.Instance, "HairMassFactor", 100, 10000 );
            visualTrackBarCollisionDamp.BindFloatData( Const.Instance, "CollisionDamp", 0, 1 );
            visualTrackBarFrictionDamp.BindFloatData( Const.Instance, "FrictionDamp", 0, 1 );
            visualTrackBarAverageHairDensity.BindFloatData( Const.Instance, "AverageHairDensity", 3, 300 );
            visualTrackBarHairDensityForceMagnitude.BindFloatData( Const.Instance, "HairDensityForceMagnitude", 0.1f, 10 );
            visualTrackBarDragCoefficient.BindFloatData( Const.Instance, "DragCoefficient", 0.01f, 0.5f );
            visualTrackBarAverageAirDensity.BindFloatData( Const.Instance, "AverageAirDensity", 0.01f, 3 );
            visualTrackBarAirDensityForceMagnitude.BindFloatData( Const.Instance, "AirDensityForceMagnitude", 0.01f, 0.5f );
            visualTrackBarAirMassFactor.BindFloatData( Const.Instance, "AirMassFactor", 20, 500 );
            visualTrackBarDiffuse.BindFloatData( RenderingOptions.Instance, "DiffuseTerm", 0, 1 );
            visualTrackBarSpecular.BindFloatData( RenderingOptions.Instance, "SpecularTerm", 0, 1 );
            visualTrackBarAmbient.BindFloatData( RenderingOptions.Instance, "AmbientTerm", 0, 1 );
            visualTrackBarShininess.BindFloatData( RenderingOptions.Instance, "Shininess", 50, 300 );
            visualTrackBarReflect.BindFloatData( RenderingOptions.Instance, "Reflect", 0, 1 );
            visualTrackBarTransmit.BindFloatData( RenderingOptions.Instance, "Transmit", 0, 1 );
            checkBoxParallel.DataBindings.Add( "Checked", Const.Instance, "Parallel", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxShowHair.DataBindings.Add( "Checked", RenderingOptions.Instance, "ShowHair", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxDebugHair.DataBindings.Add( "Checked", RenderingOptions.Instance, "DebugHair", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxShowConnections.DataBindings.Add( "Checked", RenderingOptions.Instance, "ShowConnections", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxShowHead.DataBindings.Add( "Checked", RenderingOptions.Instance, "ShowHead", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxShowMetaHead.DataBindings.Add( "Checked", RenderingOptions.Instance, "ShowMetaHead", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxCruisingLight.DataBindings.Add( "Checked", RenderingOptions.Instance, "LightCruising", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxShowVoxelGrid.DataBindings.Add( "Checked", RenderingOptions.Instance, "ShowVoxelGrid", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxOnlyShowOccupiedVoxels.DataBindings.Add( "Checked", RenderingOptions.Instance, "OnlyShowOccupiedVoxels", true, DataSourceUpdateMode.OnPropertyChanged );
            checkBoxShowDebugAir.DataBindings.Add( "Checked", RenderingOptions.Instance, "ShowDebugAir", true, DataSourceUpdateMode.OnPropertyChanged );
            visualTrackBarBillboardLength.BindFloatData( RenderingOptions.Instance, "BillboardLength", 0, 1 );
            visualTrackBarBillboardWidth.BindFloatData( RenderingOptions.Instance, "BillboardWidth", 0, 1 );
            visualTrackBarAlphaThreshold.BindFloatData( RenderingOptions.Instance, "AlphaThreshold", 0, 1 );
            visualTrackBarDeepOpacityMapDistance.BindFloatData( RenderingOptions.Instance, "DeepOpacityMapDistance", 0.001f, 0.1f );
            visualTrackBarLightCruiseSpeed.BindFloatData( RenderingOptions.Instance, "LightCruiseSpeed", 0.0005f, 0.01f );
            visualTrackBarLightDistance.BindFloatData( RenderingOptions.Instance, "LightDistance", 1, 20 );
            visualTrackBarLightIntensity.BindFloatData( RenderingOptions.Instance, "LightIntensity", 0, 3 );
        }

        private void polarToCartesian()
        {
            float x, y, z, k;
            float ele2 = elevation * MathHelper.Pi / 180;
            float azi2 = (azimuth - 90) * MathHelper.Pi / 180;

            k = (float)Math.Cos( ele2 );
            z = distance * (float)Math.Sin( ele2 );
            y = distance * (float)Math.Sin( azi2 ) * k;
            x = distance * (float)Math.Cos( azi2 ) * k;

            camera.Eye = new Vector3( x, z, y );
            camera.Target = new Vector3( 0, -0.5f, -0.6f );
            camera.Up = Vector3.UnitY;
        }

        private void checkOpenGLVersion()
        {
            string version = GL.GetString( StringName.Version );
            char major = version[ 0 ];
            if ( major < '2' )
            {
                MessageBox.Show( "You need at least OpenGL 2.0 to run this application. Aborting.", "GLSL not supported",
                                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                Close();
            }
        }

        #region OnClosing

        protected override void OnClosing( CancelEventArgs e )
        {
            Application.Idle -= applicationIdle;

            base.OnClosing( e );
        }

        #endregion

        #region applicationIdle event

        void applicationIdle( object sender, EventArgs e )
        {
            while ( glControl.IsIdle )
            {
                polarToCartesian();
                render();
            }
        }

        #endregion

        #region GLControl.Resize event handler

        void glControl_Resize( object sender, EventArgs e )
        {
            GLControl c = sender as GLControl;

            if ( c != null )
            {
                if ( c.ClientSize.Height == 0 )
                    c.ClientSize = new Size( c.ClientSize.Width, 1 );

                RenderingOptions.Instance.RenderWidth = c.ClientSize.Width;
                RenderingOptions.Instance.RenderHeight = c.ClientSize.Height;
                RenderingOptions.Instance.AspectRatio = c.AspectRatio;
                renderer.Resize( c.ClientSize.Width, c.ClientSize.Height, c.AspectRatio );
            }
        }

        #endregion

        #region GLControl Key event handlers

        void glControl_KeyUp( object sender, KeyEventArgs e )
        {
            switch ( e.KeyData )
            {
                case Keys.Space:
                    scene.Step();
                    break;
                case Keys.NumPad8:
                    scene.Up = false;
                    break;
                case Keys.NumPad5:
                    scene.Down = false;
                    break;
                case Keys.NumPad4:
                    scene.Left = false;
                    break;
                case Keys.NumPad6:
                    scene.Right = false;
                    break;
                case Keys.NumPad7:
                    scene.RotateClockwise = false;
                    break;
                case Keys.NumPad9:
                    scene.RotateAntiClockwise = false;
                    break;
            }
        }

        void glControl_KeyDown( object sender, KeyEventArgs e )
        {
            switch ( e.KeyData )
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.NumPad8:
                    scene.Up = true;
                    break;
                case Keys.NumPad5:
                    scene.Down = true;
                    break;
                case Keys.NumPad4:
                    scene.Left = true;
                    break;
                case Keys.NumPad6:
                    scene.Right = true;
                    break;
                case Keys.NumPad7:
                    scene.RotateClockwise = true;
                    break;
                case Keys.NumPad9:
                    scene.RotateAntiClockwise = true;
                    break;
            }
        }

        #endregion

        #region GLControl.Paint event handler

        void glControl_Paint( object sender, PaintEventArgs e )
        {
            if ( !loaded ) return;
            polarToCartesian();
            render();
        }

        #endregion

        #region private void render()

        private void render()
        {
            stopwatch.Start();

            renderer.Render();

            if ( !paused )
            {
                scene.Step();
                Const.Instance.CurrentTimeStep++;
            }

            fpsHistory.AddFirst( 1000.0f / stopwatch.ElapsedMilliseconds );
            stopwatch.Reset();


            glControl.SwapBuffers();
        }

        #endregion

        private void glControl_MouseWheel( object sender, MouseEventArgs e )
        {
            if ( cutting )
            {
                cutter.Position.Y += 0.0005f * e.Delta;
            }
            else
            {
                float ratio = 1 + (e.Delta / 1000.0f);

                distance /= ratio;

                if ( distance < 2 )
                    distance = 2;
            }
        }

        private void glControl_MouseMove( object sender, MouseEventArgs e )
        {
            if ( cutting )
            {
                if ( e.Button == MouseButtons.Left )
                {
                    cutter.Position += 0.01f * (e.X - mouseX) * cutMove2 - 0.01f * (e.Y - mouseY) * cutMove;
                }
                if ( e.Button == MouseButtons.Right )
                {
                    cutter.Size -= 0.01f * (e.Y - mouseY);
                }
                if ( e.Button == MouseButtons.Middle )
                {
                    cutter.RotateX += 0.001f * (e.X - mouseX);
                    cutter.RotateZ += 0.001f * (e.Y - mouseY);
                }
            }
            else
            {
                if ( e.Button == MouseButtons.Left )
                {
                    azimuth += 0.1f * (e.X - mouseX);
                    if ( azimuth > 360 )
                        azimuth -= 360;

                    elevation += 0.1f * (e.Y - mouseY);
                    if ( elevation > 89.99999 )
                        elevation = 89.99999f;
                    if ( elevation < -89.99999 )
                        elevation = -89.99999f;
                }

                if ( e.Button == MouseButtons.Right )
                {
                    distance += 0.03f * (e.Y - mouseY);
                    if ( distance < 2 )
                        distance = 2;
                }
            }

            mouseX = e.X;
            mouseY = e.Y;
        }

        private void fpsDisplayTimer_Tick( object sender, EventArgs e )
        {
            if ( !loaded ) return;
            float sum = 0;
            foreach ( float d in fpsHistory )
            {
                sum += d;
            }
            float fps = sum / fpsHistory.Count;
            fpsHistory.Clear();
            FPSLabel.Text = "FPS: " + fps.ToString( "F2" );
        }

        private void button1_Click( object sender, EventArgs e )
        {
            restart();
        }

        private void restart()
        {
            try
            {
                RenderingOptions.Instance.ShadowMapsResolution = int.Parse( comboBoxDeepOpacityMapResolution.Text );

                scene = sceneInitializer.InitializeScene();
                camera = new Camera();
                renderer = new Renderer( camera, scene, cutter );
            }
            catch ( Exception exception )
            {
                MessageBox.Show( exception.Message + "\n\nAborting.", exception.GetType().ToString() );
                Close();
            }

            buttonColor.BackColor = colorDialog.Color;
            scene.Hair.Clr = new float[]
                             {
                                 colorDialog.Color.R / 255.0f, 
                                 colorDialog.Color.G / 255.0f,
                                 colorDialog.Color.B / 255.0f
                             };

            polarToCartesian();

            // Ensure that the viewport and projection matrix are set correctly.
            glControl_Resize( glControl, EventArgs.Empty );
        }

        private void buttonRestartWithRandomSeed_Click( object sender, EventArgs e )
        {
            numericUpDownSeed.Text = r.Next( 1000 ).ToString();
            restart();
        }

        private void buttonPause_Click( object sender, EventArgs e )
        {
            paused = !paused;

            if ( paused )
                buttonPause.Text = "RESUME SIMULATION";
            else
                buttonPause.Text = "PAUSE SIMULATION";
        }

        private static string getConfigurationsDirectory()
        {
            string targetDirectory = Application.StartupPath + @"\Configurations";

            if ( !Directory.Exists( targetDirectory ) )
                Directory.CreateDirectory( targetDirectory );

            return targetDirectory;
        }

        private void saveConfigurationToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // save

            if ( saveFile == "" )
            {
                saveFileDialog.InitialDirectory = getConfigurationsDirectory();
                if ( saveFileDialog.ShowDialog() == DialogResult.OK )
                {
                    saveFile = saveFileDialog.FileName;

                    Utility.SaveConfiguration( saveFile );
                }
            }
            else
            {
                Utility.SaveConfiguration( saveFile );
            }
        }

        private void saveAsConfigurationToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // save as

            if ( saveFile != "" )
                saveFileDialog.FileName = saveFile.Substring( saveFile.LastIndexOf( '\\' ) + 1 );

            saveFileDialog.InitialDirectory = getConfigurationsDirectory();
            if ( saveFileDialog.ShowDialog() == DialogResult.OK )
            {
                saveFile = saveFileDialog.FileName;
                Utility.SaveConfiguration( saveFile );
            }
        }

        private void loadToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // load

            loaded = false;
            bool wasPaused = paused;
            paused = true;

            openFileDialog.InitialDirectory = getConfigurationsDirectory();
            if ( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                saveFile = openFileDialog.FileName;
                try
                {
                    Utility.LoadConfiguration( saveFile );
                    refresh();
                    restart();
                }
                catch ( WrongFileFormatException )
                {
                    MessageBox.Show( "Wrong file format!" );
                }
            }

            paused = wasPaused;
            loaded = true;
        }

        private void refresh()
        {
            // NOTE: I have noticed that when I change the value of some control (e.g. the following control),
            // it refreshes the data bindings of the visual trackbar.
            // This IS a nasty workaround, but so far I have not found a reliably working way to refresh the databindings explicitly.
            string oldText = numericUpDownSeed.Text;
            numericUpDownSeed.Text = "-1";
            numericUpDownSeed.Text = oldText;
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void buttonColor_Click( object sender, EventArgs e )
        {
            if ( colorDialog.ShowDialog( this ) == DialogResult.OK )
            {
                buttonColor.BackColor = colorDialog.Color;
                scene.Hair.Clr = new float[]
                                 {
                                     colorDialog.Color.R / 255.0f, 
                                     colorDialog.Color.G / 255.0f,
                                     colorDialog.Color.B / 255.0f
                                 };
            }
        }

        private void button1_Click_1( object sender, EventArgs e )
        {
            scene.Fan = !scene.Fan;
        }

        private void checkBoxShowHair_CheckedChanged( object sender, EventArgs e )
        {
            if ( checkBoxShowHair.Checked )
                groupBoxHairRendering.Enabled = true;
            else
                groupBoxHairRendering.Enabled = false;
        }

        private void checkBoxDebugHair_CheckedChanged( object sender, EventArgs e )
        {
            if ( checkBoxDebugHair.Checked )
                checkBoxShowConnections.Enabled = true;
            else
                checkBoxShowConnections.Enabled = false;
        }

        private void checkBoxShowVoxelGrid_CheckedChanged( object sender, EventArgs e )
        {
            if ( checkBoxShowVoxelGrid.Checked )
                checkBoxOnlyShowOccupiedVoxels.Enabled = true;
            else
                checkBoxOnlyShowOccupiedVoxels.Enabled = false;
        }

        private void checkBoxCruisingLight_CheckedChanged( object sender, EventArgs e )
        {
            if ( checkBoxCruisingLight.Checked )
                visualTrackBarLightCruiseSpeed.Enabled = true;
            else
                visualTrackBarLightCruiseSpeed.Enabled = false;

        }

        private void buttonCutting_Click( object sender, EventArgs e )
        {
            // zacina strihanie
            if ( cutting )
                finishCutting();
            else
                beginCutting();
        }

        private void buttonCancelCut_Click( object sender, EventArgs e )
        {
            if ( cutting )
                cancelCutting();
        }

        private void beginCutting()
        {
            cutMove = camera.Target - camera.Eye;
            cutMove.Y = 0;
            cutMove.Normalize();
            cutMove2 = Vector3.Cross( cutMove, Vector3.UnitY );

            buttonCutting.Text = "CUT!";
            buttonCancelCut.Visible = true;

            RenderingOptions.Instance.Cutting = true;
            cutting = true;
        }

        private void finishCutting()
        {
            exitCuttingMode();

            Vector3[] vertices = cutter.GetVertices();
            scene.Hair.Cut( vertices[ 0 ], vertices[ 1 ], vertices[ 2 ] );
            scene.Hair.Cut( vertices[ 0 ], vertices[ 1 ], vertices[ 0 ] - 5 * Vector3.UnitY );
            scene.Hair.Cut( vertices[ 0 ], vertices[ 2 ], vertices[ 0 ] - 5 * Vector3.UnitY );
            scene.Hair.Cut( vertices[ 3 ], vertices[ 1 ], vertices[ 3 ] - 5 * Vector3.UnitY );
            scene.Hair.Cut( vertices[ 3 ], vertices[ 2 ], vertices[ 3 ] - 5 * Vector3.UnitY );
        }

        private void cancelCutting()
        {
            exitCuttingMode();
        }

        private void exitCuttingMode()
        {
            buttonCutting.Text = "Start Cutting";
            buttonCancelCut.Visible = false;

            RenderingOptions.Instance.Cutting = false;
            cutting = false;
        }
    }
}