using AnimatingHair.Rendering;
using OpenTK;

namespace AnimatingHair
{
    /// <summary>
    /// A singleton class containing references to rendering resources, such as textures and meshes.
    /// </summary>
    class RenderingResources
    {
        public static readonly RenderingResources Instance = new RenderingResources();

        public TriangleMesh HeadModel;
        public TriangleMesh ShouldersModel;

        public int DeepOpacityMap;
        public int DeepOpacityMapBlurred;
        public int ShadowMap;

        public Matrix4 HeadModelTransformationMatrix;
        public Matrix4 HeadRotateMatrixInverse;

        public Matrix4 LightModelViewMatrix;
        public Matrix4 LightProjectionMatrix;
        public Matrix4 CameraModelViewMatrix;
    }
}
