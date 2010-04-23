using System;
using System.ComponentModel;
using System.Drawing;
using AnimatingHair.Rendering.Debug;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using AnimatingHair.Entity;

namespace AnimatingHair.Rendering
{
    /// <summary>
    /// Handles all OpenGL rendering.
    /// Is on the 'top' of the rendering hierarchy.
    /// </summary>
    class Renderer
    {
        // entities:
        private readonly Scene scene;
        private readonly Camera camera;
        private readonly Light light;

        // component renderers:
        private readonly HairRenderer hairRenderer;
        private readonly SimpleHairRenderer simpleHairRenderer;
        private readonly AirRenderer airRenderer;
        private readonly BustRenderer bustRenderer;
        private readonly MetaBustRenderer metaBustRenderer;
        private readonly VoxelGridRenderer voxelGridRenderer;

        // auxiliary
        private readonly float[] lightDiffuse;
        private readonly float[] lightAmbient;
        private readonly float[] lightSpecular;
        private float angle = -1.5f;

        #region Rendering options

        [CategoryAttribute( "Hair" ), DescriptionAttribute( "Indicates whether any hair is drawn." )]
        public bool ShowHair { get; set; }
        [CategoryAttribute( "Hair" ), DescriptionAttribute( "Indicates whether the hair is rendered in debug mode." )]
        public bool DebugHair { get; set; }
        [CategoryAttribute( "Hair" ), DescriptionAttribute( "Indicates whether neighbor connections are shown (only applies to debug mode)." )]
        public bool RenderConnections
        {
            get { return simpleHairRenderer.RenderConnections; }
            set { simpleHairRenderer.RenderConnections = value; }
        }
        [CategoryAttribute( "Bust" ), DescriptionAttribute( "Indicates whether the polygonal model of the bust is rendered." )]
        public bool ShowBust { get; set; }
        [CategoryAttribute( "Bust" ), DescriptionAttribute( "Indicates whether the mesh is drawn as a wireframe model." )]
        public bool WireFrame { get; set; }
        [CategoryAttribute( "Bust" ), DescriptionAttribute( "Indicates whether the analytical model of the bust is shown." )]
        public bool ShowMetaBust { get; set; }
        [CategoryAttribute( "Lights" ), DescriptionAttribute( "Indicates whether Light1 is orbiting or in place." )]
        public bool CruisingLight { get; set; }
        [CategoryAttribute( "Lights" ), DescriptionAttribute( "Indicates whether Light1 is turned on." )]
        public bool ShowVoxelGrid { get; set; }
        [CategoryAttribute( "Misc" ), DescriptionAttribute( "Indicates whether air particles are drawn (debug)." )]
        public bool ShowAir { get; set; }

        public float LightIntensity
        {
            set
            {
                light.Intensity = value;
                refreshLight();
            }
        }

        private void refreshLight()
        {
            lightDiffuse[ 0 ] = light.Intensity;
            lightDiffuse[ 1 ] = light.Intensity;
            lightDiffuse[ 2 ] = light.Intensity;
            lightAmbient[ 0 ] = light.Intensity / 10f;
            lightAmbient[ 1 ] = light.Intensity / 10f;
            lightAmbient[ 2 ] = light.Intensity / 10f;
            lightSpecular[ 0 ] = light.Intensity;
            lightSpecular[ 1 ] = light.Intensity;
            lightSpecular[ 2 ] = light.Intensity;
            GL.Light( LightName.Light0, LightParameter.Diffuse, lightDiffuse );
            GL.Light( LightName.Light0, LightParameter.Ambient, lightAmbient );
            GL.Light( LightName.Light0, LightParameter.Specular, lightSpecular );
        }

        #endregion

