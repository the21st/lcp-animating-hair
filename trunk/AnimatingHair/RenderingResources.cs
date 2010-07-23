using AnimatingHair.Rendering;
using OpenTK;

namespace AnimatingHair
{
    class RenderingResources
    {
        public static readonly RenderingResources Instance = new RenderingResources();

        public TriangleMesh HeadModel;
        public TriangleMesh ShouldersModel;

        public int DeepOpacityMap;
        public int ShadowMap;

        public Matrix4 BustModelTransformationMatrix;
        public Matrix4 HeadRotateMatrixInverse;

        public Matrix4 LightModelViewMatrix;
        public Matrix4 LightProjectionMatrix;
        public Matrix4 CameraModelViewMatrix;
    }
}
