using OpenTK;

namespace AnimatingHair.Entity
{
    /// <summary>
    /// Represents a white light source in the scene.
    /// It is just a data object, does not handle GL function calls.
    /// </summary>
    class Light
    {
        public Vector3 Position { get; set; }

        public float Intensity { get; set; }
    }
}
