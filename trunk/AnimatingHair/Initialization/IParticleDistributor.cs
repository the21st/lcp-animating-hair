using System.Collections.Generic;

namespace AnimatingHair.Initialization
{
    /// <summary>
    /// Defines a interface for particle distribution methods.
    /// </summary>
    interface IParticleDistributor
    {
        IEnumerable<ParticleCoordinate> DistributeParticles( int particleCount );
    }
}