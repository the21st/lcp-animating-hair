using System;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace AnimatingHair.Rendering
{
    /// <summary>
    /// Handles the rendering of hair.
    /// </summary>
    class HairRenderer
    {
        // keeps references to object it needs
        private readonly Hair hair;
        private readonly Camera camera;
        private readonly Light light;

        // the openGL texture reference
        private readonly int splatTexture;
        public int DeepOpacityMap;
        public int DepthMap;

        // the shader uniform locations
        private int axisLoc;
        private int eyeLoc;
        private int lightLoc;
        private int sign1Loc;
        private int sign2Loc;
        private int hairTextureLoc;
        private int shadowMapLoc;
        private int depthMapLoc;

        // shader objects
        private readonly int shaderProgram;
        private int vertexShaderObject;
        private int fragmentShaderObject;

        // an auxiliary array of particles for correct rendering of blended billboards
        private readonly HairParticle[] sorted;


        public HairRenderer( Camera camera, Hair hair, Light light )
        {
            this.hair = hair;
            this.camera = camera;
            this.light = light;

            // texture loading
            splatTexture = Utility.UploadTexture( FilePaths.HairTextureLocation );

            // shader loading
            using ( StreamReader vs = new StreamReader( FilePaths.BillboardShaderLocation ) )
            {
                using ( StreamReader fs = new StreamReader( FilePaths.HairShaderLocation ) )
                    createShaders( vs.ReadToEnd(), fs.ReadToEnd(),
                                   out vertexShaderObject, out fragmentShaderObject,
                                   out shaderProgram );
            }

            getShaderVariableLocations();

            sorted = new HairParticle[ hair.Particles.Length ];
            for ( int index = 0; index < hair.Particles.Length; index++ )
            {
                sorted[ index ] = hair.Particles[ index ];
            }
        }

        public void Render()
        {
            GL.Enable( EnableCap.Texture2D );
            GL.Enable( EnableCap.Blend );
            GL.Enable( EnableCap.Lighting );
            GL.Material( MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, hair.Clr );
            renderWithShaders();
        }

        private static int particleCompare( HairParticle hp1, HairParticle hp2 )
        {
            return hp2.DistanceFromCamera.CompareTo( hp1.DistanceFromCamera );
        }

        private void renderWithShaders()
        {
            // link the shader program
            GL.UseProgram( shaderProgram );

            // sort the particles according to distance from camera
            for ( int i = 0; i < hair.Particles.Length; i++ )
            {
                HairParticle hp = hair.Particles[ i ];
                hp.DistanceFromCamera = (hp.Position - camera.Eye).Length;
            }

            Array.Sort( sorted, particleCompare );


            GL.ActiveTexture( TextureUnit.Texture5 );
            GL.BindTexture( TextureTarget.Texture2D, splatTexture );
            GL.Uniform1( hairTextureLoc, 5 );

            GL.ActiveTexture( TextureUnit.Texture6 );
            GL.BindTexture( TextureTarget.Texture2D, DeepOpacityMap );
            GL.Uniform1( shadowMapLoc, 6 );

            GL.ActiveTexture( TextureUnit.Texture7 );
            GL.BindTexture( TextureTarget.Texture2D, DepthMap );
            GL.Uniform1( depthMapLoc, 7 );

            //GL.BindTexture( TextureTarget.Texture2D, splatTexture );

            GL.Uniform3( eyeLoc, camera.Eye );
            GL.Uniform3( lightLoc, light.Position );

            //GL.Disable( EnableCap.Blend );
            //GL.Enable( EnableCap.DepthTest );

            for ( int i = 0; i < sorted.Length; i++ )
            {
                if ( !sorted[ i ].IsRoot )
                    renderParticle( sorted[ i ] );
            }

            // unlink the shader program
            GL.UseProgram( 0 );
        }

        /// <summary>
        /// Renders the specified hair particle.
        /// </summary>
        /// <remarks>
        /// Only sends the same particle coordinate 4 times.
        /// Through uniform variables, the vertex coordinates are 'spread'
        /// into a quad (more specifically, a billboard).
        /// </remarks>
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

        #region Private auxiliary methods

        private void getShaderVariableLocations()
        {
            axisLoc = GL.GetUniformLocation( shaderProgram, "axis" );
            eyeLoc = GL.GetUniformLocation( shaderProgram, "eye" );
            lightLoc = GL.GetUniformLocation( shaderProgram, "light" );
            hairTextureLoc = GL.GetUniformLocation( shaderProgram, "hairTexture" );
            shadowMapLoc = GL.GetUniformLocation( shaderProgram, "deepOpacityMap" );
            depthMapLoc = GL.GetUniformLocation( shaderProgram, "depthMap" );
            sign1Loc = GL.GetAttribLocation( shaderProgram, "sign1" );
            sign2Loc = GL.GetAttribLocation( shaderProgram, "sign2" );
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
