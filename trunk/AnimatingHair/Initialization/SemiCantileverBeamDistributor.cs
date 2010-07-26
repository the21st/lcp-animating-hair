using System;
using System.Collections.Generic;
using AnimatingHair.Auxiliary;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;

namespace AnimatingHair.Initialization
{
    /// <summary>
    /// Implements the IParticleDistributor interface and
    /// thus provides a method for distributing the particles.
    /// </summary>
    /// <remarks>
    /// This class is almost the same as CantileverBeamDistributor, 
    /// but by an error, it distributes particles in a slightly different way.
    /// However, this error caused that the distribution is somewhat visually
    /// better than the actual Cantilever method.
    /// </remarks>
    class SemiCantileverBeamDistributor : IParticleDistributor
    {
        private const float normalComponent = 0.2f;
        private const float tangentComponent = 2.0f;
        private const float gravityFactor = 3;
        private const float whorlZ = -0.1f;
        private const float elasticModulus = 35000;

        private readonly SphereCoordinateGenerator scg;
        private readonly CoordinateTransformator ct;
        private readonly CantileverBeamSimulator cbs;
        private readonly Random r;

        public SemiCantileverBeamDistributor( int seed, Bust bust )
        {
            r = new Random( seed );
            scg = new SphereCoordinateGenerator( bust.Head.Center, bust.Head.Radius, seed );
            ct = new CoordinateTransformator( bust.Head.Center, bust.Head.Radius );

            Sphere[] spheres = new Sphere[ 1 ];
            spheres[ 0 ] = bust.Head;
            Cylinder[] cylinders = new Cylinder[ 0 ]; // TODO: cantilever collisions
            cbs = new CantileverBeamSimulator( spheres, cylinders, elasticModulus, Const.Instance.SecondMomentOfArea );
        }

