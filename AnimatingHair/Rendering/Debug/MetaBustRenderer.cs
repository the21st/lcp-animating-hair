using AnimatingHair.Entity.PhysicalEntity;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace AnimatingHair.Rendering.Debug
{
    /// <summary>
    /// Provides debug rendering for the bust.
    /// Draws the geometric shapes that provide collision interaction with the particles.
    /// </summary>
    class MetaBustRenderer
    {
        private static readonly float[] SkinColor = { 0.47f, 0.38f, 0.33f, 1 };
        private static readonly float[] SpecularColor = { 0.1f, 0.05f, 0.05f, 1 };

        private readonly Bust bust;

        public MetaBustRenderer( Bust bust )
        {
            this.bust = bust;
        }

        /// <summary>
        /// Draws the head (sphere) + neck (cylinder) + ShouldersModel (capsule)
        /// representation
        /// </summary>
        public void Render()
        {
            GL.PushAttrib( AttribMask.AllAttribBits );

            GL.Material( MaterialFace.Front, MaterialParameter.Specular, SpecularColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Shininess, 5 );
            GL.Material( MaterialFace.Front, MaterialParameter.Diffuse, SkinColor );
            GL.Material( MaterialFace.Front, MaterialParameter.Ambient, SkinColor );

            GL.PushMatrix();
            GL.Translate( bust.Position );

            renderHead();
            renderNeck();
            renderShoulders();

            GL.PopMatrix();
            GL.PopAttrib();
        }

        private void renderHead()
        {
            GL.PushMatrix();
            GL.Translate( bust.Head.Center );
            Utility.DrawSphere( bust.Head.Radius, 100, 100 );
            GL.PopMatrix();
        }

        private void renderNeck()
        {
            Utility.RenderCylinder(
                bust.Neck.OriginalEndpoint1.X,
                bust.Neck.OriginalEndpoint1.Y,
                bust.Neck.OriginalEndpoint1.Z,
                bust.Neck.OriginalEndpoint2.X,
                bust.Neck.OriginalEndpoint2.Y,
                bust.Neck.OriginalEndpoint2.Z,
                bust.Neck.Radius,
                100 );
        }

        private void renderShoulders()
        {
            GL.PushMatrix();
            GL.Translate( bust.ShoulderTipLeft.OriginalCenter );
            Utility.DrawSphere( bust.ShoulderTipLeft.Radius, 50, 50 );
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate( bust.ShoulderTipRight.OriginalCenter );
            Utility.DrawSphere( bust.ShoulderTipRight.Radius, 50, 50 );
            GL.PopMatrix();

            Utility.RenderCylinder(
                bust.Shoulders.OriginalEndpoint1.X,
                bust.Shoulders.OriginalEndpoint1.Y,
                bust.Shoulders.OriginalEndpoint1.Z,
                bust.Shoulders.OriginalEndpoint2.X,
                bust.Shoulders.OriginalEndpoint2.Y,
                bust.Shoulders.OriginalEndpoint2.Z,
                bust.Shoulders.Radius,
                100 );
        }
    }
}
