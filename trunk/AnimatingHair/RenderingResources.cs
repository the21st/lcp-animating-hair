using OpenTK;

namespace AnimatingHair
{
    class RenderingResources
    {
        public static readonly RenderingResources Instance = new RenderingResources();

        public Matrix4 BustModelTransformationMatrix;

        public Matrix4 LightModelViewMatrix;
        public Matrix4 LightProjectionMatrix;
        public Matrix4 CameraModelViewMatrix;
    }
}
