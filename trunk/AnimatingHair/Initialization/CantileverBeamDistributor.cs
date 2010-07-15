using System;
using System.Collections.Generic;
using AnimatingHair.Auxiliary;
using OpenTK;
using AnimatingHair.Entity.PhysicalEntity;

namespace AnimatingHair.Initialization
{
    /// <summary>
    /// Implements the IParticleDistributor interface and
    /// thus provides a method for distributing the particles as descibed
    /// by the method (treats hair strands as cantilever beams).
    /// </summary>
    class CantileverBeamDistributor : IParticleDistributor
    {
        // distribution constants
        private const float normalComponent = 1.3f;
        private const float tangentComponent = 17.33f;
        private const float whorlZ = 0.578f;
        private const float gravityFactor = 0.159f;

        // auxiliary classes used for various stages of generation of the particle coordinate
        private readonly SphereCoordinateGenerator scg;
        private readonly CoordinateTransformator ct;
        private readonly CantileverBeamSimulator cbs;
        private readonly Random r;

        public CantileverBeamDistributor( int seed, Bust bust )
        {
            r = new Random( seed );
            scg = new SphereCoordinateGenerator( seed, bust.Head );
            ct = new CoordinateTransformator( bust.Head );

            Sphere[] spheres = new Sphere[ 1 ];
            spheres[ 0 ] = bust.Head;
            Cylinder[] cylinders = new Cylinder[ 0 ]; // TODO: cantilever collisions
            cbs = new CantileverBeamSimulator( spheres, cylinders, Const.Instance.ElasticModulus, Const.Instance.SecondMomentOfArea );
        }

        /// <summary>
        /// Disributes the particles according to the cantilever-beam distribution
        /// method proposed by Anjyo et al.
        /// </summary>
        /// <param name="particleCount">The number of particles to be distributed.</param>
        /// <returns>An IEnumberable of the particles.</returns>
        /// <remarks>
        /// This is a primitive ad-hoc method for random uniform (Poisson-disk like) distribution.
        /// For each new particle, I set minDistance to a constant (large enough) value.
        /// If the new particle is not at least minDistance away from ALL other particles (that have
        /// been distributed so far), I decrease the minDistance by a constant (small enough) until
        /// the new particle is far away from all other particles.
        /// </remarks>
        public IEnumerable<ParticleCoordinate> DistributeParticles( int particleCount )
        {
            ParticleCoordinate[] result = new ParticleCoordinate[ particleCount ];

            int k = 0;
            float minDistance = 10; // NOTE: constant

            while ( k < particleCount )
            {
                bool badPosition = false;
                SphericalCoordinate sc;

                // first, generate a spherical coordinate
                sc = scg.GenerateRandomSphericalCoordinate();

                // then, generate the distance of the particle from the root of the hair (can be negative for root particles)
                float length = (Const.Instance.HairLength + Const.Instance.MaxRootDepth) * (float)r.NextDouble() - Const.Instance.MaxRootDepth;

                // transform the spherical coordinate into euclidean coordinate
                ParticleCoordinate newCoordinate = ct.TransformCoordinate( sc.Azimuth, sc.Elevation, length );

                // if it is not rooted inside the head, apply cantilever transformation
                if ( newCoordinate.S > 0 )
                {
                    Vector3 force = -gravityFactor * Vector3.UnitY;
                    newCoordinate = cbs.SimulateCantileverBeam( newCoordinate, force );
                }

                // check if the particle is far away enough from all the particles distributed so far
                for ( int i = 0; i < k; i++ )
                {
                    if ( (newCoordinate.Position - result[ i ].Position).Length < minDistance )
                    {
                        badPosition = true;
                        minDistance *= 0.99f; // NOTE: constant
                        break;
                    }
                }

                // if not, decrease minDistance and try again
                if ( !badPosition )
                {
                    result[ k ] = newCoordinate;
                    k++;
                    minDistance = 10; // NOTE: constant
                }
            }

            return result;
        }


        class SphereCoordinateGenerator
        {
            private readonly Sphere sphere;
            private readonly Random r;

            public SphereCoordinateGenerator( int seed, Sphere sphere )
            {
                this.sphere = sphere;
                r = new Random( seed );
            }

            public SphericalCoordinate GenerateRandomSphericalCoordinate()
            {
                float azimuth, elevationSentinel, elevation;

                const float minDist = 0.4f;
                Vector3 ear1 = sphere.Radius * Vector3.UnitX;
                Vector3 ear2 = -ear1;
                Vector3 result;

                do
                {
                    elevationSentinel = 0.5f * MathHelper.Pi * (float)r.NextDouble();
                    elevation = (float)Math.Sin( elevationSentinel ) * MathHelper.Pi * (float)r.NextDouble();
                    //elevation = Const.Instance.PI/2 * (float)r.NextDouble();
                    azimuth = MathHelper.Pi * (float)r.NextDouble();
                    result = sphere.Center + polarToCartesian( azimuth, elevation, sphere.Radius );
                } while ( (result - ear1).Length < minDist || (result - ear2).Length < minDist );

                SphericalCoordinate sc;
                sc.Azimuth = azimuth;
                sc.Elevation = elevation;

                return sc;
            }

            private static Vector3 polarToCartesian( float azimuth, float elevation, float distance )
            {
                float x, y, z, k;

                k = (float)Math.Cos( elevation );
                z = distance * (float)Math.Sin( elevation );
                y = distance * (float)Math.Sin( azimuth ) * k;
                x = distance * (float)Math.Cos( azimuth ) * k;

                return new Vector3( x, z, y );
            }
        }


        class CoordinateTransformator
        {
            private readonly Sphere sphere;
            private Vector3 whorlNormal;

