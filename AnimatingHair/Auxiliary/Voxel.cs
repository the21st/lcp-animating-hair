using System.Collections.Generic;
using AnimatingHair.Entity.PhysicalEntity;

namespace AnimatingHair.Auxiliary
{
    class Voxel
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public readonly List<SPHParticle> Particles;

        public Voxel(int x, int y, int z)
        {
            Particles = new List<SPHParticle>();

            X = x;
            Y = y;
            Z = z;
        }

        public void RemoveElement( SPHParticle particle )
        {
            Particles.Remove( particle );
        }

        public void AddElement( SPHParticle particle )
        {
            Particles.Add( particle );
        }
    }
}
