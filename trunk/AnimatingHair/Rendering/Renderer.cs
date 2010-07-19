using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using AnimatingHair.Rendering.Debug;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using AnimatingHair.Entity;
using AnimatingHair.Auxiliary;

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
        private readonly CutterQuad cutter;

        // component renderers:
        private readonly HairRenderer hairRenderer;
        private readonly DebugHairRenderer debugHairRenderer;
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

        // shader objects
        private readonly int shaderProgram;
        private readonly int modeLoc;
        public int Mode = 0;

        public Renderer( Camera camera, Scene scene, CutterQuad cutter )
        {
            this.camera = camera;
            this.scene = scene;
            this.cutter = cutter;

            light = new Light
            {
                Intensity = 1.5f,
                Position = new Vector3( 5, 0, 5 )
            };

            hairRenderer = new HairRenderer( camera, scene.Hair, light );
            debugHairRenderer = new DebugHairRenderer( scene.Hair );

            airRenderer = new AirRenderer( scene.Air );

            bustRenderer = new BustRenderer( scene.Bust, camera, light );
            metaBustRenderer = new MetaBustRenderer( scene.Bust );

            voxelGridRenderer = new VoxelGridRenderer( scene.VoxelGrid );

            opacityMapsRenderer = new OpacityMapsRenderer( scene.Hair, light, scene.Bust );

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
            modeLoc = GL.GetUniformLocation( shaderProgram, "mode" );

            initializeOpenGL();
        }

        public void Render()
        {
            Matrix4 translate = Matrix4.CreateTranslation( scene.Bust.Position );
            Matrix4 rotate = Matrix4.CreateRotationY( scene.Bust.Angle );
            RenderingResources.Instance.BustModelTransformationMatrix = rotate * translate;

            light.Intensity = RenderingOptions.Instance.LightIntensity;
            refreshLight();

            if ( RenderingOptions.Instance.LightCruising )
                angle += RenderingOptions.Instance.LightCruiseSpeed;
            light.Position = new Vector3( -RenderingOptions.Instance.LightDistance * (float)Math.Sin( angle ), 0.5f * RenderingOptions.Instance.LightDistance, RenderingOptions.Instance.LightDistance * (float)Math.Cos( angle ) );
            GL.Light( LightName.Light0, LightParameter.Position, new Vector4( light.Position, 1 ) );

            GL.PushAttrib( AttribMask.AllAttribBits );

            opacityMapsRenderer.RenderOpacityTexture();
            shadowTexture = opacityMapsRenderer.ShadowTexture;
            hairRenderer.DeepOpacityMap = shadowTexture;
            bustRenderer.DeepOpacityMap = shadowTexture;

            GL.PopAttrib();

            initializeOpenGL();

            GL.ActiveTexture( TextureUnit.Texture7 ); // NOTE: bez tohto je vsetko vo fixed-pipeline cierne. PRECO?

            renderScene();
        }

        private void renderScene()
        {
            // clears buffer, sets modelview matrix to LookAt from camera
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );

            Matrix4 modelViewMatrix = Matrix4.LookAt( camera.Eye, camera.Target, camera.Up );
            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadMatrix( ref modelViewMatrix );

            RenderingResources.Instance.CameraModelViewMatrix = modelViewMatrix;

            GL.Enable( EnableCap.DepthTest );
            GL.Disable( EnableCap.Blend );
            GL.Disable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Lighting );

            if ( RenderingOptions.Instance.Cutting )
                cutter.Render();

            drawAxes();

            GL.Color3( Color.White );
            GL.PushMatrix();
            GL.Translate( light.Position );
            Utility.DrawSphere( 0.1f, 20, 20 );
            GL.PopMatrix();

            GL.Enable( EnableCap.Lighting );

            GL.PushMatrix();
            {
                GL.MultMatrix( ref RenderingResources.Instance.BustModelTransformationMatrix );

                if ( RenderingOptions.Instance.ShowBust )
                {
                    bustRenderer.Render();
                }

                if ( RenderingOptions.Instance.ShowMetaBust )
                    metaBustRenderer.Render();

                if ( RenderingOptions.Instance.ShowVoxelGrid )
                    voxelGridRenderer.Render();

                if ( RenderingOptions.Instance.ShowDebugAir )
                {
                    airRenderer.Render();
                }

                if ( RenderingOptions.Instance.ShowHair )
                {
                    if ( RenderingOptions.Instance.DebugHair )
                    {
                        debugHairRenderer.Render();
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
            GL.Ortho( 0, RenderingOptions.Instance.RenderWidth, RenderingOptions.Instance.RenderHeight, 0, -1, 1 );
            GL.MatrixMode( MatrixMode.Modelview );
            GL.PushMatrix();
            GL.LoadIdentity();
            { // HUD rendering here
                GL.BindTexture( TextureTarget.Texture2D, shadowTexture );
                GL.UseProgram( shaderProgram );
                //renderDebugRectangle();
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
            GL.BindTexture( TextureTarget.Texture2D, shadowTexture );

            const int size = 250;

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

            GL.Uniform1( modeLoc, Mode );
            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 0, 1 );
                GL.Vertex2( RenderingOptions.Instance.RenderWidth - size, 0 );

                GL.TexCoord2( 0, 0 );
                GL.Vertex2( RenderingOptions.Instance.RenderWidth - size, size );

                GL.TexCoord2( 1, 0 );
                GL.Vertex2( RenderingOptions.Instance.RenderWidth, size );

                GL.TexCoord2( 1, 1 );
                GL.Vertex2( RenderingOptions.Instance.RenderWidth, 0 );
            }
            GL.End();
        }

        public void Resize( int width, int height, float ratio )
        {
            RenderingOptions.Instance.RenderWidth = width;
            RenderingOptions.Instance.RenderHeight = height;
            RenderingOptions.Instance.AspectRatio = ratio;

            refreshViewport();
        }

        private void refreshViewport()
        {
            GL.Viewport( 0, 0, RenderingOptions.Instance.RenderWidth, RenderingOptions.Instance.RenderHeight );
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView( MathHelper.PiOver4, RenderingOptions.Instance.AspectRatio, RenderingOptions.Instance.Near, RenderingOptions.Instance.Far );
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadMatrix( ref perspective );
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