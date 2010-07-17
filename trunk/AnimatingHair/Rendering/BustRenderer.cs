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
        private static readonly float[] SkinColor = { 0.47f, 0.38f, 0.33f, 1 };
        private static readonly float[] SpecularColor = { 0.1f, 0.05f, 0.05f, 1 };

        // keeps references to object it needs
        private readonly Camera camera;
        private readonly Light light;

        private readonly Bust bust;
        private readonly TriangleMesh femaleHead;

        // shader objects
        private readonly int shaderProgram;
        public int DeepOpacityMap;

        // the shader uniform locations
        private int eyeLoc;
        private int lightLoc;
        private int deepOpacityMapLoc;

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

            femaleHead = Utility.LoadOBJ( FilePaths.HeadModelLocation );
        }

        public void Render()
        {
            // link the shader program
            GL.UseProgram( shaderProgram );

            GL.PushAttrib( AttribMask.AllAttribBits );
            GL.Material( MaterialFace.Front, MaterialParameter.Specular, SpecularColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Shininess, 5 );
            GL.Material( MaterialFace.Front, MaterialParameter.Diffuse, SkinColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Ambient, SkinColor );
            GL.PushMatrix();
            // scale and translate the model so that it fits on the physical interaction model of the bust
            GL.Translate( 0, -0.62, -0.28 );
            const float scale = 0.88f;
            GL.Scale( scale, scale, scale );

            GL.ActiveTexture( TextureUnit.Texture1 );
            GL.BindTexture( TextureTarget.Texture2D, DeepOpacityMap );
            GL.Uniform1( deepOpacityMapLoc, 1 );

            femaleHead.Draw();
            GL.PopMatrix();
            GL.PopAttrib();

            // unlink the shader program
            GL.UseProgram( 0 );
        }

        private void getShaderVariableLocations()
        {
            eyeLoc = GL.GetUniformLocation( shaderProgram, "eye" );
            lightLoc = GL.GetUniformLocation( shaderProgram, "light" );
            deepOpacityMapLoc = GL.GetUniformLocation( shaderProgram, "deepOpacityMap" );
        }
    }
}
