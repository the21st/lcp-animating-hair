using AnimatingHair.Auxiliary;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;

namespace AnimatingHair.Initialization
{
    class HeadInitializer
    {
        private const float headSize = 1.0f;
        private const float shouldersWidth = 1.3f;
        private const float shouldersRadius = 0.66f;
        private const float shoulderPosY = -2.4f;
        private const float shoulderPosZ = -0.47f;

        private HeadNeckShoulders headNeckShoulders;

        public HeadNeckShoulders InitializeHead()
        {
            headNeckShoulders = new HeadNeckShoulders( 1 )
                   {
                       Head = new Sphere(
                           new Vector3( 0, 0, 0 ),
                           headSize
                           ),

                       Neck = new Cylinder(
                           new Vector3( 0, -0.3f, -0.16f ),
                           new Vector3( 0, -2.5f, -0.397f ),
                           0.54f ),

                       Shoulders = new Cylinder(
                           new Vector3( -shouldersWidth, shoulderPosY, shoulderPosZ ),
                           new Vector3( shouldersWidth, shoulderPosY, shoulderPosZ ),
                           shouldersRadius ),

                       ShoulderTipLeft = new Sphere(
                           new Vector3( -shouldersWidth, shoulderPosY, shoulderPosZ ),
                           shouldersRadius ),

                       ShoulderTipRight = new Sphere(
                           new Vector3( shouldersWidth, shoulderPosY, shoulderPosZ ),
                           shouldersRadius ),
                   };

            return headNeckShoulders;
        }
    }
}