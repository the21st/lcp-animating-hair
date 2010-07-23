using System;
using System.IO;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AnimatingHair.Rendering
{
    class ShadowMapRenderer
    {
        private readonly Bust bust;

        private int depthFBO;

        public ShadowMapRenderer( Bust bust )
        {
            this.bust = bust;

            generateDepthFBO();
        }

        private void setupMatrices()
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadMatrix( ref RenderingResources.Instance.LightProjectionMatrix );

            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadMatrix( ref RenderingResources.Instance.LightModelViewMatrix );
        }

        private void generateDepthFBO()
        {
            int shadowMapWidth = RenderingOptions.Instance.ShadowMapsResolution;
            int shadowMapHeight = RenderingOptions.Instance.ShadowMapsResolution;

            FramebufferErrorCode FBOstatus;

            // Try to use a texture depth component
            RenderingResources.Instance.ShadowMap = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.ShadowMap );

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
            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, RenderingResources.Instance.ShadowMap, 0 );

            // check depthFBO status
            FBOstatus = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
            if ( FBOstatus != FramebufferErrorCode.FramebufferComplete )
                throw new Exception( "GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use depthFBO\n" );

            // switch back to window-system-provided framebuffer
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        public void RenderShadowTexture()
        {
            //GL.ActiveTexture( TextureUnit.Texture0 ); // NOTE: jak funguje tento prikaz ?? skus vypnut / zapnut

            GL.BindFramebuffer( FramebufferTarget.Framebuffer, depthFBO );
            GL.Viewport( 0, 0, RenderingOptions.Instance.ShadowMapsResolution, RenderingOptions.Instance.ShadowMapsResolution );
            GL.Clear( ClearBufferMask.DepthBufferBit );
            setupMatrices();
            renderDepthMap();
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        private void renderDepthMap()
        {
            if ( !RenderingOptions.Instance.ShowBust )
                return;

            GL.ShadeModel( ShadingModel.Flat );
            GL.ColorMask( false, false, false, false );
            GL.Enable( EnableCap.DepthTest );
            GL.Disable( EnableCap.Texture2D );

            GL.CullFace( CullFaceMode.Back );

            GL.PushMatrix();
            {
                GL.PushMatrix();
                {
                    GL.Translate( bust.Position );

                    GL.PushMatrix();
                    {
                        GL.Rotate( MathHelper.RadiansToDegrees( bust.Angle ), 0, 1, 0 );

                        // scale and translate the model so that it fits on the physical interaction model of the bust
                        GL.Translate( RenderingOptions.Instance.BustDisplacement );
                        GL.Scale( RenderingOptions.Instance.BustScaleRatio, RenderingOptions.Instance.BustScaleRatio, RenderingOptions.Instance.BustScaleRatio );

                        RenderingResources.Instance.HeadModel.Draw();
                    }
                    GL.PopMatrix();

                    GL.Translate( RenderingOptions.Instance.BustDisplacement );
                    GL.Scale( RenderingOptions.Instance.BustScaleRatio, RenderingOptions.Instance.BustScaleRatio, RenderingOptions.Instance.BustScaleRatio );
                    RenderingResources.Instance.ShouldersModel.Draw();
                }
                GL.PopMatrix();
            }
            GL.PopMatrix();

            GL.BindTexture( TextureTarget.Texture2D, 0 );
            GL.ColorMask( true, true, true, true );
            GL.ShadeModel( ShadingModel.Smooth );
        }
    }
}
