using System.Collections.Generic;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Entity.PhysicalEntity
{
    /// <summary>
    /// An abstract particle used for smoothed particle hydrodynamics simulation.
    /// </summary>
    abstract class SPHParticle : PointMass
    {
        /// <summary>
        /// The density of the fluid volume at the position of this particle
        /// </summary>
        public float Density { get; set; }

        /// <summary>
        /// The voxel this particle is contained in.
        /// </summary>
        public Voxel ContainedIn { get; set; }

        // list of the current HairParticle neighbors, along with list of corresponding pre-computed distances and kernel values for efficiency
        public volatile List<HairParticle> NeighborsHair = new List<HairParticle>();
        public volatile List<float> DistancesHair = new List<float>();
        public volatile List<float> KernelH2DistancesHair = new List<float>();

        // list of the current AirParticle neighbors, along with list of corresponding pre-computed distances and kernel values for efficiency
        public volatile List<AirParticle> NeighborsAir = new List<AirParticle>();
        public volatile List<float> DistancesAir = new List<float>();
        public volatile List<float> KernelH2DistancesAir = new List<float>();

        public override void RKStep( int stepNumber )
        {
            Acceleration = Force / Mass;
            base.RKStep( stepNumber );
        }
    }
}