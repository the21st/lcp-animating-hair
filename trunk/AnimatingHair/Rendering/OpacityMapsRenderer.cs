using System;
using System.IO;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AnimatingHair.Rendering
{
    class OpacityMapsRenderer
    {
        // keeps references to object it needs
        private readonly Hair hair;
        private readonly Light light;

        // the openGL texture reference
        private readonly int splatTexture;

        // the depth shader uniform locations
        private int axisLocDepth;
        private int eyeLocDepth;
        private int sign1LocDepth;
        private int sign2LocDepth;
        private int alphaTresholdLocDepth;
        private int billboardWidthLocDepth;
        private int billboardLengthLocDepth;
        private int lightModelViewMatrixLocDepth;

        // the opacity shader uniform locations
        private int axisLocOpacity;
        private int eyeLocOpacity;
        private int sign1LocOpacity;
        private int sign2LocOpacity;
        private int depthMapLocOpacity;
        private int hairTextureLocOpacity;
        private int alphaTresholdLocOpacity;
        private int intensityFactorLocOpacity;
        private int billboardWidthLocOpacity;
        private int billboardLengthLocOpacity;
        private int deepOpacityMapDistanceLocOpacity;
        private int nearLocOpacity;
        private int farLocOpacity;
        private int deepOpacityMapResolutionLocOpacity;
        private int lightModelViewMatrixLocOpacity;

        // shader objects
        private readonly int depthShaderProgram;
        private readonly int opacityShaderProgram;

        private int depthFBO, shadowFBO;
        private int depthTexture;

        public float IntensityFactor = 1f;

        public OpacityMapsRenderer( Hair hair, Light light )
        {
            this.hair = hair;
            this.light = light;

            // texture loading
            splatTexture = Utility.UploadTexture( FilePaths.HairTextureLocation );

            // shader loading
            using ( StreamReader vs = new StreamReader( FilePaths.DepthVSLocation ) )
            using ( StreamReader fs = new StreamReader( FilePaths.DepthFSLocation ) )
                depthShaderProgram = Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd() );

            using ( StreamReader vs = new StreamReader( FilePaths.OpacityVSLocation ) )
            using ( StreamReader fs = new StreamReader( FilePaths.OpacityFSLocation ) )
                opacityShaderProgram = Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd() );

            getShaderVariableLocations();

            generateDepthFBO();
            generateShadowMapFBO();
        }

        public void RenderOpacityTexture()
        {
            GL.ClearColor( 0, 0, 0, 1 );
            GL.ActiveTexture( TextureUnit.Texture0 ); // NOTE: jak funguje tento prikaz ?? skus vypnut / zapnut

            GL.BindFramebuffer( FramebufferTarget.Framebuffer, depthFBO );
            GL.Viewport( 0, 0, RenderingOptions.Instance.ShadowMapsResolution, RenderingOptions.Instance.ShadowMapsResolution );
            GL.Clear( ClearBufferMask.DepthBufferBit );
            setupMatrices();
            renderDepthMap();
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );


            GL.BindFramebuffer( FramebufferTarget.Framebuffer, shadowFBO );
            //GL.Viewport( 0, 0, opacityMapSize, opacityMapSize );
            GL.Clear( ClearBufferMask.ColorBufferBit );
            //setupMatrices();
            renderOpacityMaps();
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, 0 );
        }

        private void renderDepthMap()
        {
            if ( !RenderingOptions.Instance.ShowHair || RenderingOptions.Instance.DebugHair )
                return;

            //GL.ShadeModel( ShadingModel.Flat ); // NOTE: performance boost?
            GL.ColorMask( false, false, false, false );
            GL.Enable( EnableCap.DepthTest );
            GL.Enable( EnableCap.Texture2D );

            // link the shader program
            GL.UseProgram( depthShaderProgram );

            GL.BindTexture( TextureTarget.Texture2D, splatTexture );

            GL.Uniform3( eyeLocDepth, light.Position );
            //GL.Uniform3( eyeLocDepth, camera.Eye );

            GL.Uniform1( billboardWidthLocDepth, RenderingOptions.Instance.BillboardWidth );
            GL.Uniform1( billboardLengthLocDepth, RenderingOptions.Instance.BillboardLength );
            GL.UniformMatrix4( lightModelViewMatrixLocDepth, false, ref RenderingResources.Instance.LightModelViewMatrix );

            GL.Uniform1( alphaTresholdLocDepth, RenderingOptions.Instance.AlphaTreshold );

            GL.PushMatrix();
            {
                GL.MultMatrix( ref RenderingResources.Instance.BustModelTransformationMatrix );

                for ( int i = 0; i < hair.Particles.Length; i++ )
                {
                    if ( !hair.Particles[ i ].IsRoot )
                        renderParticle( hair.Particles[ i ] );
                }
            }
            GL.PopMatrix();

            // unlink the shader program
            GL.UseProgram( 0 );
            GL.BindTexture( TextureTarget.Texture2D, 0 );
            GL.ColorMask( true, true, true, true );
            //GL.ShadeModel( ShadingModel.Smooth ); // NOTE: performance boost?
        }

        private void renderOpacityMaps()
        {
            if ( !RenderingOptions.Instance.ShowHair || RenderingOptions.Instance.DebugHair )
                return;

            GL.Disable( EnableCap.DepthTest );
            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactorSrc.One, BlendingFactorDest.One );
            GL.BlendEquationSeparate( BlendEquationMode.FuncAdd, BlendEquationMode.Min );
            GL.Enable( EnableCap.Texture2D );

            // link the shader program
            GL.UseProgram( opacityShaderProgram );

            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, splatTexture );
            GL.Uniform1( hairTextureLocOpacity, 0 );

            GL.ActiveTexture( TextureUnit.Texture1 );
            GL.BindTexture( TextureTarget.Texture2D, depthTexture );
            GL.Uniform1( depthMapLocOpacity, 1 );

            GL.Uniform1( billboardWidthLocOpacity, RenderingOptions.Instance.BillboardWidth );
            GL.Uniform1( billboardLengthLocOpacity, RenderingOptions.Instance.BillboardLength );
            GL.Uniform1( deepOpacityMapDistanceLocOpacity, RenderingOptions.Instance.DeepOpacityMapDistance );
            GL.Uniform1( nearLocOpacity, RenderingOptions.Instance.Near );
            GL.Uniform1( farLocOpacity, RenderingOptions.Instance.Far );
            GL.Uniform1( deepOpacityMapResolutionLocOpacity, (float)RenderingOptions.Instance.ShadowMapsResolution );
            GL.UniformMatrix4( lightModelViewMatrixLocOpacity, false, ref RenderingResources.Instance.LightModelViewMatrix );

            GL.Uniform1( alphaTresholdLocOpacity, RenderingOptions.Instance.AlphaTreshold );
            GL.Uniform1( intensityFactorLocOpacity, IntensityFactor );

            GL.Uniform3( eyeLocOpacity, light.Position );
            //GL.Uniform3( eyeLocOpacity, camera.Eye );

            GL.PushMatrix();
            {
                GL.MultMatrix( ref RenderingResources.Instance.BustModelTransformationMatrix );

                for ( int i = 0; i < hair.Particles.Length; i++ )
                {
                    if ( !hair.Particles[ i ].IsRoot )
                        renderParticle2( hair.Particles[ i ] );
                }
            }
            GL.PopMatrix();

            // unlink the shader program
            GL.UseProgram( 0 );
        }

        private void renderParticle( HairParticle hp )
        {
            GL.Uniform3( axisLocDepth, hp.Direction );

            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 0, 1 );
                GL.VertexAttrib1( sign1LocDepth, 1 );
                GL.VertexAttrib1( sign2LocDepth, -1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 0, 0 );
                GL.VertexAttrib1( sign1LocDepth, 1 );
                GL.VertexAttrib1( sign2LocDepth, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 0 );
                GL.VertexAttrib1( sign1LocDepth, -1 );
                GL.VertexAttrib1( sign2LocDepth, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 1 );
                GL.VertexAttrib1( sign1LocDepth, -1 );
                GL.VertexAttrib1( sign2LocDepth, -1 );
                GL.Vertex3( hp.Position );
            }
            GL.End();
        }

        private void renderParticle2( HairParticle hp )
        {
            GL.Uniform3( axisLocOpacity, hp.Direction );

            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 0, 1 );
                GL.VertexAttrib1( sign1LocOpacity, 1 );
                GL.VertexAttrib1( sign2LocOpacity, -1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 0, 0 );
                GL.VertexAttrib1( sign1LocOpacity, 1 );
                GL.VertexAttrib1( sign2LocOpacity, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 0 );
                GL.VertexAttrib1( sign1LocOpacity, -1 );
                GL.VertexAttrib1( sign2LocOpacity, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 1 );
                GL.VertexAttrib1( sign1LocOpacity, -1 );
                GL.VertexAttrib1( sign2LocOpacity, -1 );
                GL.Vertex3( hp.Position );
            }
            GL.End();
        }

        private void generateDepthFBO()
        {
            int shadowMapWidth = RenderingOptions.Instance.ShadowMapsResolution;
            int shadowMapHeight = RenderingOptions.Instance.ShadowMapsResolution;

            FramebufferErrorCode FBOstatus;

            // Try to use a texture depth component
            depthTexture = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, depthTexture );

            // GL_LINEAR does not make sense for depth texture
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest );

            // No need to force GL_DEPTH_COMPONENT24, drivers usually give you the max precision if available 
            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, shadowMapWidth, shadowMapHeight, 0,
                PixelFormat.DepthComponent, PixelType.UnsignedByte, new IntPtr() );
            GL.BindTexture( TextureTarget.Texture2D, 0 );

            // create a framebuffer object
            GL.GenFramebuffers( 1, out depthFBO );
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, depthFBO );

            // Instruct openGL that we won't bind a color texture with the currently binded depthFBO
            GL.DrawBuffer( DrawBufferMode.None );

            // attach the texture to depthFBO depth attachment point
            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthTexture, 0 );

            // check depthFBO status
            FBOstatus = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
            if ( FBOstatus != FramebufferErrorCode.FramebufferComplete )
                throw new Exception( "GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use depthFBO\n" );

            // switch back to window-system-provided framebuffer
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        private void generateShadowMapFBO()
        {
            int shadowMapWidth = RenderingOptions.Instance.ShadowMapsResolution;
            int shadowMapHeight = RenderingOptions.Instance.ShadowMapsResolution;

            FramebufferErrorCode FBOstatus;

            RenderingResources.Instance.DeepOpacityMap = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMap );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, shadowMapWidth, shadowMapHeight, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, new IntPtr() );
            GL.BindTexture( TextureTarget.Texture2D, 0 );

            // create a framebuffer object
            GL.GenFramebuffers( 1, out shadowFBO );
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, shadowFBO );

            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMap, 0 );

            // check FBO status
            FBOstatus = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
            if ( FBOstatus != FramebufferErrorCode.FramebufferComplete )
                throw new Exception( "GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use FBO\n" );

            // switch back to window-system-provided framebuffer
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        private static void setupMatrices()
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadMatrix( ref RenderingResources.Instance.LightProjectionMatrix );

            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadMatrix( ref RenderingResources.Instance.LightModelViewMatrix );
        }

        private void getShaderVariableLocations()
        {
            axisLocDepth = GL.GetUniformLocation( depthShaderProgram, "axis" );
            sign1LocDepth = GL.GetAttribLocation( depthShaderProgram, "sign1" );
            sign2LocDepth = GL.GetAttribLocation( depthShaderProgram, "sign2" );
            eyeLocDepth = GL.GetUniformLocation( depthShaderProgram, "eye" );
            alphaTresholdLocDepth = GL.GetUniformLocation( depthShaderProgram, "alphaTreshold" );
            billboardLengthLocDepth = GL.GetUniformLocation( depthShaderProgram, "renderSizeVertical" );
            billboardWidthLocDepth = GL.GetUniformLocation( depthShaderProgram, "renderSizeHorizontal" );
            lightModelViewMatrixLocDepth = GL.GetUniformLocation( depthShaderProgram, "lightModelViewMatrix" );

            axisLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "axis" );
            sign1LocOpacity = GL.GetAttribLocation( opacityShaderProgram, "sign1" );
            sign2LocOpacity = GL.GetAttribLocation( opacityShaderProgram, "sign2" );
            eyeLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "eye" );
            depthMapLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "depthMap" );
            hairTextureLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "hairTexture" );
            alphaTresholdLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "alphaTreshold" );
            intensityFactorLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "intensityFactor" );
            billboardLengthLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "renderSizeVertical" );
            billboardWidthLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "renderSizeHorizontal" );
            deepOpacityMapDistanceLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "deepOpacityMapDistance" );
            nearLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "near" );
            farLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "far" );
            deepOpacityMapResolutionLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "resolution" );
            lightModelViewMatrixLocOpacity = GL.GetUniformLocation( opacityShaderProgram, "lightModelViewMatrix" );
        }
    }
}