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
            GL.Color3( 0.0, 0.0, 0.8 );

            GL.PushMatrix();
            GL.Translate( particle.Position );
            Utility.DrawSphere( 0.03f, 3, 3 );
            GL.PopMatrix();

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