        public Renderer( Camera camera, Scene scene )
        {
            this.camera = camera;
            this.scene = scene;

            light = new Light
            {
                Intensity = 0.5f,
                Position = new Vector3( 5, 0, 5 )
            };

            hairRenderer = new HairRenderer( camera, scene.Hair );
            simpleHairRenderer = new SimpleHairRenderer( scene.Hair );

            airRenderer = new AirRenderer( scene.Air );

            bustRenderer = new BustRenderer( scene.Bust );
            metaBustRenderer = new MetaBustRenderer( scene.Bust );

            voxelGridRenderer = new VoxelGridRenderer( scene.VoxelGrid );

            ShowBust = true;
            WireFrame = false;
            ShowMetaBust = false;
            ShowHair = true;
            DebugHair = false;
            CruisingLight = false;
            ShowVoxelGrid = false;
            ShowAir = false;

            lightDiffuse = new float[ 3 ];
            lightAmbient = new float[ 3 ];
            lightSpecular = new float[ 3 ];
            refreshLight();

            initializeOpenGL();
        }

        /// <summary>
        /// The main rendering method. Called after each simulation step.
        /// </summary>
        /// <remarks>
        /// Calls the components' Render() methods according to rendering options.
        /// </remarks>
        public void Render()
        {
            renderScene();
        }

        private void renderScene()
        {
            // clears buffer, sets modelview matrix to LookAt from camera
            prepareBufferAndMatrix();

            if ( !DebugHair )
                GL.Enable( EnableCap.Blend );
            else
                GL.Disable( EnableCap.Blend );

            if ( CruisingLight )
                angle += 0.005f;

            if ( DebugHair && ShowHair )
            {
                light.Position = camera.Eye;
                GL.Light( LightName.Light0, LightParameter.Position, new Vector4( light.Position, 1 ) );
            }
            else
            {
                light.Position = new Vector3( -10 * (float)Math.Sin( angle ), 10, 10 * (float)Math.Cos( angle ) );
                GL.Light( LightName.Light0, LightParameter.Position, new Vector4( light.Position, 1 ) );
            }

            GL.Enable( EnableCap.DepthTest );
            GL.Disable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Lighting );
            drawAxes();

            GL.Enable( EnableCap.Lighting );

            if ( ShowBust )
            {
                bustRenderer.Wireframe = WireFrame;
                bustRenderer.Render();
            }
            GL.PushMatrix();
            GL.Translate( scene.Bust.Position );
            GL.Rotate( (scene.Bust.Angle * 180 / Const.PI), Vector3.UnitY );

            if ( ShowMetaBust )
                metaBustRenderer.Render();

            if ( ShowVoxelGrid )
                voxelGridRenderer.Render();

            if ( ShowAir )
            {
                airRenderer.Render();
            }

            if ( ShowHair )
            {
                if ( DebugHair )
                {
                    simpleHairRenderer.Render();
                }
                else
                {
                    GL.DepthMask( false );
                    hairRenderer.Render();
                    GL.DepthMask( true );
                }
            }

            GL.PopMatrix();
        }

        private void initializeOpenGL()
        {
            //GL.ClearColor( Color.CornflowerBlue );
            GL.ClearColor( Color.Gray );

            GL.TexEnv( TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Modulate );
            GL.BlendFunc( BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha );
            GL.ShadeModel( ShadingModel.Smooth );
            GL.Enable( EnableCap.Lighting );
            GL.Enable( EnableCap.Light0 );

            GL.Light( LightName.Light0, LightParameter.Diffuse, lightDiffuse );
            GL.Light( LightName.Light0, LightParameter.Ambient, lightAmbient );
            //GL.Light( LightName.Light0, LightParameter.Specular, lightSpecular );
        }

        private void prepareBufferAndMatrix()
        {
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );

            Matrix4 modelview = Matrix4.LookAt( camera.Eye, camera.Target, camera.Up );
            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadMatrix( ref modelview );
        }

        private static void drawAxes()
        {
            GL.Color3( Color.Red );
            GL.Begin( BeginMode.Lines );
            {
                GL.Vertex3( -Vector3.UnitX );
                GL.Vertex3( Vector3.UnitX );
                GL.Vertex3( -Vector3.UnitY );
                GL.Vertex3( Vector3.UnitY );
                GL.Vertex3( -Vector3.UnitZ );
                GL.Vertex3( Vector3.UnitZ );
            }
            GL.End();
        }
    }
}