using System.Collections.Generic;
using OpenTK;
using System;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Initialization
{
    /// <summary>
    /// Distributes particles evenly inside the specified cylinder.
    /// </summary>
    class CylinderDistributor : IParticleDistributor
    {
        private readonly CylinderCoordinateGenerator ccg;

        public CylinderDistributor( int seed, Cylinder cylinder )
        {
            ccg = new CylinderCoordinateGenerator( seed, cylinder );
        }

        /// <summary>
        /// Distributes particles in the cylinder.
        /// </summary>
        /// <remarks>
        /// Does it in a very primitive and straightforward way.
        /// The evenness of the distribution is done in the same way as
        /// described in the CantileverBeamDistributor
        /// </remarks>
        public IEnumerable<ParticleCoordinate> DistributeParticles( int particleCount )
        {
            ParticleCoordinate[] result = new ParticleCoordinate[ particleCount ];

            int k = 0;
            float minDistance = 10; // NOTE: constant

            while ( k < particleCount )
            {
                bool badPosition = false;

                Vector3 newPosition = ccg.Generate();

                for ( int i = 0; i < k; i++ )
                {
                    if ( (newPosition - result[ i ].Position).Length < minDistance )
                    {
                        badPosition = true;
                        minDistance *= 0.99f; // NOTE: constant
                        break;
                    }
                }

                if ( !badPosition )
                {
                    result[ k ] = new ParticleCoordinate
                                  {
                                      Position = newPosition
                                  };
                    k++;
                    minDistance = 10; // NOTE: constant
                }
            }

            return result;
        }

        public class CylinderCoordinateGenerator
        {
            private readonly Random r;
            private readonly Cylinder cylinder;

            public CylinderCoordinateGenerator( int seed, Cylinder cylinder )
            {
                r = new Random( seed );
                this.cylinder = cylinder;
            }

            public Vector3 Generate()
            {
                Vector3 result = Vector3.Zero;

                float radius = cylinder.Radius * (float)r.NextDouble();
                float angle = 2 * MathHelper.Pi * (float)r.NextDouble();
                result.X = radius * (float)Math.Cos( angle );
                result.Y = radius * (float)Math.Sin( angle );

                Vector3 direction = cylinder.Endpoint2 - cylinder.Endpoint1;
                result += direction * (float)r.NextDouble();

                result += cylinder.Endpoint1;

                return result;
            }
        }
    }
}