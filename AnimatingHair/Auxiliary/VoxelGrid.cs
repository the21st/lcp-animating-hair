using System;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;

namespace AnimatingHair.Auxiliary
{
    /// <summary>
    /// The auxiliary data structure used for efficient neighbor-search.
    /// The voxels are cube-shaped.
    /// </summary>
    class VoxelGrid
    {
        #region Fields

        public readonly float VoxelSize;

        /// <summary>
        /// The numbers of voxels in the X, Y and Z direction (not the total number of voxels).
        /// </summary>
        public readonly int[] VoxelCount = new int[ 3 ];

        public readonly Vector3 CornerLocation;

        public readonly Voxel[ , , ] Grid;

        /// <summary>
        /// An additional voxel containing all particles outside the grid.
        /// </summary>
        private readonly Voxel outside;

        private readonly float voxelSizeInv;

        #endregion

        /// <summary>
        /// Initializes a new voxel grid that is empty.
        /// </summary>
        /// <param name="cornerLocation">Position of the corner of the first voxel. The voxel grid spans in positive direction.</param>
        /// <param name="size">The size of the whole voxel grid (not of one voxel).</param>
        /// <param name="voxelSize">The size of one voxel</param>
        public VoxelGrid( Vector3 cornerLocation, float[] size, float voxelSize )
        {
            voxelSizeInv = 1f / voxelSize;

            CornerLocation = cornerLocation;

            VoxelSize = voxelSize;

            VoxelCount[ 0 ] = (int)Math.Ceiling( size[ 0 ] / VoxelSize );
            VoxelCount[ 1 ] = (int)Math.Ceiling( size[ 1 ] / VoxelSize );
            VoxelCount[ 2 ] = (int)Math.Ceiling( size[ 2 ] / VoxelSize );

            outside = new Voxel( -1, -1, -1 );

            Grid = new Voxel[ VoxelCount[ 0 ], VoxelCount[ 1 ], VoxelCount[ 2 ] ]; // TODO: tuto to pada pri particlecount = 1

            for ( int i = 0; i < VoxelCount[ 0 ]; i++ )
            {
                for ( int j = 0; j < VoxelCount[ 1 ]; j++ )
                {
                    for ( int k = 0; k < VoxelCount[ 2 ]; k++ )
                    {
                        Grid[ i, j, k ] = new Voxel( i, j, k );
                    }
                }
            }
        }

        /// <summary>
        /// Updates the particle's position in the voxel grid.
        /// </summary>
        public void UpdateParticle( SPHParticle particle )
        {
            Voxel oldContainer = particle.ContainedIn;
            Voxel newContainer = getCell( particle.Position );

            if ( oldContainer != newContainer )
            {
                if ( oldContainer != null )
                    oldContainer.RemoveElement( particle );

                newContainer.AddElement( particle );
                particle.ContainedIn = newContainer;
            }
        }

        public void FindNeighbors( SPHParticle particle )
        {
            bool edgeOrOutside = false;

            int x = particle.ContainedIn.X;
            int y = particle.ContainedIn.Y;
            int z = particle.ContainedIn.Z;
            Voxel currentCell;

            for ( int i = -1; i <= 1; i++ )
            {
                for ( int j = -1; j <= 1; j++ )
                {
                    for ( int k = -1; k <= 1; k++ )
                    {
                        int actX = x + i;
                        int actY = y + j;
                        int actZ = z + k;

                        if ( actX < 0 || actX >= VoxelCount[ 0 ] || actY < 0 || actY >= VoxelCount[ 1 ] || actZ < 0 || actZ >= VoxelCount[ 2 ] )
                        {
                            edgeOrOutside = true;
                        }
                        else
                        {
                            currentCell = Grid[ actX, actY, actZ ];
                            for ( int l = 0; l < currentCell.Particles.Count; l++ )
                            {
                                processNeighbor( particle, currentCell.Particles[ l ] );
                            }
                        }
                    }
                }
            }

            if ( edgeOrOutside )
            {
                for ( int i = 0; i < outside.Particles.Count; i++ )
                {
                    processNeighbor( particle, outside.Particles[ i ] );
                }
            }
        }

        private void processNeighbor( SPHParticle particle, SPHParticle neighbor )
        {
            if ( particle.ID == neighbor.ID )
                return;

            float distance = (particle.Position - neighbor.Position).Length;

            if ( distance < VoxelSize )
            {
                float kernelH2 = KernelEvaluator.ComputeKernelH2( distance );

                if ( neighbor is HairParticle )
                {
                    particle.NeighborsHair.Add( neighbor as HairParticle );
                    particle.DistancesHair.Add( distance );
                    particle.KernelH2DistancesHair.Add( kernelH2 );
                    particle.NeighborHandledHair.Add( false );
                }
                else if ( neighbor is AirParticle )
                {
                    particle.NeighborsAir.Add( neighbor as AirParticle );
                    particle.DistancesAir.Add( distance );
                    particle.KernelH2DistancesAir.Add( kernelH2 );
                    particle.NeighborHandledAir.Add( false );
                }
            }
        }

        private Voxel getCell( Vector3 location )
        {
            int x, y, z;

            x = (int)Math.Floor( (location.X - CornerLocation.X) * voxelSizeInv );
            y = (int)Math.Floor( (location.Y - CornerLocation.Y) * voxelSizeInv );
            z = (int)Math.Floor( (location.Z - CornerLocation.Z) * voxelSizeInv );

            if ( x < 0 || x >= VoxelCount[ 0 ] || y < 0 || y >= VoxelCount[ 1 ] || z < 0 || z >= VoxelCount[ 2 ] )
            {
                return outside;
            }

            return Grid[ x, y, z ];
        }
    }
}