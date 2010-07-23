using System.IO;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AnimatingHair.Rendering
{
    /// <summary>
    /// Handles the rendering of the bust entity.
    /// </summary>
    class BustRenderer
    {
        private static readonly float[] SkinDiffuseColor = { 0.47f, 0.38f, 0.33f, 1 };
        private static readonly float[] SkinSpecularColor = { 0.1f, 0.05f, 0.05f, 1 };

        // keeps references to object it needs
        private readonly Camera camera;
        private readonly Light light;

        private readonly Bust bust;

        // shader objects
        private readonly int shaderProgram;

        // the shader uniform locations
        private int deepOpacityMapLoc;
        private int shadowMapLoc;
        private int deepOpacityMapDistanceLoc;
        private int lightModelViewMatrixLoc;
        private int lightProjectionMatrixLoc;
        private int nearLoc;
        private int farLoc;

        public BustRenderer( Bust bust, Camera camera, Light light )
        {
            this.camera = camera;
            this.light = light;
            this.bust = bust;

            // shader loading
            using ( StreamReader vs = new StreamReader( FilePaths.BustVSLocation ) )
            using ( StreamReader fs = new StreamReader( FilePaths.BustFSLocation ) )
                Utility.CreateShaders( vs.ReadToEnd(), fs.ReadToEnd(), out shaderProgram );

            getShaderVariableLocations();

            RenderingResources.Instance.HeadModel = Utility.LoadOBJ( FilePaths.HeadModelLocation );
            RenderingResources.Instance.ShouldersModel = Utility.LoadOBJ( FilePaths.ShouldersModelLocation );
        }

        public void Render()
        {
            GL.PushAttrib( AttribMask.AllAttribBits );
            GL.Material( MaterialFace.Front, MaterialParameter.Specular, SkinSpecularColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Shininess, 5 );
            GL.Material( MaterialFace.Front, MaterialParameter.Diffuse, SkinDiffuseColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Ambient, SkinDiffuseColor );

            // link the shader program
            GL.UseProgram( shaderProgram );

            GL.ActiveTexture( TextureUnit.Texture1 );
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.DeepOpacityMap );
            GL.Uniform1( deepOpacityMapLoc, 1 );

            GL.ActiveTexture( TextureUnit.Texture2 );
            GL.BindTexture( TextureTarget.Texture2D, RenderingResources.Instance.ShadowMap );
            GL.Uniform1( shadowMapLoc, 2 );

            GL.Uniform1( nearLoc, RenderingOptions.Instance.Near );
            GL.Uniform1( farLoc, RenderingOptions.Instance.Far );
            GL.Uniform1( deepOpacityMapDistanceLoc, RenderingOptions.Instance.DeepOpacityMapDistance );

            Matrix4 translate = Matrix4.CreateTranslation( RenderingOptions.Instance.BustDisplacement );
            Matrix4 scale = Matrix4.Scale( RenderingOptions.Instance.BustScaleRatio );
            Matrix4 tmp = scale * translate * RenderingResources.Instance.BustModelTransformationMatrix *
                          RenderingResources.Instance.LightModelViewMatrix;
            GL.UniformMatrix4( lightModelViewMatrixLoc, false, ref tmp );
            GL.UniformMatrix4( lightProjectionMatrixLoc, false, ref RenderingResources.Instance.LightProjectionMatrix );

            GL.PushMatrix();
            {
                GL.Translate( bust.Position );

                GL.PushMatrix();
                {
                    GL.Rotate( MathHelper.RadiansToDegrees( bust.Angle ), 0, 1, 0 );

                    // scale and translate the model so that it fits on the physical interaction model of the bust
                    GL.Translate( RenderingOptions.Instance.BustDisplacement );
                    GL.Scale( RenderingOptions.Instance.BustScaleRatio, RenderingOptions.Instance.BustScaleRatio,
                              RenderingOptions.Instance.BustScaleRatio );

                    RenderingResources.Instance.HeadModel.Draw();
                }
                GL.PopMatrix();

                GL.Translate( RenderingOptions.Instance.BustDisplacement );
                GL.Scale( RenderingOptions.Instance.BustScaleRatio, RenderingOptions.Instance.BustScaleRatio,
                          RenderingOptions.Instance.BustScaleRatio );

                Matrix4 translate2 = Matrix4.CreateTranslation( bust.Position );
                Matrix4 tmp2 = scale * translate * translate2 * RenderingResources.Instance.LightModelViewMatrix;
                GL.UniformMatrix4( lightModelViewMatrixLoc, false, ref tmp2 );

                RenderingResources.Instance.ShouldersModel.Draw();
            }
            GL.PopMatrix();

            // unlink the shader program
            GL.UseProgram( 0 );

            GL.PopAttrib();
        }

        private void getShaderVariableLocations()
        {
            deepOpacityMapLoc = GL.GetUniformLocation( shaderProgram, "deepOpacityMap" );
            shadowMapLoc = GL.GetUniformLocation( shaderProgram, "shadowMap" );
            deepOpacityMapDistanceLoc = GL.GetUniformLocation( shaderProgram, "deepOpacityMapDistance" );
            lightModelViewMatrixLoc = GL.GetUniformLocation( shaderProgram, "lightModelViewMatrix" );
            lightProjectionMatrixLoc = GL.GetUniformLocation( shaderProgram, "lightProjectionMatrix" );
            nearLoc = GL.GetUniformLocation( shaderProgram, "near" );
            farLoc = GL.GetUniformLocation( shaderProgram, "far" );
        }
    }
}
