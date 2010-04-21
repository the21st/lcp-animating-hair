using OpenTK;

namespace AnimatingHair.Entity
{
    /// <summary>
    /// Represents the camera, from which the scene is viewed.
    /// </summary>
    class Camera
    {
        /// <summary>
        /// Eye of the camera.
        /// </summary>
        public Vector3 Eye { get; set; }

        /// <summary>
        /// Target of the camera.
        /// </summary>
        public Vector3 Target { get; set; }

        /// <summary>
        /// The up vector of the view
        /// </summary>
        public Vector3 Up { get; set; }

        public Camera()
        {
            Eye = Vector3.Zero;
            Target = Vector3.UnitZ;
            Up = Vector3.UnitY;
        }
    }
}
