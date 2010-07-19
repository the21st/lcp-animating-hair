using System;
using AnimatingHair.Auxiliary;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;

namespace AnimatingHair.Entity
{
    /// <summary>
    /// The main entity object on the top of the (composition) hierarchy.
    /// Its most important function Step() handles one simulation step.
    /// </summary>
    class Scene
    {
        // Properties indicating head movement
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool RotateClockwise { get; set; }
        public bool RotateAntiClockwise { get; set; }

        /// <summary>
        /// Indicates whether the imaginary "fan" is turned on or off.
        /// </summary>
        public bool Fan { get { return Air.Fan; } set { Air.Fan = value; } }

        // the entities
        public Bust Bust { get; set; }
        public Hair Hair { get; set; }
        public Air Air { get; set; }

        /// <summary>
        /// The auxiliary data structure for fast space inquiries.
        /// </summary>
        public VoxelGrid VoxelGrid { get; set; }

        /// <summary>
        /// A collection of all SPH particles in the scene.
        /// These are just references to the same objects that are in the specific fluids.
        /// </summary>
        public SPHParticle[] Particles;

        /// <summary>
        /// Makes the simulation progress by one step (with runge-kutta 4 integration).
        /// The "delta time" of this discrete step is specified by the value of Const.Instance.TimeStep.
        /// </summary>
        internal void Step()
        {
            // based on user input (indirectly - through boolean fields), calculates acceleration acting on the bust
            Vector3 bustMovementAcceleration = getMovementAcceleration();
            float bustAngularAcceleration = getAngularAcceleration();

            // the initialization of the runge-kutta intergration
            rkStep( 0 );

            // the 4 steps of the runge-kutta
            for ( int i = 1; i < 5; i++ )
            {
                // 0. update positions in voxel Grid
                updateVoxelGrid();

                // 1. neighbor search
                neighborSearchVoxelGrid();

                // 2. calculate density
                Hair.CalculateDensity();
                Air.CalculateDensity();

                // 3. apply forces
                applyAllForces( bustMovementAcceleration, bustAngularAcceleration );

                // 4. update positions and velocities
                rkStep( i );

                // 4. update particle directions
                Hair.UpdateDirections();
            }

            // the finalization of the runge-kutta method
            rkStep( 5 );

            Hair.UpdateDirections();

            Air.CorrectBoundaries();
        }

        /// <summary>
        /// Applies forces on entities.
        /// </summary>
        private void applyAllForces( Vector3 bustMovementAcceleration, float bustAngularAcceleration )
        {
            Hair.CalculateForcesOnSelf();
            Air.CalculateForcesOnSelf();

            Bust.ApplyForcesOnParticles( Particles );

            applyInteractionForces( Hair.Particles, Air.Particles );

            Hair.ApplyInertialAcceleration( -bustMovementAcceleration ); // TODO: take into account bust rotation
            Hair.ApplyInertialAngularAcceleration( -bustAngularAcceleration, Bust.AngularVelocity );
            Bust.Acceleration = bustMovementAcceleration;
            Bust.AngularAcceleration = bustAngularAcceleration;
        }

        private float getAngularAcceleration()
        {
            float result = 0;

            if ( RotateClockwise )
                result += 0.5f;
            else
                if ( RotateAntiClockwise )
                    result -= 0.5f;
                else
                    result -= Bust.AngularVelocity;

            if ( Bust.AngularVelocity > 1.0 && result > 0 )
            {
                result = 0;
            }

            if ( Bust.AngularVelocity < -1.0 && result < 0 )
            {
                result = 0;
            }

            return result;
        }

        private Vector3 getMovementAcceleration()
        {
            Vector3 result = new Vector3();
            if ( Up )
                result.Z += 0.4f;
            if ( Down )
                result.Z -= 0.4f;
            if ( Left )
                result.X += 0.4f;
            if ( Right )
                result.X -= 0.4f;

            if ( (result.X == 0 && result.Z == 0) || Bust.Velocity.Length > 0.6 )
            {
                result -= Bust.Velocity;
            }

            return result;
        }

