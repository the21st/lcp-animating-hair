using System;
using System.IO;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace AnimatingHair.Rendering
{
    class GaussianBlurRenderer
    {
        // the depth shader uniform locations
        private int blurSizeLocHorizontal;
        private int textureLocHorizontal;

        // the opacity shader uniform locations
        private int blurSizeLocVertical;
        private int textureLocVertical;

        // shader objects
        private readonly int horizontalShaderProgram;
        private readonly int verticalShaderProgram;
        private int vertexShaderObject;
        private int fragmentShaderObject;

        private int horizontalFBO, verticalFBO;
        public int horizontalTexture;


        public GaussianBlurRenderer()
        {
            // shader loading
            using ( StreamReader vs = new StreamReader( FilePaths.GaussBlurVSLocation ) )
            using ( StreamReader fs = new StreamReader( FilePaths.GaussBlurHorizontalFSLocation ) )
                horizontalShaderProgram = Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd() );

            using ( StreamReader vs = new StreamReader( FilePaths.GaussBlurVSLocation ) )
            using ( StreamReader fs = new StreamReader( FilePaths.GaussBlurVerticalFSLocation ) )
                verticalShaderProgram = Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd() );

            getShaderVariableLocations();

            generateHorizontalFBO();
            generateVerticalFBO();
        }

        public void BlurDeepOpacityTexture()
        {
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, horizontalFBO );
            GL.Viewport( 0, 0, RenderingOptions.Instance.ShadowMapsResolution, RenderingOptions.Instance.ShadowMapsResolution );
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
            setupMatrices();
            renderHorizontalBlur();
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );

            GL.BindFramebuffer( FramebufferTarget.Framebuffer, verticalFBO );
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
            renderVerticalBlur();
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        private static void renderQuad()
        {
            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 0, 1 );
                GL.Vertex2( 0, 0 );

                GL.TexCoord2( 0, 0 );
                GL.Vertex2( 0, 1 );

                GL.TexCoord2( 1, 0 );
                GL.Vertex2( 1, 1 );

                GL.TexCoord2( 1, 1 );
                GL.Vertex2( 1, 0 );
            }
            GL.End();
        }

        private void renderHorizontalBlur()
        {
            if ( !RenderingOptions.Instance.ShowHair || RenderingOptions.Instance.DebugHair )
                return;

            GL.Disable( EnableCap.DepthTest );
            GL.Disable( EnableCap.Lighting );
            GL.Disable( EnableCap.Blend );
            GL.DepthMask( false );

            GL.Enable( EnableCap.Texture2D );

            // link the shader program
            GL.UseProgram( horizontalShaderProgram );

            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMap );
            GL.Uniform1( textureLocHorizontal, 0 );

            GL.Uniform1( blurSizeLocHorizontal, RenderingOptions.Instance.BlurSize );

            renderQuad();

            // unlink the shader program
            GL.UseProgram( 0 );
        }

        private void renderVerticalBlur()
        {
            if ( !RenderingOptions.Instance.ShowHair || RenderingOptions.Instance.DebugHair )
                return;

            GL.Disable( EnableCap.DepthTest );
            GL.Disable( EnableCap.Lighting );
            GL.Disable( EnableCap.Blend );
            GL.DepthMask( false );

            GL.Enable( EnableCap.Texture2D );
            GL.UseProgram( 0 );

            // link the shader program
            GL.UseProgram( verticalShaderProgram );

            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, horizontalTexture );
            GL.Uniform1( textureLocVertical, 0 );

            GL.Uniform1( blurSizeLocVertical, RenderingOptions.Instance.BlurSize );

            renderQuad();

            // unlink the shader program
            GL.UseProgram( 0 );
        }

        private void generateHorizontalFBO()
        {
            int shadowMapWidth = RenderingOptions.Instance.ShadowMapsResolution;
            int shadowMapHeight = RenderingOptions.Instance.ShadowMapsResolution;

            FramebufferErrorCode FBOstatus;

            horizontalTexture = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, horizontalTexture );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, shadowMapWidth, shadowMapHeight, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, new IntPtr() );
            GL.BindTexture( TextureTarget.Texture2D, 0 );

            // create a framebuffer object
            GL.GenFramebuffers( 1, out horizontalFBO );
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, horizontalFBO );

            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, horizontalTexture, 0 );

            // check FBO status
            FBOstatus = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
            if ( FBOstatus != FramebufferErrorCode.FramebufferComplete )
                throw new Exception( "GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use FBO\n" );

            // switch back to window-system-provided framebuffer
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        private void generateVerticalFBO()
        {
            int shadowMapWidth = RenderingOptions.Instance.ShadowMapsResolution;
            int shadowMapHeight = RenderingOptions.Instance.ShadowMapsResolution;

            FramebufferErrorCode FBOstatus;

            RenderingResources.Instance.DeepOpacityMapBlurred = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMapBlurred );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, shadowMapWidth, shadowMapHeight, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, new IntPtr() );
            GL.BindTexture( TextureTarget.Texture2D, 0 );

            // create a framebuffer object
            GL.GenFramebuffers( 1, out verticalFBO );
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, verticalFBO );

            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMapBlurred, 0 );

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
            GL.LoadIdentity();
            GL.Ortho( 0, 1, 1, 0, -1, 1 );
            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadIdentity();
        }

        private void getShaderVariableLocations()
        {
            blurSizeLocHorizontal = GL.GetUniformLocation( horizontalShaderProgram, "blurSize" );
            textureLocHorizontal = GL.GetUniformLocation( horizontalShaderProgram, "RTScene" );

            blurSizeLocVertical = GL.GetUniformLocation( verticalShaderProgram, "blurSize" );
            textureLocVertical = GL.GetUniformLocation( verticalShaderProgram, "RTBlurH" );
        }
    }
}
