using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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
        private int viewportWidth = 800, viewportHeight = 600;
        private float aspectRatio = 800f / 600f;
        private const float near = 1, far = 30;
        private float lightDistance = 8;

        // shader objects
        private readonly int shaderProgram;
        private readonly int modeLoc;
        public int Mode = 0;

        #region Rendering options

        [CategoryAttribute( "Hair" ), DescriptionAttribute( "Indicates whether any hair is drawn." )]
        public bool ShowHair { get; set; }
        [CategoryAttribute( "Hair" ), DescriptionAttribute( "Indicates whether the hair is rendered in debug Mode." )]
        public bool DebugHair { get; set; }
        [CategoryAttribute( "Hair" ), DescriptionAttribute( "Indicates whether neighbor connections are shown (only applies to debug Mode)." )]
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

        #endregion

        public float Misc1
        {
            set
            {
                angle = value * MathHelper.TwoPi + MathHelper.Pi;
            }
        }

        public float Misc2
        {
            set
            {
                //opacityMapsRenderer.AlphaTreshold = value / 2.0f;
                opacityMapsRenderer.IntensityFactor = value;
            }
        }


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
            CruisingLight = false;
            ShowVoxelGrid = false;
            ShowAir = false;

            lightDiffuse = new float[ 3 ];
            lightAmbient = new float[ 3 ];
            lightSpecular = new float[ 3 ];
            refreshLight();

            // shader loading
            using ( StreamReader vs = new StreamReader( FilePaths.DebugVSLocation ) )
            {
                using ( StreamReader fs = new StreamReader( FilePaths.DebugFSLocation ) )
                    Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd(), out shaderProgram );
            }
            modeLoc = GL.GetUniformLocation( shaderProgram, "Mode" );

            initializeOpenGL();
        }

        public void Render()
        {
            if ( CruisingLight )
                angle += 0.002f;

            light.Position = new Vector3( -lightDistance * (float)Math.Sin( angle ), 0.5f * lightDistance, lightDistance * (float)Math.Cos( angle ) );
            GL.Light( LightName.Light0, LightParameter.Position, new Vector4( light.Position, 1 ) );

            opacityMapsRenderer.RenderOpacityTexture();
            shadowTexture = opacityMapsRenderer.ShadowTexture;
            hairRenderer.DeepOpacityMap = shadowTexture;

            setTextureMatrix();

            initializeOpenGL();

            renderScene();
        }

        private void renderScene()
        {
            // clears buffer, sets modelview matrix to LookAt from camera
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );

            Matrix4 modelview = Matrix4.LookAt( camera.Eye, camera.Target, camera.Up );
            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadMatrix( ref modelview );

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
            GL.Disable( EnableCap.Blend );

            if ( ShowBust )
            {
                bustRenderer.Wireframe = WireFrame;
                bustRenderer.Render();
            }

            GL.PushMatrix();
            {
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
            }
            GL.PopMatrix();

            // --- HUD ---

            GL.Disable( EnableCap.DepthTest );
            GL.Disable( EnableCap.Lighting );
            GL.Disable( EnableCap.Blend );
            GL.DepthMask( false );
            GL.MatrixMode( MatrixMode.Projection );
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho( 0, viewportWidth, viewportHeight, 0, -1, 1 );
            GL.MatrixMode( MatrixMode.Modelview );
            GL.PushMatrix();
            GL.LoadIdentity();
            { // HUD rendering here
                GL.BindTexture( TextureTarget.Texture2D, shadowTexture );
                GL.UseProgram( shaderProgram );
                renderDebugRectangle();
                GL.UseProgram( 0 );
            }
            GL.Enable( EnableCap.DepthTest );
            GL.DepthMask( true );
            GL.MatrixMode( MatrixMode.Projection );
            GL.PopMatrix();
            GL.MatrixMode( MatrixMode.Modelview );
            GL.PopMatrix();
        }

        private void renderDebugRectangle()
        {
            GL.Enable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Blend );
            GL.Disable( EnableCap.Lighting );
            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, shadowTexture );

            int size = 260;

            GL.Uniform1( modeLoc, 3 );
            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 0, 1 );
                GL.Vertex2( 0, 0 );

                GL.TexCoord2( 0, 0 );
                GL.Vertex2( 0, size );

                GL.TexCoord2( 1, 0 );
                GL.Vertex2( size, size );

                GL.TexCoord2( 1, 1 );
                GL.Vertex2( size, 0 );
            }
            GL.End();

            //GL.Uniform1( modeLoc, Mode );
            //GL.Begin( BeginMode.Quads );
            //{
            //    GL.TexCoord2( 0, 1 );
            //    GL.Vertex2( viewportWidth - size, 0 );

            //    GL.TexCoord2( 0, 0 );
            //    GL.Vertex2( viewportWidth - size, size );

            //    GL.TexCoord2( 1, 0 );
            //    GL.Vertex2( viewportWidth, size );

            //    GL.TexCoord2( 1, 1 );
            //    GL.Vertex2( viewportWidth, 0 );
            //}
            //GL.End();
        }

        private void setTextureMatrix()
        {
            GL.MatrixMode( MatrixMode.Texture );
            GL.ActiveTexture( TextureUnit.Texture7 );

            GL.LoadIdentity();
            GL.MultMatrix( ref opacityMapsRenderer.LightProjectionMatrix );
            GL.MultMatrix( ref opacityMapsRenderer.LightModelViewMatrix );

            // Go back to normal matrix Mode
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
            //GL.ClearColor( Color.Black );

            GL.TexEnv( TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Modulate );
            GL.BlendFunc( BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha );
            GL.ShadeModel( ShadingModel.Smooth );
            GL.Enable( EnableCap.Lighting );
            GL.Enable( EnableCap.Light0 );

            GL.Light( LightName.Light0, LightParameter.Diffuse, lightDiffuse );
            GL.Light( LightName.Light0, LightParameter.Ambient, lightAmbient );
            //GL.Light( LightName.Light0, LightParameter.Specular, lightSpecular );
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