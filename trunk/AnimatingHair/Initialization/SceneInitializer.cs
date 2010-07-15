using AnimatingHair.Entity;
using AnimatingHair.Entity.PhysicalEntity;
using OpenTK;
using AnimatingHair.Auxiliary;

namespace AnimatingHair.Initialization
{
    /// <summary>
    /// The top of the "initialization hierarchy". Initializes the
    /// whole scene entity.
    /// </summary>
    class SceneInitializer
    {
        private Scene scene;
        private readonly HairInitializer hairInitializer = new HairInitializer();
        private readonly AirInitializer airInitializer = new AirInitializer();
        private readonly BustInitializer bustInitializer = new BustInitializer();

        /// <summary>
        /// Creates and initializes the scene entity by creating and
        /// initializing the bust, hair and air objects (components of scene object).
        /// </summary>
        /// <returns>The newly created Scene object</returns>
        internal Scene InitializeScene()
        {
            Bust bust = bustInitializer.InitializeBust();
            Hair hair = hairInitializer.InitializeHair( bust );
            Air air = airInitializer.InitializeAir();

            // NOTE: constant
            VoxelGrid voxelGrid = new VoxelGrid( new Vector3( -3, -5, -3 ), new float[] { 7, 8, 10 }, 2 * Const.Instance.H2 );

            scene = new Scene
                    {
                        Air = air,
                        Bust = bust,
                        Hair = hair,
                        VoxelGrid = voxelGrid,
                        Particles = new SPHParticle[ Const.Instance.HairParticleCount + Const.Instance.AirParticleCount ]
                    };


            for ( int i = 0; i < scene.Hair.Particles.Length; i++ )
            {
                scene.Particles[ i ] = scene.Hair.Particles[ i ];
            }

            for ( int i = 0; i < scene.Air.Particles.Length; i++ )
            {
                scene.Particles[ scene.Hair.Particles.Length + i ] = scene.Air.Particles[ i ];
            }

            return scene;
        }
    }
}