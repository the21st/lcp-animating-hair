using System;
using System.Collections.Generic;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Entity.PhysicalEntity
{
    /// <summary>
    /// An abstract particle used for smoothed particle hydrodynamics simulation.
    /// </summary>
    abstract class SPHParticle : PointMass
    {
        protected SPHParticle( int id, float mass )
            : base( mass )
        {
            ID = id;
        }

        /// <summary>
        /// The density of the fluid volume at the position of this particle
        /// </summary>
        public float Density { get; set; }

        /// <summary>
        /// The voxel this particle is contained in.
        /// </summary>
        public Voxel ContainedIn { get; set; }

        public readonly int ID;

        // list of the current HairParticle neighbors, along with list of corresponding pre-computed distances and kernel values for efficiency
        public List<HairParticle> NeighborsHair = new List<HairParticle>();
        public List<float> DistancesHair = new List<float>();
        public List<float> KernelH2DistancesHair = new List<float>();
        public List<bool> NeighborHandledHair = new List<bool>();

        // list of the current AirParticle neighbors, along with list of corresponding pre-computed distances and kernel values for efficiency
        public List<AirParticle> NeighborsAir = new List<AirParticle>();
        public List<float> DistancesAir = new List<float>();
        public List<float> KernelH2DistancesAir = new List<float>();
        public List<bool> NeighborHandledAir = new List<bool>();

        public override void RKStep( int stepNumber )
        {
            Acceleration = Force * MassInverse;
            base.RKStep( stepNumber );
        }
    }
}