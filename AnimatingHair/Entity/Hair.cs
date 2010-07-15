using System;
using System.Collections.Generic;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Entity
{
    /// <summary>
    /// Represents hair in the simulation.
    /// </summary>
    class Hair : Fluid<HairParticle>
    {
        /// <summary>
        /// A collection for direct access of the pairs established in the beginning. 
        /// </summary>
        public readonly ParticlePair[ , ] ParticlePairs;

        /// <summary>
        /// A collection for iteration of the pairs established in the beginning. 
        /// </summary>
        public readonly List<ParticlePair> ParticlePairsIteration;

        /// <summary>
        /// Color of the hair.
        /// </summary>
        public float[] Clr;

        // auxiliary arrays used for calculation of forces defined by the method
        private readonly float[] alphaR;
        private readonly float[] alphaT;

        public Hair()
        {
            //Clr = new float[] { 0.55f, 0.26f, 0.13f };
            Clr = new float[] { 1f, 0.47f, 0.24f };
            alphaT = new float[ Const.Instance.HairParticleCount ];
            alphaR = new float[ Const.Instance.HairParticleCount ];
            ParticlePairsIteration = new List<ParticlePair>();
            ParticlePairs = new ParticlePair[ Const.Instance.HairParticleCount, Const.Instance.HairParticleCount ];
            Particles = new HairParticle[ Const.Instance.HairParticleCount ];
        }

        /// <summary>
        /// Applies forces on hair particles caused by the hair particles themselves (and by gravity).
        /// </summary>
        public void CalculateForcesOnSelf()
        {
            prepareParticles();

            prepareParticlePairs();

            applyGravitationalForce();

            applySpringForces();

            applyNeighborForces();

            applyAirFrictionForces();

            resetRootParticleForces();
        }

        /// <summary>
        /// Updates particle direction according to the method.
        /// </summary>
        public void UpdateDirections()
        {
            Parallel.For( 0, Particles.Length, i =>
            //for ( int i = 0; i < Particles.Length; i++ )
            {
                updateDirection( Particles[ i ] );
            } );
        }

        /// <summary>
        /// Exerts on each particle a "fictitious" inertial force,
        /// in order to react to the head's movement.
        /// </summary>
        /// <remarks>
        /// This is done because we desribe the particles with respect
        /// to the local coordinate system attached to the head.
        /// </remarks>
        /// <param name="acceleration">The acceleration of the system (the head).</param>
        public void ApplyInertialAcceleration( Vector3 acceleration )
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                HairParticle particle = Particles[ i ];
                if ( !particle.IsRoot )
                    particle.Force += particle.Mass * acceleration;
            }
        }

        /// <summary>
        /// Exerts on each particle a "fictitious" centrifugal and inertial force,
        /// in order to react to the head's rotation.
        /// </summary>
        /// <remarks>
        /// This is done because we desribe the particles with respect
        /// to the local coordinate system attached to the head.
        /// </remarks>
        /// <param name="angularAcceleration">The angular acceleration in radians per second squared.</param>
        public void ApplyInertialAngularAcceleration( float angularAcceleration, float angularVelocity )
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                HairParticle particle = Particles[ i ];
                if ( !particle.IsRoot )
                    particle.ApplyAngularAcceleration( angularAcceleration, angularVelocity );
            }
        }

        #region Private methods

        private void resetRootParticleForces()
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                HairParticle hp = Particles[ i ];

                if ( hp.IsRoot )
                {
                    hp.Force = Vector3.Zero;
                }
            }
        }

        private void prepareParticlePairs()
        {
            Parallel.For( 0, ParticlePairsIteration.Count, i =>
            //for ( int i = 0; i < ParticlePairsIteration.Count; i++ )
            {
                ParticlePair pair = ParticlePairsIteration[ i ];
                ParticlePair oppositePair = pair.OppositePair;

                pair.CurrentPositionDifference = pair.ParticleJ.Position - pair.ParticleI.Position;
                oppositePair.CurrentPositionDifference = -pair.CurrentPositionDifference;

                pair.CurrentDistance = pair.CurrentPositionDifference.Length;
                oppositePair.CurrentDistance = pair.CurrentDistance;

                pair.Alpha = findAlphaIJ( pair );
                oppositePair.Alpha = pair.Alpha;

                pair.T = findTIJ( pair );
                oppositePair.T = findTIJ( oppositePair );
            } );
        }

        private void prepareParticles()
        {
            Parallel.For( 0, Particles.Length, i =>
            //for ( int i = 0; i < Particles.Length; i++ )
            {
                HairParticle hp = Particles[ i ];

                hp.Force = Vector3.Zero;

                calculateNewKX( hp );

                calculateAlphas( hp );
            } );
        }

        private void applyGravitationalForce()
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                HairParticle particle = Particles[ i ];
                particle.Force.Y += particle.Mass * ((float)-Const.Instance.Gravity);
            }
        }

        private void applyAirFrictionForces()
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                HairParticle hp = Particles[ i ];

                hp.Force += -hp.Mass * Const.Instance.AirFriction * hp.Velocity;
            }
        }

        private void applyNeighborForces()
        {
            //for ( int i = 0; i < Particles.Length; i++ )
            //{
            //    HairParticle particle = Particles[ i ];

            //    for ( int j = 0; j < particle.NeighborsHair.Count; j++ )
            //    {
            //        Vector3 f = calculateHairHairForces(
            //            particle, particle.NeighborsHair[ j ],
            //            particle.DistancesHair[ j ], particle.KernelH2DistancesHair[ j ] );
            //        particle.Force += f;
            //    }
            //}

            Parallel.For( 0, Particles.Length, i =>
            //for ( int i = 0; i < Particles.Length; i++ )
            {
                HairParticle particle = Particles[ i ];
                for ( int j = 0; j < particle.NeighborsHair.Count; j++ )
                {
                    Vector3 f = calculateHairHairForces(
                        particle, particle.NeighborsHair[ j ],
                        particle.DistancesHair[ j ], particle.KernelH2DistancesHair[ j ] );
                    particle.Force += f;
                }
            } );
        }

        /// <summary>
        /// Calculates the interaction force due to attraction/repulsion,
        /// collision and frictional forces between particles.
        /// </summary>
        /// <remarks>
        /// This is one of the single most time-consuming functions in the simulation.
        /// </remarks>
        /// <returns>The resultant force of these three types of interaction.</returns>
        private static Vector3 calculateHairHairForces( HairParticle hpI, HairParticle hpJ, float distance, float kernelH2 )
        {
            // TODO: try to optimize

            Vector3 xIJ = hpJ.Position - hpI.Position;
            Vector3 vIJ = hpJ.Velocity - hpI.Velocity;

            Vector3 dN = Vector3.Cross( hpI.Direction, hpJ.Direction );
            float dNLength = dN.Length;
            if ( dNLength < 0.01 ) // NOTE: constant, "if ||dN|| << 1"
            {
                dN = xIJ - (Vector3.Dot( xIJ, hpI.Direction ) * hpI.Direction);
                dN.Normalize();
            }
            else
            {
                dN /= dNLength;
            }


            // ATTRACTION/REPULUSION FORCE
            float P_i = Const.Instance.HairDensityForceMagnitude * (hpI.Density - Const.Instance.AverageHairDensity);
            float P_j = Const.Instance.HairDensityForceMagnitude * (hpJ.Density - Const.Instance.AverageHairDensity);

            Vector3 kernelGradientH2 = KernelEvaluator.ComputeKernelGradientH2( -xIJ, distance );
            float d1 = P_i / (hpI.Density * hpI.Density);
            float d2 = P_j / (hpJ.Density * hpJ.Density);
            Vector3 fA = -hpI.Mass * hpJ.Mass * (d1 + d2) * kernelGradientH2;


            // COLLISION FORCE
            Vector3 fC;
            float sign = (Vector3.Dot( xIJ, dN ) * Vector3.Dot( vIJ, dN ));
            if ( sign < 0 )
                fC = Const.Instance.CollisionDamp * kernelH2 * Vector3.Dot( vIJ, dN ) * dN;
            else
                fC = Vector3.Zero;


            // FRICTION FORCE
            Vector3 fF = Vector3.Zero;
            Vector3 dT = vIJ - (Vector3.Dot( vIJ, dN ) * dN);
            if ( !(Math.Abs( dT.X ) < 0.00001 && Math.Abs( dT.Y ) < 0.00001 && Math.Abs( dT.Z ) < 0.00001) )  // NOTE: constant - almost zero
            {
                dT.Normalize();
                fF = Const.Instance.FrictionDamp * kernelH2 * (Vector3.Dot( vIJ, dT ) * dT);
            }


            return fA + fC + fF;
        }

        private static Vector3 findTIJ( ParticlePair pp )
        {
            Vector3 axis = Vector3.Cross( pp.ParticleI.Direction, pp.CurrentPositionDifference );
            return Geometry.RotateVectorAroundAxis( pp.CurrentPositionDifference, axis, pp.Theta );
        }

        private void updateDirection( HairParticle hp )
        {
            Vector3 sum = Vector3.Zero;

            for ( int i = 0; i < hp.NeighborsRoot.Count; i++ )
            {
                ParticlePair pp = ParticlePairs[ hp.ID, hp.NeighborsRoot[ i ].ID ];
                sum += pp.Alpha * pp.K * pp.T;
            }

            for ( int i = 0; i < hp.NeighborsTip.Count; i++ )
            {
                ParticlePair pp = ParticlePairs[ hp.ID, hp.NeighborsTip[ i ].ID ];
                sum += pp.Alpha * pp.K * pp.T;
            }

            sum.Normalize();

            if ( float.IsNaN( sum.X ) )
                hp.Direction = Vector3.Zero;
            else
                hp.Direction = sum;
        }

        private void calculateAlphas( HairParticle hp )
        {
            float numerator = Const.Instance.ElasticModulus * hp.Area;
            float denominator;

            denominator = 0;
            for ( int i = 0; i < hp.NeighborsRoot.Count; i++ )
            {
                ParticlePair pp = ParticlePairs[ hp.ID, hp.NeighborsRoot[ i ].ID ];
                denominator += pp.L * pp.K;
            }
            if ( denominator != 0 )
                alphaR[ hp.ID ] = numerator / denominator;

            denominator = 0;
            for ( int i = 0; i < hp.NeighborsTip.Count; i++ )
            {
                ParticlePair pp = ParticlePairs[ hp.ID, hp.NeighborsTip[ i ].ID ];
                denominator += pp.L * pp.K;
            }
            if ( denominator != 0 )
                alphaT[ hp.ID ] = numerator / denominator;
        }

        private void applySpringForces()
        {
            for ( int i = 0; i < ParticlePairsIteration.Count; i++ )
            {
                ParticlePair pp = ParticlePairsIteration[ i ];

                Vector3 deltaLocation = pp.CurrentPositionDifference;
                float distance = pp.CurrentDistance;
                deltaLocation /= distance;

                float springCoefficient = pp.Alpha * pp.K;
                float springFactor = springCoefficient * (distance - pp.L);
                Vector3 force = springFactor * deltaLocation;

                pp.ParticleI.Force += force;
                pp.ParticleJ.Force += -force;
            }
        }

        private float findAlphaIJ( ParticlePair pair )
        {
            // Checks which of the paired particles is closer to root
            if ( pair.IsRootI )
            {
                return (alphaR[ pair.J ] + alphaT[ pair.I ]) / 2;
            }
            else
            {
                return (alphaR[ pair.I ] + alphaT[ pair.J ]) / 2;
            }
        }

        private void calculateNewKX( HairParticle hp )
        {
            for ( int i = 0; i < hp.NeighborsRoot.Count; i++ )
            {
                HairParticle neighbor = hp.NeighborsRoot[ i ];

                ParticlePair currentPair = ParticlePairs[ hp.ID, neighbor.ID ];

                currentPair.K = currentPair.C * KernelEvaluator.ComputeKernelH1( currentPair.CurrentDistance );
            }

            for ( int i = 0; i < hp.NeighborsTip.Count; i++ )
            {
                HairParticle neighbor = hp.NeighborsTip[ i ];

                ParticlePair currentPair = ParticlePairs[ hp.ID, neighbor.ID ];

                currentPair.K = currentPair.C * KernelEvaluator.ComputeKernelH1( currentPair.CurrentDistance );
            }
        }

        #endregion
    }
}
