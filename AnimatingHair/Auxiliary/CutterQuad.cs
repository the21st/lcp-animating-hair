using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace AnimatingHair.Auxiliary
{
    class CutterQuad
    {
        public Vector3 Position { get; set; }
        public float XRotate { get; set; }
        public float ZRotate { get; set; }
        public float Size { get; set; }

        public CutterQuad()
        {
            Position = Vector3.Zero;
            XRotate = 0;
            ZRotate = 0;
            Size = 1;
        }

        public void Render()
        {
            //asd
            GL.Disable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Lighting );
            GL.Disable( EnableCap.Blend );
            GL.Enable( EnableCap.DepthTest );

            GL.Color3( Color.Green );
            GL.PushMatrix();
            GL.Begin( BeginMode.Quads );
            {
                GL.Scale( Size, Size, Size );
                GL.Translate( Position );
                GL.Rotate( XRotate, 1, 0, 0 );
                GL.Rotate( ZRotate, 0, 0, 1 );

                GL.Vertex3( 1, 0, 1 );
                GL.Vertex3( 1, 0, -1 );
                GL.Vertex3( -1, 0, -1 );
                GL.Vertex3( -1, 0, 1 );
            }
            GL.End();
            GL.PopMatrix();
        }
    }
}
