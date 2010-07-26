using AnimatingHair.Auxiliary;
using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;

namespace AnimatingHair.Rendering.Debug
{
    /// <summary>
    /// Provides debug rendering for hair.
    /// </summary>
    class DebugHairRenderer
    {
        private readonly Hair hair;

        public DebugHairRenderer( Hair hair )
        {
            this.hair = hair;
        }

        /// <summary>
        /// Renders all hair particles as two triangles at the particles position.
        /// Also renders their direction and current acceleration as lines.
        /// </summary>
        internal void Render()
        {
            GL.Disable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Lighting );

            foreach ( HairParticle hp in hair.Particles )
            {
                renderParticle( hp );
            }

            if ( !RenderingOptions.Instance.ShowConnections )
                return;

            for ( int i = 0; i < hair.ParticlePairsIteration.Count; i++ )
            {
                renderPair( hair.ParticlePairsIteration[ i ] );
            }
        }

        private static void renderParticle( HairParticle hp )
        {
            if ( hp.IsRoot )
                GL.Color3( 0, 0, 0 );
            else
                GL.Color3( 0.999, 0.01, 0.01 );

            if ( hp.NeighborsRoot.Count + hp.NeighborsTip.Count == 0 )
                GL.Color3( Color.White );

            GL.PushMatrix();
            GL.Translate( hp.Position );
            Utility.DrawSphere( 0.02f, 10, 10 );
            GL.PopMatrix();

            //return;

            // -- direction --
            if ( !hp.IsRoot )
            {
                GL.Color3( 0.3, 0.3, 0.3 );
                GL.Begin( BeginMode.Lines );
                {
                    GL.Vertex3( hp.Position );
                    GL.Vertex3( (hp.Position + 0.2f * hp.Direction) );
                }
                GL.End();
            }

            // -- acceleration --
            if ( !hp.IsRoot )
            {
                GL.Color3( 0.9, 0.35, 0.1 );

                GL.Begin( BeginMode.Lines );
                {
                    GL.Vertex3( hp.Position );
                    GL.Vertex3( (hp.Position + 0.1f * hp.Acceleration) );
                }
                GL.End();
            }
        }

        private static void renderPair( ParticlePair pair )
        {
            GL.Color3( 0.6, 0.8, 0.1 );
            GL.Begin( BeginMode.Lines );
            GL.Vertex3( pair.ParticleI.Position );
            GL.Vertex3( pair.ParticleJ.Position );
            GL.End();
        }
    }
}
