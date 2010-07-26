using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Rendering.Debug
{
    /// <summary>
    /// Provides debug rendering for voxel grid.
    /// </summary>
    class VoxelGridRenderer
    {
        private readonly VoxelGrid voxelGrid;

        public VoxelGridRenderer( VoxelGrid voxelGrid )
        {
            this.voxelGrid = voxelGrid;
        }

        /// <summary>
        /// Renders the voxels of the grid.
        /// Renders only those voxels, which contain at least 
        /// one particle at a given moment.
        /// </summary>
        internal void Render()
        {
            GL.Disable( EnableCap.Lighting );
            GL.Disable( EnableCap.Texture2D );

            for ( int i = 0; i < voxelGrid.VoxelCount[ 0 ]; i++ )
            {
                for ( int j = 0; j < voxelGrid.VoxelCount[ 1 ]; j++ )
                {
                    for ( int k = 0; k < voxelGrid.VoxelCount[ 2 ]; k++ )
                    {
                        if ( !RenderingOptions.Instance.OnlyShowOccupiedVoxels || voxelGrid.Grid[ i, j, k ].Particles.Count > 0 )
                        {
                            GL.Color3( Color.DarkBlue );
                            Vector3 newCorner = voxelGrid.CornerLocation;
                            newCorner.X += i * voxelGrid.VoxelSize;
                            newCorner.Y += j * voxelGrid.VoxelSize;
                            newCorner.Z += k * voxelGrid.VoxelSize;
                            drawCube( newCorner, voxelGrid.VoxelSize );
                        }
                    }
                }
            }
        }

        private static void drawCube( Vector3 cornerLocation, float edgeSize )
        {
            Vector3 corner = cornerLocation;
            float dx = edgeSize;
            float dy = edgeSize;
            float dz = edgeSize;

            GL.Begin( BeginMode.LineLoop );
            {
                GL.Vertex3( corner );
                corner.X += dx;
                GL.Vertex3( corner );
                corner.Y += dy;
                GL.Vertex3( corner );
                corner.X -= dx;
                GL.Vertex3( corner );
                corner.Y -= dy;
                GL.Vertex3( corner );

                corner.Z += dz;
                GL.Vertex3( corner );

                GL.Vertex3( corner );
                corner.X += dx;
                GL.Vertex3( corner );
                corner.Y += dy;
                GL.Vertex3( corner );
                corner.X -= dx;
                GL.Vertex3( corner );
                corner.Y -= dy;
                GL.Vertex3( corner );

                corner.Z -= dz;
            }
            GL.End();

            GL.Begin( BeginMode.Lines );
            {
                GL.Vertex3( corner );
                corner.Z += dz;
                GL.Vertex3( corner );
                corner.Z -= dz;

                corner.X += dx;

                GL.Vertex3( corner );
                corner.Z += dz;
                GL.Vertex3( corner );
                corner.Z -= dz;

                corner.Y += dy;

                GL.Vertex3( corner );
                corner.Z += dz;
                GL.Vertex3( corner );
                corner.Z -= dz;

                corner.X -= dx;

                GL.Vertex3( corner );
                corner.Z += dz;
                GL.Vertex3( corner );
                corner.Z -= dz;
            }
            GL.End();
        }
    }
}
