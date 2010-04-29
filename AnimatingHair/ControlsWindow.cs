using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AnimatingHair.Entity;
using AnimatingHair.Initialization;
using AnimatingHair.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Collections.Generic;

namespace AnimatingHair
{
    public partial class ControlsWindow : Form
    {
        #region Members

        private Scene scene;
        private SceneInitializer sceneInitializer;
        private Renderer renderer;
        private Camera camera;
        private float distance = 10, elevation = 0, azimuth = 90, mouseX, mouseY;
        private readonly Random r = new Random();
        private bool loaded = false;
        private bool paused = true;
        private string saveFile = "";
        private readonly LinkedList<float> fpsHistory = new LinkedList<float>();

        #endregion

        public ControlsWindow()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            checkOpenGLVersion();

            sceneInitializer = new SceneInitializer();
            scene = sceneInitializer.InitializeScene();
            camera = new Camera();

            try
            {
                renderer = new Renderer( camera, scene );
            }
            catch ( IOException exception )
            {
                MessageBox.Show( exception.Message + "\n\nAborting.", exception.GetType().ToString() );
                Close();
            }

            initControls();

            propertyGridRenderer.SelectedObject = renderer;

            Application.Idle += applicationIdle;

            Timer.GetETime();

            loaded = true;
        }

        private void initControls()
        {
            glControl.KeyDown += glControl_KeyDown;
            glControl.KeyUp += glControl_KeyUp;
            glControl.Resize += glControl_Resize;
            glControl.Paint += glControl_Paint;

            glControl.MouseWheel += glControl_MouseWheel;

            colorDialog.Color = Color.FromArgb( (int)(scene.Hair.Clr[ 0 ] * 255), (int)(scene.Hair.Clr[ 1 ] * 255), (int)(scene.Hair.Clr[ 2 ] * 255) );
            buttonColor.BackColor = colorDialog.Color;

            trackBar_valueChanged( trackBarLightIntensity, null );

            Text =
                GL.GetString( StringName.Vendor ) + " " +
                GL.GetString( StringName.Renderer ) + " " +
                GL.GetString( StringName.Version );

            // Ensure that the viewport and projection matrix are set correctly.
            glControl_Resize( glControl, EventArgs.Empty );

            initializeTrackBars();

            fpsDisplayTimer.Enabled = true;
        }

        private void trackBar_valueChanged( object sender, EventArgs e )
        {
            TrackBar tb = sender as TrackBar;
            float value = ((float)tb.Value) / tb.Maximum;

            if ( tb == trackBarAirFriction )
            {
                textBoxAirFriction.Text = (value).ToString();
            }
            if ( tb == trackBarAttractionRepulsion )
            {
                textBoxAttractionRepulsion.Text = (value / 2).ToString();
            }
            if ( tb == trackBarAttractionRepulsionAir )
            {
                textBoxAttractionRepulsionAir.Text = (value / 2).ToString();
            }
            if ( tb == trackBarAverageDensity )
            {
                textBoxAverageDensity.Text = (value * 49 + 1).ToString();
            }
            if ( tb == trackBarAverageDensityAir )
            {
                textBoxAverageDensityAir.Text = (value * 3 + 0.1).ToString();
            }
            if ( tb == trackBarCollisionDamping )
            {
                textBoxCollisionDamping.Text = (value).ToString();
            }
            if ( tb == trackBarDrag )
            {
                textBoxDrag.Text = (value / 3).ToString();
            }
            if ( tb == trackBarFrictionDamping )
            {
                textBoxFrictionDamping.Text = (value).ToString();
            }
            if ( tb == trackBarGravity )
            {
                textBoxGravity.Text = (2 * value - 1).ToString();
            }
            if ( tb == trackBarHairLength )
            {
                textBoxHairLength.Text = (value * 4).ToString();
            }
            if ( tb == trackBarMaxRootDepth )
            {
                textBoxMaxRootDepth.Text = (value).ToString();
            }
            if ( tb == trackBarNumberOfAirParticles )
            {
                textBoxNumberOfAirParticles.Text = ((int)(value * 500 + 1)).ToString();
            }
            if ( tb == trackBarNumberOfParticles )
            {
                textBoxNumberOfParticles.Text = ((int)(value * 1900 + 100)).ToString();
            }
            if ( tb == trackBarLightIntensity )
            {
                renderer.LightIntensity = value * 2;
            }
            if ( tb == trackBarMisc1 )
            {
                renderer.Misc1 = value * 2;
            }
            if ( tb == trackBarMisc2 )
            {
                renderer.Misc2 = value * 2;
            }

            updateConstants( tb );
        }

