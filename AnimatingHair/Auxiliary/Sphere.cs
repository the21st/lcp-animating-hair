using OpenTK;

namespace AnimatingHair.Auxiliary
{
    /// <summary>
    /// A sphere representation by center and radius.
    /// </summary>
    class Sphere
    {
        public Vector3 Center { get; private set; }
        public float Radius { get; private set; }
        public float RadiusSquared { get; private set; }

        public Sphere(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
            RadiusSquared = radius * radius;
        }
    }
}
