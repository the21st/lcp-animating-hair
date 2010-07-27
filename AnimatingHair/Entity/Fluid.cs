using AnimatingHair.Entity.PhysicalEntity;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Entity
{
    class Fluid<TParticle> where TParticle : SPHParticle
    {
        /// <summary>
        /// The actual particles that represent the fluid volume.
        /// </summary>
        public TParticle[] Particles;

        /// <summary>
        /// Calculates the SPH density at the particles' positions,
        /// using the formula from the method.
        /// </summary>
        public void CalculateDensity()
        {
            if ( Const.Instance.Parallel )
                Parallel.For( 0, Particles.Length, index =>
                {
                    SPHParticle particle = Particles[ index ];

                    float sum = particle.Mass * KernelEvaluator.Kernel0H2;

                    for ( int i = 0; i < particle.NeighborsHair.Count; i++ )
                    {
                        sum += particle.NeighborsHair[ i ].Mass *
                            particle.KernelH2DistancesHair[ i ];
                    }

                    particle.Density = sum;
                } );
            else
                for ( int index = 0; index < Particles.Length; index++ )
                {
                    SPHParticle particle = Particles[ index ];

                    float sum = particle.Mass * KernelEvaluator.Kernel0H2;

                    for ( int i = 0; i < particle.NeighborsHair.Count; i++ )
                    {
                        sum += particle.NeighborsHair[ i ].Mass *
                               particle.KernelH2DistancesHair[ i ];
                    }

                    particle.Density = sum;
                }
        }

        /// <summary>
        /// Invokes a step in the Runge-Kutta integration.
        /// 0 is the initialization step.
        /// 1-4 are the integration steps.
        /// 5 is the finalization step.
        /// </summary>
        /// <param name="stepNumber">Only values 0-5</param>
        public void RKStep( int stepNumber )
        {
            for ( int i = 0; i < Particles.Length; i++ )
            {
                Particles[ i ].RKStep( stepNumber );
            }
        }
    }
}
