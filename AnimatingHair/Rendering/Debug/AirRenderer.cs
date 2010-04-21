using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace AnimatingHair.Rendering.Debug
{
    /// <summary>
    /// Provides debug rendering for air particles.
    /// </summary>
    class AirRenderer
    {
        private readonly Air air;

        public AirRenderer( Air air )
        {
            this.air = air;
        }

        internal void Render()
        {
            GL.Disable( EnableCap.Texture2D );
            GL.Disable( EnableCap.Lighting );
            for ( int i = 0; i < air.Particles.Length; i++ )
            {
                renderParticle( air.Particles[ i ] );
            }
        }

        private static void renderParticle( AirParticle particle )
        {
            GL.Color3( 0.0, 0.0, 1.0 );

            const float renderSize = 0.05f;

            GL.PointSize( 3 );

            GL.Begin( BeginMode.Triangles );
            {
                GL.Vertex3( particle.Position );
                GL.Vertex3( (particle.Position + new Vector3( renderSize, renderSize, 0 )) );
                GL.Vertex3( (particle.Position + new Vector3( -renderSize, renderSize, 0 )) );
                GL.Vertex3( particle.Position );
                GL.Vertex3( (particle.Position + new Vector3( 0, renderSize, renderSize )) );
                GL.Vertex3( (particle.Position + new Vector3( 0, renderSize, -renderSize )) );
            }
            GL.End();

            // -- velocity --
            GL.Color3( 0.0, 0.0, 0.0 );
            GL.Begin( BeginMode.Lines );
            {
                GL.Vertex3( particle.Position );
                GL.Vertex3( (particle.Position + 0.5f * particle.Velocity) );
            }
            GL.End();
            return;

            // -- acceleration --
            {
                GL.Color3( 0.9, 0.1, 0.1 );
                GL.Begin( BeginMode.Lines );
                {
                    GL.Vertex3( particle.Position );
                    GL.Vertex3( (particle.Position + particle.Acceleration) );
                }
                GL.End();
            }
        }
    }
}