        private void neighborSearchVoxelGrid()
        {
            // clear old values
            Parallel.For( 0, Particles.Length, i =>
            //for ( int i = 0; i < Particles.Length; i++ )
            {
                SPHParticle particle = Particles[ i ];

                particle.NeighborsHair.Clear();
                particle.DistancesHair.Clear();
                particle.KernelH2DistancesHair.Clear();

                particle.NeighborsAir.Clear();
                particle.DistancesAir.Clear();
                particle.KernelH2DistancesAir.Clear();
            } );

            // find current neighbors
            Parallel.For( 0, Particles.Length, i =>
            //for ( int i = 0; i < Particles.Length; i++ )
            {
                SPHParticle particle = Particles[ i ];
                VoxelGrid.FindNeighbors( particle );
            } );
        }

        private void updateVoxelGrid()
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                VoxelGrid.UpdateParticle( Particles[ i ] );
            }
        }

        private void rkStep( int stepNumber )
        {
            Hair.RKStep( stepNumber );
            Air.RKStep( stepNumber );
            Bust.RKStep( stepNumber );
        }

        /// <summary>
        /// Applies drag forces between hair and air particles.
        /// </summary>
        /// <remarks>
        /// The implementation is probably not correct (in a physical manner),
        /// but the method does not specify how to calculate the "velocity estimate"
        /// at a given point. However, visually, the air reaction is believable.
        /// </remarks>
        private static void applyInteractionForces( HairParticle[] hairParticles, AirParticle[] airParticles )
        {
            for ( int index = 0; index < hairParticles.Length; index++ )
            {
                HairParticle hairParticle = hairParticles[ index ];
                Vector3 velocityEstimate = Vector3.Zero;

                // this is my ad-hoc method for calculating the "velocity estimate of air" at the position of the hair particle
                for ( int i = 0; i < hairParticle.NeighborsAir.Count; i++ )
                {
                    velocityEstimate += hairParticle.NeighborsAir[ i ].Velocity * hairParticle.KernelH2DistancesAir[ i ];
                }

                Vector3 force;
                force = hairParticle.Mass * Const.Instance.DragCoefficient * (velocityEstimate - hairParticle.Velocity);
                hairParticle.Force += force;
            }

            for ( int index = 0; index < airParticles.Length; index++ )
            {
                AirParticle airParticle = airParticles[ index ];
                Vector3 velocityEstimate = Vector3.Zero;

                // vice versa
                for ( int i = 0; i < airParticle.NeighborsHair.Count; i++ )
                {
                    velocityEstimate += airParticle.NeighborsHair[ i ].Velocity * airParticle.KernelH2DistancesHair[ i ];
                }

                Vector3 force;
                force = airParticle.Mass * Const.Instance.DragCoefficient * (velocityEstimate - airParticle.Velocity);
                airParticle.Force += force;
            }
        }

        /// <summary>
        /// This is an "alternative" step method, using Euler integration.
        /// </summary>
        public void StepEuler()
        {
            Vector3 bustMovementAcceleration = getMovementAcceleration();
            float bustAngularAcceleration = getAngularAcceleration();

            // 0. update positions in voxel Grid
            updateVoxelGrid();

            // 1. neighbor search
            neighborSearchVoxelGrid();

            // 2. calculate density
            Hair.CalculateDensity();
            Air.CalculateDensity();

            // 3. apply forces
            applyAllForces( bustMovementAcceleration, bustAngularAcceleration );

            // 4. update positions and velocities
            eulerIntegrate();

            // 4. update particle directions
            Hair.UpdateDirections();
        }

        private void eulerIntegrate()
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                Particles[ i ].IntegrateForce( Const.Instance.TimeStep );
            }
        }
    }
}
