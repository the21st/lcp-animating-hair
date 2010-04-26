using System;
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
        private const int opacityMapSize = 512;

        // keeps references to object it needs
        private readonly Hair hair;
        private readonly Light light;

        // the openGL texture reference
        private readonly int splatTexture;

        // the depth shader uniform locations
        private int axisLoc;
        private int eyeLoc;
        private int sign1Loc;
        private int sign2Loc;

        // the opacity shader uniform locations
        private int axisLoc2;
        private int eyeLoc2;
        private int sign1Loc2;
        private int sign2Loc2;
        private int depthMapLoc2;
        private int hairTextureLoc2;
        private int distLoc2;

        // shader objects
        private readonly int depthShaderProgram;
        private readonly int opacityShaderProgram;
        private int vertexShaderObject;
        private int fragmentShaderObject;

        private readonly Vector3 centerPosition;

        public Matrix4 LightProjectionMatrix, LightModelViewMatrix;
        public float Dist = 0.1f;

        public OpacityMapsRenderer( Hair hair, Light light )
        {
            this.hair = hair;
            this.light = light;

            // texture loading
            splatTexture = Utility.UploadTexture( FilePaths.HairTextureLocation );

            centerPosition = calculateCenterPosition();

            // texture loading
            Utility.UploadTexture( FilePaths.HairTextureLocation );

            // shader loading
            using ( StreamReader vs = new StreamReader( FilePaths.DepthShaderLocation ) )
            {
                createShaders( vs.ReadToEnd(), out depthShaderProgram );
            }

            using ( StreamReader vs = new StreamReader( FilePaths.OpacityVSLocation ) )
            {
                using ( StreamReader fs = new StreamReader( FilePaths.OpacityFSLocation ) )
                    createShaders( vs.ReadToEnd(), fs.ReadToEnd(),
                                   out vertexShaderObject, out fragmentShaderObject,
                                   out opacityShaderProgram );
            }

            getShaderVariableLocations();

            generateShadowFBO();
        }

        private void setupMatrices()
        {
            GL.MatrixMode( MatrixMode.Projection );
            GL.LoadMatrix( ref LightProjectionMatrix );

            GL.MatrixMode( MatrixMode.Modelview );
            GL.LoadMatrix( ref LightModelViewMatrix );
        }

        private int FBO, depthTexture;

        private void generateShadowFBO()
        {
            const int shadowMapWidth = opacityMapSize;
            const int shadowMapHeight = opacityMapSize;

            FramebufferErrorCode FBOstatus;

            // Try to use a texture depth component
            depthTexture = GL.GenTexture();
            GL.BindTexture( TextureTarget.Texture2D, depthTexture );

            // GL_LINEAR does not make sense for depth texture. However, next tutorial shows usage of GL_LINEAR and PCF
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest );

            // No need to force GL_DEPTH_COMPONENT24, drivers usually give you the max precision if available 
            GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, shadowMapWidth, shadowMapHeight, 0,
                PixelFormat.DepthComponent, PixelType.UnsignedByte, new IntPtr() );
            GL.BindTexture( TextureTarget.Texture2D, 0 );

            // create a framebuffer object
            GL.GenFramebuffers( 1, out FBO );
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, FBO );

            // Instruct openGL that we won't bind a color texture with the currently binded FBO
            GL.DrawBuffer( DrawBufferMode.None );
            // glReadBuffer( GL_NONE );

            // attach the texture to FBO depth attachment point
            GL.FramebufferTexture2D( FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthTexture, 0 );

            // check FBO status
            FBOstatus = GL.CheckFramebufferStatus( FramebufferTarget.Framebuffer );
            if ( FBOstatus != FramebufferErrorCode.FramebufferComplete )
                throw new Exception( "GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use FBO\n" );

            // switch back to window-system-provided framebuffer
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );
        }

        public void RenderOpacityTexture()
        {
            LightProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView( MathHelper.PiOver4, 1, 1, 30 );
            LightModelViewMatrix = Matrix4.LookAt( light.Position, centerPosition, Vector3.UnitY );

            GL.BindFramebuffer( FramebufferTarget.Framebuffer, FBO );
            GL.Viewport( 0, 0, opacityMapSize, opacityMapSize );
            GL.Clear( ClearBufferMask.DepthBufferBit );
            GL.Clear( ClearBufferMask.DepthBufferBit );
            setupMatrices();
            renderDepthMap();
            GL.BindFramebuffer( FramebufferTarget.Framebuffer, 0 );

            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
            renderOpacityMaps();
        }

        private void renderDepthMap()
        {
            GL.ShadeModel( ShadingModel.Flat );
            GL.ColorMask( false, false, false, false );
            GL.Enable( EnableCap.DepthTest );

            // link the shader program
            GL.UseProgram( depthShaderProgram );

            GL.Uniform3( eyeLoc, light.Position );

            for ( int i = 0; i < hair.Particles.Length; i++ )
            {
                if ( !hair.Particles[ i ].IsRoot )
                    renderParticle( hair.Particles[ i ] );
            }

            // unlink the shader program
            GL.UseProgram( 0 );
            GL.ColorMask( true, true, true, true );
            GL.ShadeModel( ShadingModel.Smooth );
        }

        private void renderOpacityMaps()
        {
            GL.Disable( EnableCap.DepthTest );
            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactorSrc.One, BlendingFactorDest.One );
            GL.Enable( EnableCap.Texture2D );

            // link the shader program
            GL.UseProgram( opacityShaderProgram );

            int loc1 = GL.GetUniformLocation( opacityShaderProgram, "hairTexture" );
            GL.ActiveTexture( TextureUnit.Texture3 );
            GL.BindTexture( TextureTarget.Texture2D, splatTexture );
            //GL.Uniform1( hairTextureLoc2, splatTexture );
            GL.Uniform1( loc1, 3 );

            int loc2 = GL.GetUniformLocation( opacityShaderProgram, "depthMap" );
            GL.ActiveTexture( TextureUnit.Texture4 );
            GL.BindTexture( TextureTarget.Texture2D, depthTexture );
            //GL.Uniform1( depthMapLoc2, 4 );
            GL.Uniform1( loc2, 4 );

            distLoc2 = GL.GetUniformLocation( opacityShaderProgram, "dist" );
            GL.Uniform1( distLoc2, Dist );

            GL.Uniform3( eyeLoc2, light.Position );

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

            axisLoc2 = GL.GetUniformLocation( opacityShaderProgram, "axis" );
            sign1Loc2 = GL.GetAttribLocation( opacityShaderProgram, "sign1" );
            sign2Loc2 = GL.GetAttribLocation( opacityShaderProgram, "sign2" );
            eyeLoc2 = GL.GetUniformLocation( opacityShaderProgram, "eye" );
            depthMapLoc2 = GL.GetUniformLocation( opacityShaderProgram, "depthMap" );
            hairTextureLoc2 = GL.GetUniformLocation( opacityShaderProgram, "hairTexture" );
            distLoc2 = GL.GetUniformLocation( opacityShaderProgram, "distt" );
        }

        #region Private auxiliary methods

        // Creates, compiles and links a vertex shader.
        // Fragment shader is not needed for now; it's commented out.
        private static void createShaders( string vs, out int program )
        {
            int statusCode;
            string info;

            int vertexObject = GL.CreateShader( ShaderType.VertexShader );

            // Compile vertex shader
            GL.ShaderSource( vertexObject, vs );
            GL.CompileShader( vertexObject );
            GL.GetShaderInfoLog( vertexObject, out info );
            GL.GetShader( vertexObject, ShaderParameter.CompileStatus, out statusCode );

            if ( statusCode != 1 )
                throw new ApplicationException( info );

            program = GL.CreateProgram();
            GL.AttachShader( program, vertexObject );

            GL.LinkProgram( program );
        }

        // Creates, compiles and links a vertex shader.
        // Fragment shader is not needed for now; it's commented out.
        private static void createShaders(
            string vs, string fs,
            out int vertexObject, out int fragmentObject,
            out int program )
        {
            int statusCode;
            string info;

            vertexObject = GL.CreateShader( ShaderType.VertexShader );
            fragmentObject = GL.CreateShader( ShaderType.FragmentShader );

            // Compile vertex shader
            GL.ShaderSource( vertexObject, vs );
            GL.CompileShader( vertexObject );
            GL.GetShaderInfoLog( vertexObject, out info );
            GL.GetShader( vertexObject, ShaderParameter.CompileStatus, out statusCode );

            if ( statusCode != 1 )
                throw new ApplicationException( info );

            // Compile fragment shader
            GL.ShaderSource( fragmentObject, fs );
            GL.CompileShader( fragmentObject );
            GL.GetShaderInfoLog( fragmentObject, out info );
            GL.GetShader( fragmentObject, ShaderParameter.CompileStatus, out statusCode );

            if ( statusCode != 1 )
                throw new ApplicationException( info );

            program = GL.CreateProgram();
            GL.AttachShader( program, fragmentObject );
            GL.AttachShader( program, vertexObject );

            GL.LinkProgram( program );
        }

        #endregion
    }
}