using System;
using OpenTK;

namespace AnimatingHair.Auxiliary
{
    /// <summary>
    /// A cylinder representation by two endpoints and a radius.
    /// </summary>
    class Cylinder
    {
        public Vector3 Endpoint1 { get; set; }
        public Vector3 Endpoint2 { get; set; }
        public Vector3 OriginalEndpoint1 { get; set; }
        public Vector3 OriginalEndpoint2 { get; set; }
        public float Radius { get; set; }

        public readonly float Length;
        public readonly float LengthSquared;
        public readonly float RadiusSquared;

        public Cylinder( Vector3 endpoint1, Vector3 endpoint2, float radius )
        {
            Endpoint1 = endpoint1;
            Endpoint2 = endpoint2;
            OriginalEndpoint1 = endpoint1;
            OriginalEndpoint2 = endpoint2;
            Radius = radius;
            RadiusSquared = radius * radius;
            LengthSquared = (endpoint2 - endpoint1).LengthSquared;
            Length = (float)Math.Sqrt( LengthSquared );
        }
    }
}
