using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;

namespace AnimatingHair.Auxiliary
{
    /// <summary>
    /// The auxiliary structure used to pair particles for lengthwise coherence of hair
    /// </summary>
    class ParticlePair
    {
        public ParticlePair OppositePair { get; set; }

        public HairParticle ParticleI { get; set; }
        public HairParticle ParticleJ { get; set; }

        // the IDs of the paired particles
        public int I { get; set; }
        public int J { get; set; }

        // the values stored throughout the simulation as specified by the method
        public float A { get; set; }
        public float C { get; set; }
        public float K { get; set; }
        public float L { get; set; }
        public Vector3 T { get; set; }
        public float Alpha { get; set; }
        public float Theta { get; set; }
        public float SinTheta { get; set; }
        public float CosTheta { get; set; }

        /// <summary>
        /// Indicates if PaticleI (with id I) is the one closer to the root of the hair.
        /// </summary>
        public bool IsRootI { get; set; }

        // precomputed values (at each step) for efficiency
        public float CurrentDistance { get; set; }
        public Vector3 CurrentPositionDifference { get; set; }

        public override string ToString()
        {
            return ParticleI.ID + " " + ParticleJ.ID;
        }
    }
}