        private void initializeTrackBars()
        {
            float maxValue = trackBarNumberOfParticles.Maximum;
            trackBarAirFriction.Value = Convert.ToInt32( maxValue * Const.AirFriction );
            trackBarAttractionRepulsion.Value = Convert.ToInt32( maxValue * Const.k_a * 2 );
            trackBarAttractionRepulsionAir.Value = Convert.ToInt32( maxValue * Const.k_a_air * 2 );
            trackBarAverageDensity.Value = Convert.ToInt32( maxValue * (Const.rho_0 - 1) / 49 );
            trackBarAverageDensityAir.Value = Convert.ToInt32( maxValue * (Const.rho_0_air - 0.1) / 3 );
            trackBarCollisionDamping.Value = Convert.ToInt32( maxValue * Const.d_c );
            trackBarDrag.Value = Convert.ToInt32( maxValue * Const.DragCoefficient * 3 );
            trackBarFrictionDamping.Value = Convert.ToInt32( maxValue * Const.d_f );
            trackBarGravity.Value = Convert.ToInt32( maxValue * (Const.Gravity + 1) / 2 );
            trackBarHairLength.Value = Convert.ToInt32( maxValue * Const.HairLength / 4 );
            trackBarMaxRootDepth.Value = Convert.ToInt32( maxValue * Const.s_r );
            trackBarNumberOfAirParticles.Value = Convert.ToInt32( maxValue * Const.AirParticleCount / 500.0 );
            trackBarNumberOfParticles.Value = Convert.ToInt32( maxValue * (Const.HairParticleCount - 100) / 1900.0 );
            numericUpDownSeed.Text = Const.Seed.ToString();
        }

        private void updateConstants( TrackBar tb )
        {
            if ( !loaded )
                return;

            if ( tb == trackBarAirFriction )
            {
                Const.AirFriction = float.Parse( textBoxAirFriction.Text );
            }
            if ( tb == trackBarAttractionRepulsion )
            {
                Const.k_a = float.Parse( textBoxAttractionRepulsion.Text );
            }
            if ( tb == trackBarAttractionRepulsionAir )
            {
                Const.k_a_air = float.Parse( textBoxAttractionRepulsionAir.Text );
            }
            if ( tb == trackBarAverageDensity )
            {
                Const.rho_0 = float.Parse( textBoxAverageDensity.Text );
            }
            if ( tb == trackBarAverageDensityAir )
            {
                Const.rho_0_air = float.Parse( textBoxAverageDensityAir.Text );
            }
            if ( tb == trackBarCollisionDamping )
            {
                Const.d_c = float.Parse( textBoxCollisionDamping.Text );
            }
            if ( tb == trackBarDrag )
            {
                Const.DragCoefficient = float.Parse( textBoxDrag.Text );
            }
            if ( tb == trackBarFrictionDamping )
            {
                Const.d_f = float.Parse( textBoxFrictionDamping.Text );
            }
            if ( tb == trackBarGravity )
            {
                Const.Gravity = float.Parse( textBoxGravity.Text );
            }
            if ( tb == trackBarHairLength )
            {
                Const.HairLength = float.Parse( textBoxHairLength.Text );
            }
            if ( tb == trackBarMaxRootDepth )
            {
                Const.s_r = float.Parse( textBoxMaxRootDepth.Text );
            }
            if ( tb == trackBarNumberOfAirParticles )
            {
                Const.AirParticleCount = int.Parse( textBoxNumberOfAirParticles.Text );
            }
            if ( tb == trackBarNumberOfParticles )
            {
                Const.HairParticleCount = int.Parse( textBoxNumberOfParticles.Text );
            }
        }

