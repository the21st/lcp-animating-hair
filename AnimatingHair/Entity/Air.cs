using System;
using AnimatingHair.Auxiliary;
using AnimatingHair.Entity.PhysicalEntity;
using AnimatingHair.Initialization;
using OpenTK;

namespace AnimatingHair.Entity
{
    /// <summary>
    /// Represents air in the simulation. Isn't meant to be omnipresent (like real air), just to simulate wind.
    /// </summary>
    class Air : Fluid<AirParticle>
    {
        /// <summary>
        /// The boundary sphere. If a particle ventures outside this sphere, its position is reset (behind the fan)
        /// </summary>
        private readonly Sphere boundarySphere;

        #region Fan

        private const float fanPosition = 3; // Z-coordinate of an imaginary "fan"

        /// <summary>
        /// Gets or sets whether the imaginary "fan" is turned on
        /// </summary>
        public bool Fan { get; set; }

        /// <summary>
        /// The strength of the fan blowing the particles towards the head.
        /// </summary>
        public float FanStrength { get; set; }

        #endregion

        private readonly CylinderDistributor.CylinderCoordinateGenerator cylinderCoordinateGenerator;

        public Air()
        {
            boundarySphere = new Sphere( Vector3.UnitZ * 3, 6 );
            Particles = new AirParticle[ Const.AirParticleCount ];
            cylinderCoordinateGenerator = new CylinderDistributor.CylinderCoordinateGenerator( Const.Seed, new Cylinder( new Vector3( 0, -1, 4 ), new Vector3( 0, -1, 7 ), 2 ) );
        }

        /// <summary>
        /// Applies forces on air particles caused by the air particles themselves (and by the "fan").
        /// </summary>
        public void CalculateForcesOnSelf()
        {
            // set the forces to zero
            prepareParticles();

            // apply SPH momentum equation
            applyPressureForces();

            // apply fan force, if it is turned on
            if (Fan)
                applyFanForce();
        }

        /// <summary>
        /// Checks whether there are any particles outside the boundary sphere.
        /// If so, restarts their position and sets their velocities to zero.
        /// </summary>
        public void CorrectBoundaries()
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                AirParticle particle = Particles[ i ];
                if ( (particle.Position - boundarySphere.Center).LengthSquared > boundarySphere.RadiusSquared )
                {
                    resetParticle( particle );
                }
            }
        }

        private void resetParticle( AirParticle particle )
        {
            particle.Force = Vector3.Zero;
            particle.Acceleration = Vector3.Zero;
            particle.Velocity = Vector3.Zero;

            particle.Position = cylinderCoordinateGenerator.Generate();
        }

        private void applyFanForce()
        {
            Vector3 forceDirection = -Vector3.UnitZ; // direction in which the fan is blowing
            for ( int i = 0; i < Particles.Length; i++ )
            {
                AirParticle particle = Particles[ i ];

                // apply force directly proportional to the distance of the particle from the fan
                float distance = Math.Abs( particle.Position.Z - fanPosition );

                float factor = FanStrength / (distance + 1);

                particle.Force += particle.Mass * factor * forceDirection;
            }
        }

        private void applyPressureForces()
        {
            for ( int index = 0; index < Particles.Length; index++ )
            {
                AirParticle particle = Particles[ index ];

                for ( int i = 0; i < particle.NeighborsAir.Count; i++ )
                {
                    AirParticle neighborParticle = particle.NeighborsAir[ i ];
                    Vector3 acceleration = calculatePressureAcceleration( particle, neighborParticle, particle.DistancesAir[ i ] );
                    particle.Force += particle.Mass * acceleration;
                }
            }
        }

        private static Vector3 calculatePressureAcceleration( AirParticle particle1, AirParticle particle2, float distance )
        {
            // apply equation of motion from SPH:

            float P_i = Const.k_a_air * (particle1.Density - Const.rho_0_air);
            float P_j = Const.k_a_air * (particle2.Density - Const.rho_0_air);

            Vector3 wgrad = KernelEvaluator.ComputeKernelGradientH2( particle1.Position - particle2.Position, distance );
            float d1 = P_i / (particle1.Density * particle1.Density);
            float d2 = P_j / (particle2.Density * particle2.Density);
            Vector3 a = -particle2.Mass * (d1 + d2) * wgrad;

            return a;
        }

        private void prepareParticles()
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                Particles[ i ].Force = Vector3.Zero;
            }
        }
    }
}
