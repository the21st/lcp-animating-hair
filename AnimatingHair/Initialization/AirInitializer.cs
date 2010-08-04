using System.Collections.Generic;
using AnimatingHair.Entity;
using AnimatingHair.Auxiliary;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;

namespace AnimatingHair.Initialization
{
    class AirInitializer
    {
        private Air air;

        public Air InitializeAir()
        {
            air = new Air
                  {
                      FanStrength = 30
                  };

            distributeParticles();

            return air;
        }

        private void distributeParticles()
        {
            IParticleDistributor distributor = new CylinderDistributor( Const.Instance.Seed, new Cylinder( new Vector3( 0, -1, 4 ), new Vector3( 0, -1, 7 ), 2 ) );

            IEnumerable<ParticleCoordinate> coordinates = distributor.DistributeParticles( Const.Instance.AirParticleCount );

            int i = 0;
            foreach ( ParticleCoordinate coordinate in coordinates )
            {
                AirParticle airParticle = new AirParticle( Const.Instance.HairParticleCount + i, Const.Instance.AirParticleMass() )
                                          {
                                              Position = coordinate.Position
                                          };

                air.Particles[ i ] = airParticle;
                i++;
            }
        }
    }
}