        public IEnumerable<ParticleCoordinate> DistributeParticles( int particleCount )
        {
            ParticleCoordinate[] result = new ParticleCoordinate[ particleCount ];

            // tu prebieha nejake moje ad-hoc rovnomerne nahodne rozmiestnovanie (vysledkom sa to asi podoba poissonovi)
            int k = 0;
            float minDistance = 10; // NOTE: constant

            while ( k < particleCount )
            {
                bool badPosition = false;
                SphericalCoordinate sc;
                sc = scg.GenerateRandomSphericalCoordinate();

                ParticleCoordinate newCoordinate = ct.TransformCoordinate( sc.Azimuth, sc.Elevation, (Const.Instance.HairLength / 2 + Const.Instance.MaxRootDepth) * (float)r.NextDouble() - Const.Instance.MaxRootDepth );
                if ( newCoordinate == null )
                    continue;

                if ( newCoordinate.S > 0 )
                {
                    Vector3 force = -gravityFactor * Vector3.UnitY;
                    newCoordinate = cbs.SimulateCantileverBeam( newCoordinate, force );
                }

                if ( float.IsNaN( newCoordinate.Position.X ) || float.IsNaN( newCoordinate.Position.Y ) || float.IsNaN( newCoordinate.Position.Z ) ||
                    float.IsNaN( newCoordinate.Direction.X ) || float.IsNaN( newCoordinate.Direction.Y ) || float.IsNaN( newCoordinate.Direction.Z ) )
                    continue;

                for ( int i = 0; i < k; i++ )
                {
                    float length = (newCoordinate.Position - result[ i ].Position).Length;
                    if ( newCoordinate.S > 0 )
                    {
                        float add = (1 / (newCoordinate.S + 3));
                        length *= 1 + add;
                    }
                    if ( length < minDistance )
                    {
                        badPosition = true;
                        minDistance *= 0.99f; // NOTE: constant
                        break;
                    }
                }

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
            private readonly Vector3 center;
            private readonly float radius;
            private readonly Random r;

            public SphereCoordinateGenerator( Vector3 center, float radius, int seed )
            {
                r = new Random( seed );
                this.center = center;
                this.radius = radius;
            }

            public SphericalCoordinate GenerateRandomSphericalCoordinate()
            {
                float azimuth, elevationSentinel, elevation;

                const float minDist = 0.4f; // NOTE: constant
                Vector3 ear1 = radius * Vector3.UnitX;
                Vector3 ear2 = -ear1;
                Vector3 result;

                do
                {
                    elevationSentinel = 0.5f * MathHelper.Pi * (float)r.NextDouble();
                    elevation = (float)Math.Sin( elevationSentinel ) * MathHelper.Pi * (float)r.NextDouble();
                    //elevation = Const.Instance.PI/2 * (float)r.NextDouble();
                    azimuth = MathHelper.Pi * (float)r.NextDouble();
                    result = center + polarToCartesian( azimuth, elevation, radius );
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
            private readonly Vector3 center;
            private readonly float radius;
            private readonly Vector3 whorlNormal;

            public CoordinateTransformator( Vector3 center, float radius )
            {
                this.center = center;
                this.radius = radius;
                whorlNormal = new Vector3( 0, 1, whorlZ );
                whorlNormal.Normalize();
            }

            public ParticleCoordinate TransformCoordinate( float azimuth, float elevation, float length )
            {
                ParticleCoordinate result = new ParticleCoordinate { S = length };

                Vector3 startPosition = polarToCartesian( azimuth, elevation, radius );

                startPosition = Geometry.RotateVectorAroundAxis( startPosition, Vector3.UnitX, 0.6f ); // NOTE: constant

                Vector3 scalpNormal;

                if ( length < 0 )
                {
                    scalpNormal = startPosition - center;
                    scalpNormal.Normalize();
                    result.Position = startPosition + length * scalpNormal;
                    result.Direction = scalpNormal;
                    result.RootDirection = scalpNormal;
                    return result;
                }

                // TODO: make better
                if ( startPosition.Z > 0 )
                {
                    float distanceFromWhorl = (startPosition - whorlNormal).Length;
                    length = 0.3f * (1.5f - distanceFromWhorl) * length;
                }

                result.S = length;

                scalpNormal = startPosition - center;

                Vector3 rootDirection =
                    -tangentComponent * (whorlNormal - (Vector3.Dot( whorlNormal, scalpNormal ) * scalpNormal)) +
                    normalComponent * scalpNormal;

                Vector3 gravity = -gravityFactor * Vector3.UnitY;

                result = simulateParticleMotion( startPosition, rootDirection, gravity, length );

                result.S = length;
                result.RootDirection = rootDirection;

                if ( (result.Position - center).Length < radius )
                    return null;

                return result;
            }

            private static ParticleCoordinate simulateParticleMotion( Vector3 startPosition, Vector3 startVelocity, Vector3 gravity, float length )
            {
                ParticleCoordinate result = new ParticleCoordinate();
                const float timeStep = 0.1f;
                Vector3 position = startPosition;
                Vector3 velocity = startVelocity;
                float distanceTraveled = 0;

                while ( distanceTraveled < length )
                {
                    distanceTraveled += (velocity * timeStep).Length;
                    position += velocity * timeStep;
                    velocity += gravity * timeStep;
                }

                result.Position = position;

                velocity.Normalize();
                result.Direction = velocity;

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
            private const float segmentLength = 0.2f;

            private readonly Sphere[] spheres;
            private readonly Cylinder[] cylinders;
            private readonly float eTimesI;

            public CantileverBeamSimulator( Sphere[] spheres, Cylinder[] cylinders, float elasticModulus, float momentum )
            {
                this.spheres = spheres;
                this.cylinders = cylinders;
                eTimesI = elasticModulus * momentum;
            }

            // TODO: collisions with spheres and cylinders
            public ParticleCoordinate SimulateCantileverBeam( ParticleCoordinate particleCoordinate, Vector3 externalForce )
            {
                int k = Convert.ToInt32( particleCoordinate.S / segmentLength );
                ParticleCoordinate result = new ParticleCoordinate();

                Vector3 a0, a1, a2, pPrevious, pPreviousStar, pCurrent, pCurrentStar, d, yi, ei;
                float m1, m2, g1, g2, y1, y2;

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

                    // TODO: collisions
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