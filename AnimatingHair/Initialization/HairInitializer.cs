using System;
using System.Collections.Generic;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Initialization
{
    /// <summary>
    /// Used for initializing the Hair entity.
    /// </summary>
    class HairInitializer
    {
        private Hair hair;
        private IParticleDistributor distributor;

        /// <summary>
        /// Creates and initializes the hair entity by distributing
        /// the particles and establishing lengthwise connections.
        /// Also sets the smoothing length values for the KernelEvaluator.
        /// </summary>
        /// <param name="bust">The bust on which to distribute the hair</param>
        /// <returns>The newly created Hair object</returns>
        public Hair InitializeHair( Bust bust )
        {
            // TODO: which distributor?
            distributor = new SemiCantileverBeamDistributor( Const.Instance.Seed, bust );
            //IParticleDistributor distributor = new CantileverBeamDistributor( Const.Instance.Seed, scene.Bust );

            hair = new Hair();

            // first, distribute the particles in space
            distributeParticles();

            // calculates values required by the method
            calculateArea();
            calculateH1H2();

            // set the smoothing lengths used throughout the whole simulation, so that 
            // the evaluator can precalculate values for efficiency
            KernelEvaluator.SetH1( Const.Instance.H1 );
            KernelEvaluator.SetH2( Const.Instance.H2 );

            // establish lengthwise neighbor connections that stay the same throughout the simulation
            establishConnections();
            trimNeighbors();
            makePairsSymmetrical();

            return hair;
        }

        #region Private initialize methods

        private void makePairsSymmetrical()
        {
            for ( int i = 0; i < Const.Instance.HairParticleCount; i++ )
            {
                for ( int j = 0; j < Const.Instance.HairParticleCount; j++ )
                {
                    if ( hair.ParticlePairs[ i, j ] == null && hair.ParticlePairs[ j, i ] != null )
                    {
                        hair.ParticlePairsIteration.Remove( hair.ParticlePairs[ i, j ] );
                        hair.ParticlePairsIteration.Remove( hair.ParticlePairs[ j, i ] );

                        hair.ParticlePairs[ j, i ] = null;

                        hair.Particles[ i ].NeighborsTip.Remove( hair.Particles[ j ] );
                        hair.Particles[ i ].NeighborsRoot.Remove( hair.Particles[ j ] );
                        hair.Particles[ j ].NeighborsTip.Remove( hair.Particles[ i ] );
                        hair.Particles[ j ].NeighborsRoot.Remove( hair.Particles[ i ] );
                    }
                }
            }
        }

        private void distributeParticles()
        {
            IEnumerable<ParticleCoordinate> coordinates = distributor.DistributeParticles( Const.Instance.HairParticleCount );

            int i = 0;
            foreach ( ParticleCoordinate coordinate in coordinates )
            {
                HairParticle hp = new HairParticle( i, Const.Instance.HairParticleMass() )
                                  {
                                      Position = coordinate.Position,
                                      Direction = coordinate.Direction,
                                      RootDirection = coordinate.RootDirection,
                                      U = coordinate.U,
                                      V = coordinate.V,
                                      S = coordinate.S
                                  };


                hair.Particles[ i ] = hp;

                i++;
            }
        }

        private void calculateArea()
        {
            foreach ( HairParticle hp in hair.Particles )
            {
                hp.Area = (float)Math.Pow( hp.Mass / Const.Instance.DensityOfHairMaterial, 2.0 / 3.0 ); // TODO: precalculate inverse
            }
        }

        private void calculateH1H2()
        {
            float min;
            float sum = 0;

            for ( int i = 0; i < hair.Particles.Length; i++ )
            {
                min = float.MaxValue;

                for ( int j = i + 1; j < hair.Particles.Length; j++ )
                {
                    float currDist = (hair.Particles[ i ].Position - hair.Particles[ j ].Position).Length;
                    if ( currDist < min )
                        min = currDist;
                }

                if ( min != float.MaxValue )
                    sum += min;
            }

            float avgMinDist = sum / hair.Particles.Length;

            Const.Instance.H1 = 1.1f * avgMinDist; // NOTE: constant, "little larger"
            Const.Instance.H2 = avgMinDist / 1.1f; // NOTE: constant, "little smaller"
        }

        private void establishConnections()
        {
            for ( int i = 0; i < hair.Particles.Length; i++ )
                for ( int j = i + 1; j < hair.Particles.Length; j++ )
                {
                    Vector3 xIJ = hair.Particles[ j ].Position - hair.Particles[ i ].Position;
                    float l = xIJ.Length;

                    if ( (l < 2 * Const.Instance.H1) && !(hair.Particles[ i ].IsRoot && hair.Particles[ j ].IsRoot) )
                    {
                        ParticlePair pairIJ = new ParticlePair
                                              {
                                                  ParticleI = hair.Particles[ i ],
                                                  ParticleJ = hair.Particles[ j ],
                                                  I = i,
                                                  J = j,
                                                  L = l,
                                                  CurrentPositionDifference = xIJ,
                                                  Theta = Vector3.CalculateAngle( hair.Particles[ i ].Direction, xIJ )
                                              };

                        ParticlePair pairJI = new ParticlePair
                                              {
                                                  ParticleI = hair.Particles[ j ],
                                                  ParticleJ = hair.Particles[ i ],
                                                  I = j,
                                                  J = i,
                                                  L = l,
                                                  CurrentPositionDifference = -xIJ,
                                                  Theta = Vector3.CalculateAngle( hair.Particles[ j ].Direction, -xIJ )
                                              };

                        pairIJ.SinTheta = (float)Math.Sin( pairIJ.Theta );
                        pairIJ.CosTheta = (float)Math.Cos( pairIJ.Theta );
                        pairJI.SinTheta = (float)Math.Sin( pairJI.Theta );
                        pairJI.CosTheta = (float)Math.Cos( pairJI.Theta );

                        pairIJ.A = pairJI.A = calculateA( pairIJ.Theta, pairJI.Theta );

                        pairIJ.C = pairJI.C = pairIJ.A * l;

                        if ( pairIJ.A < Const.Instance.NeighborAlignmentTreshold )
                            continue;

                        pairIJ.K = pairJI.K = pairIJ.C * KernelEvaluator.ComputeKernelH1( l );

                        if ( hair.Particles[ j ].S > hair.Particles[ i ].S )
                        {
                            hair.Particles[ i ].NeighborsTip.Add( hair.Particles[ j ] );
                            hair.Particles[ j ].NeighborsRoot.Add( hair.Particles[ i ] );
                            pairIJ.IsRootI = true;
                            pairJI.IsRootI = false;
                        }
                        else
                        {
                            hair.Particles[ i ].NeighborsRoot.Add( hair.Particles[ j ] );
                            hair.Particles[ j ].NeighborsTip.Add( hair.Particles[ i ] );
                            pairIJ.IsRootI = false;
                            pairJI.IsRootI = true;
                        }

                        hair.ParticlePairs[ i, j ] = pairIJ;
                        hair.ParticlePairs[ j, i ] = pairJI;

                        pairIJ.OppositePair = pairJI;
                        pairJI.OppositePair = pairIJ;

                        hair.ParticlePairsIteration.Add( pairIJ );
                    }
                }
        }

        private static float calculateA( float thetaIJ, float thetaJI )
        {
            if ( (float)Math.Cos( thetaIJ ) * (float)Math.Cos( thetaJI ) < 0 )
                return Math.Abs( (float)Math.Cos( thetaIJ ) - (float)Math.Cos( thetaJI ) ) / 2.0f;

            return 0;
        }

        private void trimNeighbors()
        {
            foreach ( HairParticle hp in hair.Particles )
            {
                HairParticle particle = hp;

                hp.NeighborsRoot.Sort(
                    ( hp1, hp2 ) =>
                    -(hair.ParticlePairs[ particle.ID, hp1.ID ].K.CompareTo( hair.ParticlePairs[ particle.ID, hp2.ID ].K ))
                    );

                hp.NeighborsTip.Sort(
                    ( hp1, hp2 ) =>
                    -(hair.ParticlePairs[ particle.ID, hp1.ID ].K.CompareTo( hair.ParticlePairs[ particle.ID, hp2.ID ].K ))
                    );

                int countRoot = hp.NeighborsRoot.Count;
                int countTip = hp.NeighborsTip.Count;

                while ( countRoot + countTip > Const.Instance.MaxNeighbors )
                {
                    if ( countTip == 0 && countRoot == 0 )
                        break;

                    if ( countRoot == 0 )
                    {
                        countTip--;
                        continue;
                    }

                    if ( countTip == 0 )
                    {
                        countRoot--;
                        continue;
                    }

                    if ( hair.ParticlePairs[ hp.ID, hp.NeighborsRoot[ countRoot - 1 ].ID ].K < hair.ParticlePairs[ hp.ID, hp.NeighborsTip[ countTip - 1 ].ID ].K )
                        countRoot--;
                    else
                        countTip--;
                }

                for ( int i = countRoot; i < hp.NeighborsRoot.Count; i++ )
                {
                    hair.ParticlePairsIteration.Remove( hair.ParticlePairs[ hp.ID, hp.NeighborsRoot[ i ].ID ] );
                    hair.ParticlePairs[ hp.ID, hp.NeighborsRoot[ i ].ID ] = null;
                }
                hp.NeighborsRoot.RemoveRange( countRoot, hp.NeighborsRoot.Count - countRoot );

                for ( int i = countTip; i < hp.NeighborsTip.Count; i++ )
                {
                    hair.ParticlePairsIteration.Remove( hair.ParticlePairs[ hp.ID, hp.NeighborsTip[ i ].ID ] );
                    hair.ParticlePairs[ hp.ID, hp.NeighborsTip[ i ].ID ] = null;
                }
                hp.NeighborsTip.RemoveRange( countTip, hp.NeighborsTip.Count - countTip );

                hp.NeighborsRoot.Sort(
                    ( hp1, hp2 )
                    =>
                    hp1.ID.CompareTo( hp2.ID )
                    );

                hp.NeighborsTip.Sort(
                    ( hp1, hp2 )
                    =>
                    hp1.ID.CompareTo( hp2.ID )
                    );
            }
        }

        #endregion
    }
}