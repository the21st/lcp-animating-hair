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

        private readonly OpacityMapsRenderer opacityMapsRenderer;

        // auxiliary
        private readonly float[] lightDiffuse;
        private readonly float[] lightAmbient;
        private readonly float[] lightSpecular;
        private float angle = 0;
        private int shadowTexture;
        private int depthTexture;
        private int viewportWidth = 800, viewportHeight = 600;
        private float aspectRatio = 800f / 600f;
        private float near = 1, far = 30;

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
            get
            {
                return light.Intensity;
            }
            set
            {
                light.Intensity = value;
                refreshLight();
            }
        }

        public float Misc1
        {
            set
            {
                //opacityMapsRenderer.Dist = value / 10.0f;
            }
        }

        public float Misc2
        {
            set
            {
                //opacityMapsRenderer.AlphaTreshold = value / 2.0f;
            }
        }

        #endregion

        public Renderer( Camera camera, Scene scene )
        {
            this.camera = camera;
            this.scene = scene;

            light = new Light
            {
                Intensity = 1f,
                Position = new Vector3( 5, 0, 5 )
            };

            hairRenderer = new HairRenderer( camera, scene.Hair, light );
            simpleHairRenderer = new SimpleHairRenderer( scene.Hair );

            airRenderer = new AirRenderer( scene.Air );

            bustRenderer = new BustRenderer( scene.Bust );
            metaBustRenderer = new MetaBustRenderer( scene.Bust );

            voxelGridRenderer = new VoxelGridRenderer( scene.VoxelGrid );

            opacityMapsRenderer = new OpacityMapsRenderer( scene.Hair, light );

            ShowBust = false;
            WireFrame = false;
            ShowMetaBust = false;
            ShowHair = true;
            DebugHair = false;
            CruisingLight = true;
            ShowVoxelGrid = false;
            ShowAir = false;

            lightDiffuse = new float[ 3 ];
            lightAmbient = new float[ 3 ];
            lightSpecular = new float[ 3 ];
            refreshLight();

            initializeOpenGL();
        }

        public void Render()
        {
            if ( CruisingLight )
                angle += 0.002f;

            light.Position = new Vector3( -5 * (float)Math.Sin( angle ), 5, 5 * (float)Math.Cos( angle ) );
            GL.Light( LightName.Light0, LightParameter.Position, new Vector4( light.Position, 1 ) );

            opacityMapsRenderer.RenderOpacityTexture();
            shadowTexture = opacityMapsRenderer.ShadowTexture;
            depthTexture = opacityMapsRenderer.DepthTexture;
            hairRenderer.DepthMap = depthTexture;
            hairRenderer.DeepOpacityMap = shadowTexture;

            setTextureMatrix(); // NOTE: vnutri funkcie

            initializeOpenGL();

            renderScene();
        }

        private void renderScene()
        {
            // clears buffer, sets modelview matrix to LookAt from camera
            prepareBufferAndMatrix();

            GL.Enable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Blend );
            GL.Disable( EnableCap.Lighting );
            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, shadowTexture );

            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 1, 0 );
                GL.Vertex3( -2, -2, 0 );

                GL.TexCoord2( 1, 1 );
                GL.Vertex3( -2, 2, 0 );

                GL.TexCoord2( 0, 1 );
                GL.Vertex3( 2, 2, 0 );

                GL.TexCoord2( 0, 0 );
                GL.Vertex3( 2, -2, 0 );
            }
            GL.End();
            return;

            if ( !DebugHair )
                GL.Enable( EnableCap.Blend );
            else
                GL.Disable( EnableCap.Blend );

            GL.Enable( EnableCap.DepthTest );
            GL.Disable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Lighting );
            drawAxes();
            GL.PointSize( 10 );
            GL.Color3( Color.White );
            GL.Begin( BeginMode.Points );
            GL.Vertex3( light.Position );
            GL.End();
            GL.PointSize( 1 );

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

        private void setTextureMatrix()
        {
            // This is matrix transform every coordinate x,y,z
            // x = x* 0.5 + 0.5 
            // y = y* 0.5 + 0.5 
            // z = z* 0.5 + 0.5 
            // Moving from unit cube [-1,1] to [0,1]  
            Matrix4 bias = new Matrix4(
                0.5f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.5f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.5f, 0.0f,
                0.5f, 0.5f, 0.5f, 1.0f ); // NOTE: transpose?
            //Matrix4 bias = new Matrix4(
            //    0.5f, 0.0f, 0.0f, 0.5f,
            //    0.0f, 0.5f, 0.0f, 0.5f,
            //    0.0f, 0.0f, 0.5f, 0.5f,
            //    0.0f, 0.0f, 0.0f, 1.0f ); // transpose

            GL.MatrixMode( MatrixMode.Texture );
            GL.ActiveTexture( TextureUnit.Texture7 );

            GL.LoadIdentity();
            GL.LoadMatrix( ref bias );

            // concatating all matrice into one.
            GL.MultMatrix( ref opacityMapsRenderer.LightProjectionMatrix );
            GL.MultMatrix( ref opacityMapsRenderer.LightModelViewMatrix );

            // Go back to normal matrix mode
            GL.MatrixMode( MatrixMode.Modelview );
        }

        public void Resize( int width, int height, float ratio )
        {
            viewportWidth = width;
            viewportHeight = height;
            aspectRatio = ratio;

            refreshViewport();
        }

        private void refreshViewport()
        {
            GL.Viewport( 0, 0, viewportWidth, viewportHeight );
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView( MathHelper.PiOver4, aspectRatio, near, far );
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadMatrix( ref perpective );
        }

        private void initializeOpenGL()
        {
            refreshViewport();

            GL.ClearColor( Color.CornflowerBlue );
            //GL.ClearColor( Color.Gray );

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
    }
}