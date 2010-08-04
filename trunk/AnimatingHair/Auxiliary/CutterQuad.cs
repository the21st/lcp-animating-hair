using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace AnimatingHair.Auxiliary
{
    /// <summary>
    /// Represents the quad used for cutting hair.
    /// </summary>
    class CutterQuad
    {
        public Vector3 Position;
        public float RotateX { get; set; }
        public float RotateZ { get; set; }
        public float Size { get; set; }

        public CutterQuad()
        {
            Position = Vector3.Zero;
            RotateX = 0;
            RotateZ = 0;
            Size = 1;
        }

        public Vector3[] GetVertices()
        {
            Vector3[] result = new Vector3[ 4 ];

            Matrix4 translate = Matrix4.CreateTranslation( Position );
            Matrix4 rotateX = Matrix4.CreateRotationX( RotateX );
            Matrix4 rotateZ = Matrix4.CreateRotationZ( RotateZ );

            Matrix4 transform = (rotateZ * rotateX) * translate;

            Vector3 v0, v1, v2, v3;
            v0 = new Vector3( -Size, 0, -Size );
            v1 = new Vector3( -Size, 0, Size );
            v2 = new Vector3( Size, 0, -Size );
            v3 = new Vector3( Size, 0, Size );

            result[ 0 ] = Vector3.Transform( v0, transform );
            result[ 1 ] = Vector3.Transform( v1, transform );
            result[ 2 ] = Vector3.Transform( v2, transform );
            result[ 3 ] = Vector3.Transform( v3, transform );

            return result;
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
            GL.Translate( Position );
            GL.Rotate( MathHelper.RadiansToDegrees( RotateX ), 1, 0, 0 );
            GL.Rotate( MathHelper.RadiansToDegrees( RotateZ ), 0, 0, 1 );
            GL.Scale( Size, Size, Size );
            GL.Begin( BeginMode.Quads );
            {
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
