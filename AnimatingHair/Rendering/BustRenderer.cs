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
        private static readonly float[] SkinColor = { 0.93f, 0.76f, 0.66f, 1 };

        private readonly Bust bust;
        private readonly TriangleMesh femaleHead;
        private int eyeballTexture;
        private readonly float[] specularColor;

        public bool Wireframe { get; set; }

        public BustRenderer( Bust bust )
        {
            this.bust = bust;

            femaleHead = Utility.LoadOBJ( FilePaths.HeadModelLocation );
            eyeballTexture = Utility.UploadTexture( FilePaths.EyeballTextureLocation );

            specularColor = new float[] { 0.1f, 0.05f, 0.05f };
        }

        public void Render()
        {
            femaleHead.Wireframe = Wireframe;

            GL.PushAttrib( AttribMask.AllAttribBits );
            GL.Material( MaterialFace.Front, MaterialParameter.Specular, specularColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Shininess, 5 );
            GL.Material( MaterialFace.Front, MaterialParameter.Diffuse, SkinColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Ambient, SkinColor );
            GL.PushMatrix();
            GL.Translate( bust.Position );
            GL.Rotate( MathHelper.RadiansToDegrees( bust.Angle), Vector3.UnitY );
            // scale and translate the model so that it fits on the physical interaction model of the bust
            GL.Translate( 0, -0.62, -0.28 );
            const float scale = 0.88f;
            GL.Scale( scale, scale, scale );
            femaleHead.Draw();
            GL.PopMatrix();
            GL.PopAttrib();
        }
    }
}
