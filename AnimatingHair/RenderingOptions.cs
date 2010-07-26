using OpenTK;

namespace AnimatingHair
{
    class RenderingOptions
    {
        public static readonly RenderingOptions Instance = new RenderingOptions();

        private RenderingOptions()
        {
            ShowHair = true;
            DebugHair = false;
            ShowConnections = false;

            BillboardLength = 0.5f;
            BillboardWidth = 0.15f;
            AlphaTreshold = 0.15f;
            DeepOpacityMapDistance = 0.015f;
            ShadowMapsResolution = 1024;

            ShowBust = false;
            ShowMetaBust = false;

            AmbientTerm = 0.05f;
            DiffuseTerm = 0.30f;
            SpecularTerm = 0.65f;
            Shininess = 180.0f;
            Reflect = 0.75f;
            Transmit = 0.25f;

            LightCruising = false;
            LightCruiseSpeed = 0.002f;
            LightDistance = 7;
            LightIntensity = 1.5f;

            ShowVoxelGrid = false;
            OnlyShowOccupiedVoxels = true;

            ShowDebugAir = false;

            Near = 1;
            Far = 30;

            BustScaleRatio = 0.85f;
            BustDisplacement = new Vector3( 0, -0.62f, -0.255f );

            Cutting = false;
        }

        public bool ShowHair { get; set; }
        public bool DebugHair { get; set; }
        public bool ShowConnections { get; set; }

        public float AmbientTerm { get; set; }
        public float DiffuseTerm { get; set; }
        public float SpecularTerm { get; set; }
        public float Shininess { get; set; }
        public float Reflect { get; set; }
        public float Transmit { get; set; }

        public bool DirectionalOpacity { get; set; }
        public float BillboardLength { get; set; }
        public float BillboardWidth { get; set; }
        public float AlphaTreshold { get; set; }
        public float DeepOpacityMapDistance { get; set; }
        public int ShadowMapsResolution { get; set; }

        public bool ShowBust { get; set; }
        public bool ShowMetaBust { get; set; }
        public bool OnlyRotateHead { get; set; }

        public bool LightCruising { get; set; }
        public float LightCruiseSpeed { get; set; }
        public float LightDistance { get; set; }
        public float LightIntensity { get; set; }

        public bool ShowVoxelGrid { get; set; }
        public bool OnlyShowOccupiedVoxels { get; set; }

        public bool ShowDebugAir { get; set; }

        public float Near { get; set; }
        public float Far { get; set; }
        public int RenderWidth { get; set; }
        public int RenderHeight { get; set; }
        public float AspectRatio { get; set; }

        public float BustScaleRatio { get; set; }
        public Vector3 BustDisplacement;

        public bool Cutting { get; set; }
    }
}
