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
    class SimpleHairRenderer
    {
        private readonly Hair hair;
        public bool RenderConnections = false;

        public SimpleHairRenderer( Hair hair )
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

            if ( !RenderConnections )
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
                GL.Color3( 1.0, 0.0, 0.0 );

            const float renderSize = 0.05f;

            if ( hp.NeighborsRoot.Count + hp.NeighborsTip.Count == 0 )
                GL.Color3( Color.White );

            GL.Begin( BeginMode.Triangles );
            {
                GL.Vertex3( hp.Position );
                GL.Vertex3( (hp.Position + new Vector3( renderSize, renderSize, 0 )) );
                GL.Vertex3( (hp.Position + new Vector3( -renderSize, renderSize, 0 )) );
                GL.Vertex3( hp.Position );
                GL.Vertex3( (hp.Position + new Vector3( 0, renderSize, renderSize )) );
                GL.Vertex3( (hp.Position + new Vector3( 0, renderSize, -renderSize )) );
            }
            GL.End();

            // -- direction --
            if ( !hp.IsRoot )
            {
                GL.Color3( 0.0, 0.0, 0.0 );
                GL.Begin( BeginMode.Lines );
                {
                    GL.Vertex3( hp.Position );
                    GL.Vertex3( (hp.Position + 0.5f * hp.Direction) );
                }
                GL.End();
            }

            // -- acceleration --
            if ( !hp.IsRoot )
            {
                GL.Color3( 0.9, 0.1, 0.1 );

                GL.Begin( BeginMode.Lines );
                {
                    GL.Vertex3( hp.Position );
                    GL.Vertex3( (hp.Position + 0.5f * hp.Acceleration) );
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
