using System;
using AnimatingHair.Auxiliary;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
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
        public readonly int splatTexture;

        // the shader uniform locations
        private int axisLoc;
        private int eyeLoc;
        private int lightLoc;
        private int hairTextureLoc;
        private int deepOpacityMapLoc;
        private int shadowMapLoc;
        private int billboardWidthLoc;
        private int billboardLengthLoc;
        private int deepOpacityMapDistanceLoc;
        private int nearLoc;
        private int farLoc;
        private int cameraModelViewMatrixLoc;
        private int lightModelViewMatrixLoc;
        private int lightProjectionMatrixLoc;
        private int ambientTermLoc;
        private int diffuseTermLoc;
        private int specularTermLoc;
        private int shininessLoc;
        private int rhoReflectLoc;
        private int rhoTransmitLoc;

        // the shader attribute locations
        private int sign1Loc;
        private int sign2Loc;

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
            using ( StreamReader fs = new StreamReader( FilePaths.HairShaderLocation ) )
                shaderProgram = Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd() );

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
                Vector3 pos = Vector3.Transform( hp.Position, RenderingResources.Instance.HeadModelTransformationMatrix );
                hp.DistanceFromCamera = (pos - camera.Eye).Length;
            }

            Array.Sort( sorted, particleCompare );

            // tieto 2 riadky neviem preco tu musia byt, bez nich mi nevykresli debug obdlzniky (HUD) do bufferu
            GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMap );

            GL.ActiveTexture( TextureUnit.Texture1 );
            GL.BindTexture( TextureTarget.Texture2D, splatTexture );
            GL.Uniform1( hairTextureLoc, 1 );

            GL.ActiveTexture( TextureUnit.Texture2 );
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMap );
            GL.Uniform1( deepOpacityMapLoc, 2 );

            GL.ActiveTexture( TextureUnit.Texture3 );
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.ShadowMap );
            GL.Uniform1( shadowMapLoc, 3 );


            GL.Uniform1( ambientTermLoc, RenderingOptions.Instance.AmbientTerm );
            GL.Uniform1( diffuseTermLoc, RenderingOptions.Instance.DiffuseTerm );
            GL.Uniform1( specularTermLoc, RenderingOptions.Instance.SpecularTerm );
            GL.Uniform1( shininessLoc, RenderingOptions.Instance.Shininess );
            GL.Uniform1( rhoReflectLoc, RenderingOptions.Instance.Reflect );
            GL.Uniform1( rhoTransmitLoc, RenderingOptions.Instance.Transmit );

            GL.Uniform1( billboardWidthLoc, RenderingOptions.Instance.BillboardWidth );
            GL.Uniform1( billboardLengthLoc, RenderingOptions.Instance.BillboardLength );
            GL.Uniform1( deepOpacityMapDistanceLoc, RenderingOptions.Instance.DeepOpacityMapDistance );
            GL.Uniform1( nearLoc, RenderingOptions.Instance.Near );
            GL.Uniform1( farLoc, RenderingOptions.Instance.Far );
            GL.UniformMatrix4( cameraModelViewMatrixLoc, false, ref RenderingResources.Instance.CameraModelViewMatrix );
            RenderingResources.Instance.LightModelViewMatrix = RenderingResources.Instance.HeadModelTransformationMatrix * RenderingResources.Instance.LightModelViewMatrix;
            GL.UniformMatrix4( lightModelViewMatrixLoc, false, ref RenderingResources.Instance.LightModelViewMatrix );
            GL.UniformMatrix4( lightProjectionMatrixLoc, false, ref RenderingResources.Instance.LightProjectionMatrix );

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

        private void getShaderVariableLocations()
        {
            axisLoc = GL.GetUniformLocation( shaderProgram, "axis" );
            eyeLoc = GL.GetUniformLocation( shaderProgram, "eye" );
            lightLoc = GL.GetUniformLocation( shaderProgram, "light" );
            hairTextureLoc = GL.GetUniformLocation( shaderProgram, "hairTexture" );
            deepOpacityMapLoc = GL.GetUniformLocation( shaderProgram, "deepOpacityMap" );
            shadowMapLoc = GL.GetUniformLocation( shaderProgram, "shadowMap" );
            billboardLengthLoc = GL.GetUniformLocation( shaderProgram, "renderSizeVertical" );
            billboardWidthLoc = GL.GetUniformLocation( shaderProgram, "renderSizeHorizontal" );
            deepOpacityMapDistanceLoc = GL.GetUniformLocation( shaderProgram, "deepOpacityMapDistance" );
            nearLoc = GL.GetUniformLocation( shaderProgram, "near" );
            farLoc = GL.GetUniformLocation( shaderProgram, "far" );
            cameraModelViewMatrixLoc = GL.GetUniformLocation( shaderProgram, "cameraModelViewMatrix" );
            lightModelViewMatrixLoc = GL.GetUniformLocation( shaderProgram, "lightModelViewMatrix" );
            lightProjectionMatrixLoc = GL.GetUniformLocation( shaderProgram, "lightProjectionMatrix" );
            ambientTermLoc = GL.GetUniformLocation( shaderProgram, "K_a" );
            diffuseTermLoc = GL.GetUniformLocation( shaderProgram, "K_d" );
            specularTermLoc = GL.GetUniformLocation( shaderProgram, "K_s" );
            shininessLoc = GL.GetUniformLocation( shaderProgram, "shininess" );
            rhoReflectLoc = GL.GetUniformLocation( shaderProgram, "rho_reflect" );
            rhoTransmitLoc = GL.GetUniformLocation( shaderProgram, "rho_transmit" );

            sign1Loc = GL.GetAttribLocation( shaderProgram, "sign1" );
            sign2Loc = GL.GetAttribLocation( shaderProgram, "sign2" );
        }
    }
}
