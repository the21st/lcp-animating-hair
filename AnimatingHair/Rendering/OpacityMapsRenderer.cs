﻿using System;
using System.Drawing;
using System.IO;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AnimatingHair.Rendering
{
    class OpacityMapsRenderer
    {
        private const int opacityMapSize = 1024;

        // keeps references to object it needs
        private readonly Hair hair;
        private readonly Light light;
        private readonly Camera camera;

        // the openGL texture reference
        private readonly int splatTexture;

        // the depth shader uniform locations
        private int axisLoc;
        private int eyeLoc;
        private int sign1Loc;
        private int sign2Loc;
        private int alphaTresholdLoc;

        // the opacity shader uniform locations
        private int axisLoc2;
        private int eyeLoc2;
        private int sign1Loc2;
        private int sign2Loc2;
        private int depthMapLoc2;
        private int hairTextureLoc2;
        private int distLoc2;
        private int alphaTresholdLoc2;
        private int intensityFactorLoc2;

        // shader objects
        private readonly int depthShaderProgram;
        private readonly int opacityShaderProgram;
        private int vertexShaderObject;
        private int fragmentShaderObject;

        private Vector3 centerPosition;

        private int depthFBO, shadowFBO;
        public int ShadowTexture, DepthTexture;

        public Matrix4 LightProjectionMatrix, LightModelViewMatrix;
        public float Dist = 0.1f;
        public float IntensityFactor = 1f;
        private float near = 1, far = 30;

        public OpacityMapsRenderer( Hair hair, Light light, Camera camera )
        {
            this.hair = hair;
            this.light = light;
            this.camera = camera;

            // texture loading
            splatTexture = Utility.UploadTexture( FilePaths.HairTextureLocation );

            centerPosition = calculateCenterPosition();

            // shader loading
            using ( StreamReader vs = new StreamReader( FilePaths.DepthVSLocation ) )
            using ( StreamReader fs = new StreamReader( FilePaths.DepthFSLocation ) )
                Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd(), out depthShaderProgram );

            using ( StreamReader vs = new StreamReader( FilePaths.OpacityVSLocation ) )
            using ( StreamReader fs = new StreamReader( FilePaths.OpacityFSLocation ) )
                Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd(), out opacityShaderProgram );

            getShaderVariableLocations();

            generateDepthFBO();
            generateShadowMapFBO();
        }

        private void setupMatrices()
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadMatrix( ref LightProjectionMatrix );

            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadMatrix( ref LightModelViewMatrix );
        }

        private void generateDepthFBO()
        {
            const int shadowMapWidth = opacityMapSize;
            const int shadowMapHeight = opacityMapSize;

            FramebufferErrorCode FBOstatus;

            // Try to use a texture depth component
            DepthTexture = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, DepthTexture );

            // GL_LINEAR does not make sense for depth texture. However, next tutorial shows usage of GL_LINEAR and PCF
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
            // glReadBuffer( GL_NONE );

            // attach the texture to depthFBO depth attachment point
            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, DepthTexture, 0 );

            // check depthFBO status
            FBOstatus = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
            if ( FBOstatus != FramebufferErrorCode.FramebufferComplete )
                throw new Exception( "GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use depthFBO\n" );

            // switch back to window-system-provided framebuffer
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        private void generateShadowMapFBO()
        {
            const int shadowMapWidth = opacityMapSize;
            const int shadowMapHeight = opacityMapSize;

            FramebufferErrorCode FBOstatus;

            ShadowTexture = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, ShadowTexture );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest );

            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, shadowMapWidth, shadowMapHeight, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, new IntPtr() );
            GL.BindTexture( TextureTarget.Texture2D, 0 );

            // create a framebuffer object
            GL.GenFramebuffers( 1, out shadowFBO );
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, shadowFBO );

            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, ShadowTexture, 0 );

            // check FBO status
            FBOstatus = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
            if ( FBOstatus != FramebufferErrorCode.FramebufferComplete )
                throw new Exception( "GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use shadowFBO\n" );

            // switch back to window-system-provided framebuffer
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        public void RenderOpacityTexture()
        {
            LightProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView( MathHelper.PiOver4, 1, near, far );
            LightModelViewMatrix = Matrix4.LookAt( light.Position, centerPosition, Vector3.UnitY );
            //LightModelViewMatrix = Matrix4.LookAt( light.Position, Vector3.Zero, Vector3.UnitY );

            GL.ClearColor( 0, 0, 0, 1 );
            GL.ActiveTexture( TextureUnit.Texture0 ); // NOTE: jak funguje tento prikaz ?? skus vypnut / zapnut

            GL.BindFramebuffer( FramebufferTarget.Framebuffer, depthFBO );
            GL.Viewport( 0, 0, opacityMapSize, opacityMapSize );
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
            //GL.ShadeModel( ShadingModel.Flat ); // NOTE: performance boost?
            GL.ColorMask( false, false, false, false );
            GL.Enable( EnableCap.DepthTest );
            GL.Enable( EnableCap.Texture2D );

            // link the shader program
            GL.UseProgram( depthShaderProgram );

            GL.BindTexture( TextureTarget.Texture2D, splatTexture );

            GL.Uniform3( eyeLoc, light.Position );
            //GL.Uniform3( eyeLoc, camera.Eye );

            GL.Uniform1( alphaTresholdLoc, RenderingOptions.Instance.AlphaTreshold );

            for ( int i = 0; i < hair.Particles.Length; i++ )
            {
                if ( !hair.Particles[ i ].IsRoot )
                    renderParticle( hair.Particles[ i ] );
            }

            // unlink the shader program
            GL.UseProgram( 0 );
            GL.BindTexture( TextureTarget.Texture2D, 0 );
            GL.ColorMask( true, true, true, true );
            //GL.ShadeModel( ShadingModel.Smooth ); // NOTE: performance boost?
        }

        private void renderOpacityMaps()
        {
            GL.Disable( EnableCap.DepthTest );
            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactorSrc.One, BlendingFactorDest.One );
            GL.BlendEquationSeparate( BlendEquationMode.FuncAdd, BlendEquationMode.Min );
            GL.Enable( EnableCap.Texture2D );

            // link the shader program
            GL.UseProgram( opacityShaderProgram );

            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, splatTexture );
            GL.Uniform1( hairTextureLoc2, 0 );

            GL.ActiveTexture( TextureUnit.Texture1 );
            GL.BindTexture( TextureTarget.Texture2D, DepthTexture );
            GL.Uniform1( depthMapLoc2, 1 );

            distLoc2 = GL.GetUniformLocation( opacityShaderProgram, "dist" );
            GL.Uniform1( distLoc2, Dist );
            GL.Uniform1( alphaTresholdLoc2, RenderingOptions.Instance.AlphaTreshold );
            GL.Uniform1( intensityFactorLoc2, IntensityFactor );

            GL.Uniform3( eyeLoc2, light.Position );
            //GL.Uniform3( eyeLoc2, camera.Eye );

            for ( int i = 0; i < hair.Particles.Length; i++ )
            {
                if ( !hair.Particles[ i ].IsRoot )
                    renderParticle2( hair.Particles[ i ] );
            }

            // unlink the shader program
            GL.UseProgram( 0 );
        }

        private void renderParticle( HairParticle hp )
        {
            GL.Uniform3( axisLoc, hp.Direction );

            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 0, 1 );
                GL.VertexAttrib1( sign1Loc, 1 );
                GL.VertexAttrib1( sign2Loc, -1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 0, 0 );
                GL.VertexAttrib1( sign1Loc, 1 );
                GL.VertexAttrib1( sign2Loc, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 0 );
                GL.VertexAttrib1( sign1Loc, -1 );
                GL.VertexAttrib1( sign2Loc, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 1 );
                GL.VertexAttrib1( sign1Loc, -1 );
                GL.VertexAttrib1( sign2Loc, -1 );
                GL.Vertex3( hp.Position );
            }
            GL.End();
        }

        private void renderParticle2( HairParticle hp )
        {
            GL.Uniform3( axisLoc2, hp.Direction );

            GL.Begin( BeginMode.Quads );
            {
                GL.TexCoord2( 0, 1 );
                GL.VertexAttrib1( sign1Loc2, 1 );
                GL.VertexAttrib1( sign2Loc2, -1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 0, 0 );
                GL.VertexAttrib1( sign1Loc2, 1 );
                GL.VertexAttrib1( sign2Loc2, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 0 );
                GL.VertexAttrib1( sign1Loc2, -1 );
                GL.VertexAttrib1( sign2Loc2, 1 );
                GL.Vertex3( hp.Position );

                GL.TexCoord2( 1, 1 );
                GL.VertexAttrib1( sign1Loc2, -1 );
                GL.VertexAttrib1( sign2Loc2, -1 );
                GL.Vertex3( hp.Position );
            }
            GL.End();
        }

        private Vector3 calculateCenterPosition()
        {
            Vector3 sum = Vector3.Zero;
            for ( int i = 0; i < hair.Particles.Length; i++ )
            {
                sum += hair.Particles[ i ].Position;
            }
            return sum / hair.Particles.Length;
        }

        private void getShaderVariableLocations()
        {
            axisLoc = GL.GetUniformLocation( depthShaderProgram, "axis" );
            sign1Loc = GL.GetAttribLocation( depthShaderProgram, "sign1" );
            sign2Loc = GL.GetAttribLocation( depthShaderProgram, "sign2" );
            eyeLoc = GL.GetUniformLocation( depthShaderProgram, "eye" );
            alphaTresholdLoc = GL.GetUniformLocation( depthShaderProgram, "alphaTreshold" );

            axisLoc2 = GL.GetUniformLocation( opacityShaderProgram, "axis" );
            sign1Loc2 = GL.GetAttribLocation( opacityShaderProgram, "sign1" );
            sign2Loc2 = GL.GetAttribLocation( opacityShaderProgram, "sign2" );
            eyeLoc2 = GL.GetUniformLocation( opacityShaderProgram, "eye" );
            depthMapLoc2 = GL.GetUniformLocation( opacityShaderProgram, "depthMap" );
            hairTextureLoc2 = GL.GetUniformLocation( opacityShaderProgram, "hairTexture" );
            distLoc2 = GL.GetUniformLocation( opacityShaderProgram, "distt" );
            alphaTresholdLoc2 = GL.GetUniformLocation( opacityShaderProgram, "alphaTreshold" );
            intensityFactorLoc2 = GL.GetUniformLocation( opacityShaderProgram, "intensityFactor" );
        }
    }
}