            public CoordinateTransformator( Sphere sphere )
            {
                this.sphere = sphere;
                whorlNormal = new Vector3( 0, 1, whorlZ );
                whorlNormal.Normalize();
            }

            public ParticleCoordinate TransformCoordinate( float azimuth, float elevation, float length )
            {
                whorlNormal = new Vector3( 0, 1, -whorlZ );
                whorlNormal.Normalize();

                ParticleCoordinate result = new ParticleCoordinate
                                            {
                                                S = length
                                            };

                Vector3 startPosition = polarToCartesian( azimuth, elevation, sphere.Radius );

                startPosition = Geometry.RotateVectorAroundAxis( startPosition, Vector3.UnitX, 0.6f ); // NOTE: constant

                Vector3 scalpNormal;

                if ( length < 0 )
                {
                    scalpNormal = startPosition - sphere.Center;
                    scalpNormal.Normalize();
                    result.Position = startPosition + length * scalpNormal;
                    result.Direction = scalpNormal;
                    result.RootDirection = scalpNormal;
                    return result;
                }

                // TODO: make better
                {
                    float distanceFromWhorl = (startPosition - whorlNormal).Length;

                    if ( distanceFromWhorl < 0.3 )
                    {
                        length = (distanceFromWhorl / 0.6f) * length;
                    }
                    else
                    {
                        if ( startPosition.Z > -whorlZ )
                        {
                            float factor = Math.Abs( startPosition.X );
                            factor += 1;
                            factor /= 2;
                            length = (1.4f - distanceFromWhorl) / 1.4f * length;
                            length *= (2 * factor);
                        }
                    }
                }

                if ( length < 0 )
                    length = 0;

                result.S = length;

                scalpNormal = startPosition - sphere.Center;

                Vector3 rootDirection =
                    -tangentComponent * (whorlNormal - (Vector3.Dot( whorlNormal, scalpNormal ) * scalpNormal)) +
                    normalComponent * scalpNormal;

                rootDirection.Normalize();

                result.RootDirection = rootDirection;
                result.Position = startPosition;
                result.Direction = rootDirection;

                return result;
            }

            private static Vector3 polarToCartesian( float azimuth, float elevation, float distance )
            {
                float x, y, z, k;

                k = (float)Math.Cos( elevation );
                z = distance * (float)Math.Sin( elevation );
                y = distance * (float)Math.Sin( azimuth ) * k;
                x = distance * (float)Math.Cos( azimuth ) * k;

                return new Vector3( x, z, y );
            }
        }


        class CantileverBeamSimulator
        {
            private const float segmentLength = 0.04693f;

            private readonly Sphere[] spheres;
            private readonly Cylinder[] cylinders;
            private readonly float elasticModulus;
            private readonly float secondMomentumOfArea;

            public CantileverBeamSimulator( Sphere[] spheres, Cylinder[] cylinders, float elasticModulus, float momentum )
            {
                this.spheres = spheres;
                this.cylinders = cylinders;
                this.elasticModulus = elasticModulus;
                secondMomentumOfArea = momentum;
            }

            // TODO: add collision detection with spheres and cylinders
            public ParticleCoordinate SimulateCantileverBeam( ParticleCoordinate particleCoordinate, Vector3 externalForce )
            {
                int k = Convert.ToInt32( particleCoordinate.S / segmentLength );
                ParticleCoordinate result = new ParticleCoordinate();

                Vector3 a0, a1, a2, pPrevious, pPreviousStar, pCurrent, pCurrentStar, d, yi, ei;
                float m1, m2, g1, g2, y1, y2;
                float eTimesI = elasticModulus * secondMomentumOfArea;

                if ( particleCoordinate.S <= 0 )
                    return particleCoordinate;

                particleCoordinate.RootDirection.Normalize();
                pCurrent = particleCoordinate.Position;
                d = segmentLength * particleCoordinate.RootDirection;

                for ( int i = 1; i <= k; i++ )
                {
                    a0 = d;
                    a0.Normalize();
                    d = a0 * segmentLength;
                    pPrevious = pCurrent;
                    pPreviousStar = pPrevious + externalForce;
                    pCurrentStar = pPrevious + d;
                    a2 = Vector3.Cross( a0, pPreviousStar - pPrevious );
                    a2.Normalize();
                    a1 = Vector3.Cross( a2, a0 );

                    g1 = component( externalForce, a1 );
                    m1 = -g1 * segmentLength * (k - i + 1) * (k - i + 1) / 2;
                    y1 = -0.5f * (m1 / eTimesI) * segmentLength * segmentLength;

                    g2 = component( externalForce, a2 );
                    m2 = -g2 * segmentLength * (k - i + 1) * (k - i + 1) / 2;
                    y2 = -0.5f * (m2 / eTimesI) * segmentLength * segmentLength;

                    yi = y1 * a1 + y2 * a2;

                    Vector3 tmp = pCurrentStar + yi;

                    ei = tmp - pPrevious;
                    ei.Normalize();
                    ei *= segmentLength;

                    pCurrent = pPrevious + ei;

                    // TODO: colission detection with spheres and cylinders
                    foreach ( Cylinder cylinder in cylinders )
                    {
                    }

                    foreach ( Sphere sphere in spheres )
                    {
                        if ( (pCurrent - sphere.Center).Length < sphere.Radius )
                        {

                        }
                    }

                    d = ei;
                }

                result.Position = pCurrent;
                result.Direction = Vector3.Normalize( d );
                result.RootDirection = particleCoordinate.RootDirection;
                result.S = particleCoordinate.S;
                return result;
            }

            private static float component( Vector3 v, Vector3 target )
            {
                float targetLength = target.Length;
                return Vector3.Dot( v, target ) / targetLength;
            }
        }
    }
}