        private void polarToCartesian()
        {
            float x, y, z, k;
            float ele2 = elevation * Const.PI / 180;
            float azi2 = (azimuth - 90) * Const.PI / 180;

            k = (float)Math.Cos( ele2 );
            z = distance * (float)Math.Sin( ele2 );
            y = distance * (float)Math.Sin( azi2 ) * k;
            x = distance * (float)Math.Cos( azi2 ) * k;

            camera.Eye = new Vector3( x, z, y );
            camera.Target = new Vector3( 0, 0, 0 );
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

            if ( c.ClientSize.Height == 0 )
                c.ClientSize = new Size( c.ClientSize.Width, 1 );

            renderer.Resize( c.ClientSize.Width, c.ClientSize.Height, c.AspectRatio );
        }

        #endregion

        #region GLControl Key event handlers

        void glControl_KeyUp( object sender, KeyEventArgs e )
        {
            switch ( e.KeyData )
            {
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
            polarToCartesian();
            render();
        }

        #endregion

        #region private void render()

        private void render()
        {
            fpsHistory.AddFirst( 1000.0f / Timer.GetETime() );

            renderer.Render();

            glControl.SwapBuffers();

            if ( !paused )
            {
                scene.Step();
                Const.Time++;
            }
        }

        #endregion

        private void glControl_MouseWheel( object sender, MouseEventArgs e )
        {
            float ratio = 1 + (e.Delta / 1000.0f);

            distance /= ratio;

            if ( distance < 2 )
                distance = 2;
        }

        private void glControl_MouseMove( object sender, MouseEventArgs e )
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

            mouseX = e.X;
            mouseY = e.Y;
        }

        private void fpsDisplayTimer_Tick( object sender, EventArgs e )
        {
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
            updateRestartValues();
            restart();
        }

        private void restart()
        {
            if ( Const.HairParticleCount > 1000 )
            {
                Const.TimeStep = 0.03f;
            }
            else
            {
                Const.TimeStep = 0.1f;
            }

            scene = sceneInitializer.InitializeScene();
            camera = new Camera();
            renderer = new Renderer( camera, scene )
                       {
                           CruisingLight = ((Renderer)propertyGridRenderer.SelectedObject).CruisingLight,
                           DebugHair = ((Renderer)propertyGridRenderer.SelectedObject).DebugHair,
                           RenderConnections = ((Renderer)propertyGridRenderer.SelectedObject).RenderConnections,
                           ShowBust = ((Renderer)propertyGridRenderer.SelectedObject).ShowBust,
                           ShowMetaBust = ((Renderer)propertyGridRenderer.SelectedObject).ShowMetaBust,
                           ShowHair = ((Renderer)propertyGridRenderer.SelectedObject).ShowHair,
                           WireFrame = ((Renderer)propertyGridRenderer.SelectedObject).WireFrame,
                           ShowVoxelGrid = ((Renderer)propertyGridRenderer.SelectedObject).ShowVoxelGrid,
                           ShowAir = ((Renderer)propertyGridRenderer.SelectedObject).ShowAir,
                       };

            trackBar_valueChanged( trackBarLightIntensity, null );

            buttonColor.BackColor = colorDialog.Color;
            scene.Hair.Clr = new float[]
                                 {
                                     colorDialog.Color.R / 255.0f, 
                                     colorDialog.Color.G / 255.0f,
                                     colorDialog.Color.B / 255.0f
                                 };

            propertyGridRenderer.SelectedObject = renderer;

            polarToCartesian();
        }

        private void updateRestartValues()
        {
            Const.Seed = int.Parse( numericUpDownSeed.Text );
        }

        private void buttonRestartWithRandomSeed_Click( object sender, EventArgs e )
        {
            numericUpDownSeed.Text = r.Next( 1000 ).ToString();
            updateRestartValues();
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
                Utility.LoadConfiguration( saveFile );

                initializeTrackBars();
                restart();
            }

            paused = wasPaused;
            loaded = true;
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
    }
}
