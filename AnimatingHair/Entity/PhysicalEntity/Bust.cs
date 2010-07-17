using OpenTK;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Entity.PhysicalEntity
{
    /// <summary>
    /// The physical-interaction Bust representation - it is simplified into a sphere (head), a cylinders (neck) and a capsule (shoulders
    /// </summary>
    class Bust : RigidBody
    {
        // TODO: friction force on particles - Coulomb's model

        private const float interactionDistance = 0.3f;

        public Bust( float mass ) : base( mass ) { }

        public Sphere Head { get; set; }
        public Cylinder Neck { get; set; }
        public Cylinder Shoulders { get; set; }
        public Sphere ShoulderTipLeft { get; set; }
        public Sphere ShoulderTipRight { get; set; }

        public void ApplyForcesOnParticles( SPHParticle[] particles )
        {
            Parallel.For( 0, particles.Length, i =>
            //for ( int i = 0; i < particles.Length; i++ )
            {
                SPHParticle particle = particles[ i ];

                applySphereCollisionForce( Head, particle );

                applyCylinderCollisionForce( Neck, particle );

                if ( !applyCylinderCollisionForce( Shoulders, particle ) )
                {
                    applySphereCollisionForce( ShoulderTipLeft, particle );
                    applySphereCollisionForce( ShoulderTipRight, particle );
                }
            } );
        }

        private static void applySphereCollisionForce( Sphere sphere, SPHParticle particle )
        {
            Vector3 normal = particle.Position - sphere.Center;
            float distance = normal.Length;
            normal = normal / distance;

            if ( distance < sphere.Radius + interactionDistance && distance > sphere.Radius )
            {
                float magnitude = 1;
                float dot = Vector3.Dot( particle.Velocity, -normal );
                if ( dot > 0 )
                {
                    magnitude *= dot;
                    particle.Force += particle.Mass * magnitude * normal;
                }
            }
            else if ( distance < sphere.Radius )
            {
                particle.Force += 4 * particle.Mass * normal;
            }
        }

        private static bool applyCylinderCollisionForce( Cylinder cylinder, SPHParticle particle )
        {
            if ( !Geometry.PointInInfiniteRadiusCylinder( cylinder, particle.Position ) )
                return false;

            Vector3 normal = Geometry.LineToPointNormal( cylinder.Endpoint1, cylinder.Endpoint2, particle.Position );
            float distance = normal.Length;
            normal = normal / distance;

            if ( distance < cylinder.Radius )
            {
                particle.Force += 4 * particle.Mass * normal;
                return true;
            }
            else if ( (distance < cylinder.Radius + interactionDistance) && (distance > cylinder.Radius) )
            {
                float magnitude = 1;
                float dot = Vector3.Dot( particle.Velocity, -normal );
                if ( dot > 0 )
                {
                    magnitude *= dot;
                    particle.Force += particle.Mass * magnitude * normal;
                    return true;
                }
            }

            return false;
        }
    }
}