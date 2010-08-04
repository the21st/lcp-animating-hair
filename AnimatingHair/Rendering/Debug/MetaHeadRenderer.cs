using AnimatingHair.Auxiliary;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace AnimatingHair.Rendering.Debug
{
    /// <summary>
    /// Provides debug rendering for the head.
    /// Draws the geometric shapes that provide collision interaction with the particles.
    /// </summary>
    class MetaHeadRenderer
    {
        private static readonly float[] SkinColor = { 0.47f, 0.38f, 0.33f, 1 };
        private static readonly float[] SpecularColor = { 0.1f, 0.05f, 0.05f, 1 };

        private readonly HeadNeckShoulders headNeckShoulders;

        public MetaHeadRenderer( HeadNeckShoulders headNeckShoulders )
        {
            this.headNeckShoulders = headNeckShoulders;
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
            GL.Translate( headNeckShoulders.Position );

            renderHead();
            renderNeck();
            renderShoulders();

            GL.PopMatrix();
            GL.PopAttrib();
        }

        private void renderHead()
        {
            GL.PushMatrix();
            GL.Translate( headNeckShoulders.Head.Center );
            Utility.DrawSphere( headNeckShoulders.Head.Radius, 100, 100 );
            GL.PopMatrix();
        }

        private void renderNeck()
        {
            Utility.RenderCylinder(
                headNeckShoulders.Neck.OriginalEndpoint1.X,
                headNeckShoulders.Neck.OriginalEndpoint1.Y,
                headNeckShoulders.Neck.OriginalEndpoint1.Z,
                headNeckShoulders.Neck.OriginalEndpoint2.X,
                headNeckShoulders.Neck.OriginalEndpoint2.Y,
                headNeckShoulders.Neck.OriginalEndpoint2.Z,
                headNeckShoulders.Neck.Radius,
                100 );
        }

        private void renderShoulders()
        {
            GL.PushMatrix();
            GL.Translate( headNeckShoulders.ShoulderTipLeft.OriginalCenter );
            Utility.DrawSphere( headNeckShoulders.ShoulderTipLeft.Radius, 50, 50 );
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate( headNeckShoulders.ShoulderTipRight.OriginalCenter );
            Utility.DrawSphere( headNeckShoulders.ShoulderTipRight.Radius, 50, 50 );
            GL.PopMatrix();

            Utility.RenderCylinder(
                headNeckShoulders.Shoulders.OriginalEndpoint1.X,
                headNeckShoulders.Shoulders.OriginalEndpoint1.Y,
                headNeckShoulders.Shoulders.OriginalEndpoint1.Z,
                headNeckShoulders.Shoulders.OriginalEndpoint2.X,
                headNeckShoulders.Shoulders.OriginalEndpoint2.Y,
                headNeckShoulders.Shoulders.OriginalEndpoint2.Z,
                headNeckShoulders.Shoulders.Radius,
                100 );
        }
    }
}
