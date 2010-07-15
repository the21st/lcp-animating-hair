using System.IO;

namespace AnimatingHair
{
    class Const
    {
        public static readonly Const Instance = new Const();

        public Const()
        {
            Seed = 0;
            TimeStep = 0.05f;

            AirFriction = 0.05f;
            Gravity = 0.5f;

            HairParticleCount = 1000;
            HairLength = 2.3f;
            MaxRootDepth = 0.5f;

            MaxNeighbors = 12;
            NeighborAlignmentTreshold = 0.5f;

            DensityOfHairMaterial = 10f;
            ElasticModulus = 1000;
            //ElasticModulus = 250000000f; // elastic modulus - zodpoveda materialu
            SecondMomentOfArea = 0.002f;
            //SecondMomentOfArea = 1.5e-10f;
            HairMassFactor = 1000;
            //HairMassFactor = 6.8e-11f;

            CollisionDamp = 0.9f;
            FrictionDamp = 0.9f;
            AverageHairDensity = 30f;
            HairDensityForceMagnitude = 0.1f;

            AirParticleCount = 1;
            DragCoefficient = 0.1f;
            AverageAirDensity = 1f;
            AirDensityForceMagnitude = 0.1f;
            AirMassFactor = 100;
        }

        #region Simulation variables

        public int Seed { get; set; }

        public float TimeStep { get; set; }

        public long CurrentTimeStep = 0;
        public float H1;
        public float H2;

        #endregion

        #region Global variables

        public float Gravity { get; set; }

        public float AirFriction { get; set; }

        #endregion

        #region Hair initialization constants

        public int HairParticleCount { get; set; }

        public float HairLength { get; set; }

        public float MaxRootDepth { get; set; }

        public int MaxNeighbors { get; set; }

        public float NeighborAlignmentTreshold { get; set; }

        #endregion

        #region Physical properties of hair

        public float DensityOfHairMaterial { get; set; }

        public float ElasticModulus { get; set; }

        public float SecondMomentOfArea { get; set; }

        public float HairMassFactor { get; set; }

        public float HairParticleMass()
        {
            return HairMassFactor * HairLength / HairParticleCount;
        }

        #endregion

        #region Custom properties of hair

        public float CollisionDamp { get; set; }

        public float FrictionDamp { get; set; }

        public float AverageHairDensity { get; set; }

        public float HairDensityForceMagnitude { get; set; }

        #endregion

        #region Air

        public int AirParticleCount { get; set; }

        public float AverageAirDensity { get; set; }

        public float AirDensityForceMagnitude { get; set; }

        public float DragCoefficient { get; set; }

        public float AirMassFactor { get; set; }

        public float AirParticleMass()
        {
            return AirMassFactor / AirParticleCount;
        }

        #endregion

        public new string ToString()
        {
            StringWriter stringWriter = new StringWriter();

            stringWriter.WriteLine( "Seed = " + Const.Instance.Seed );
            stringWriter.WriteLine( "HairParticleCount = " + Const.Instance.HairParticleCount );
            stringWriter.WriteLine( "HairLength = " + Const.Instance.HairLength );
            stringWriter.WriteLine( "s_r = " + Const.Instance.MaxRootDepth );
            stringWriter.WriteLine( "AirParticleCount = " + Const.Instance.AirParticleCount );

            stringWriter.WriteLine( "AirFriction = " + Const.Instance.AirFriction );
            stringWriter.WriteLine( "Gravity = " + Const.Instance.Gravity );

            stringWriter.WriteLine( "d_c = " + Const.Instance.CollisionDamp );
            stringWriter.WriteLine( "d_f = " + Const.Instance.FrictionDamp );
            stringWriter.WriteLine( "k_a = " + Const.Instance.HairDensityForceMagnitude );
            stringWriter.WriteLine( "rho_0 = " + Const.Instance.AverageHairDensity );

            stringWriter.WriteLine( "DragCoefficient = " + Const.Instance.DragCoefficient );
            stringWriter.WriteLine( "k_a_air = " + Const.Instance.AirDensityForceMagnitude );
            stringWriter.WriteLine( "rho_0_air = " + Const.Instance.AverageAirDensity );

            stringWriter.Flush();
            return stringWriter.ToString();
        }

        public void FromString( string s )
        {
            StringReader stringReader = new StringReader( s );

            string line;
            string[] lineParts;
            while ( true )
            {
                line = stringReader.ReadLine();

                if ( line == null )
                    break;

                if ( line.Trim() == "" )
                    continue;

                lineParts = line.Split( '=' );
                for ( int i = 0; i < lineParts.Length; i++ )
                {
                    lineParts[ i ] = lineParts[ i ].Trim().ToLowerInvariant();
                }

                switch ( lineParts[ 0 ] )
                {
                    case "seed":
                        {
                            Const.Instance.Seed = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "hairparticlecount":
                        {
                            Const.Instance.HairParticleCount = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "hairlength":
                        {
                            Const.Instance.HairLength = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "s_r":
                        {
                            Const.Instance.MaxRootDepth = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "airparticlecount":
                        {
                            Const.Instance.AirParticleCount = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "airfriction":
                        {
                            Const.Instance.AirFriction = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "gravity":
                        {
                            Const.Instance.Gravity = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "d_c":
                        {
                            Const.Instance.CollisionDamp = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "d_f":
                        {
                            Const.Instance.FrictionDamp = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "k_a":
                        {
                            Const.Instance.HairDensityForceMagnitude = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "rho_0":
                        {
                            Const.Instance.AverageHairDensity = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "dragcoefficient":
                        {
                            Const.Instance.DragCoefficient = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "k_a_air":
                        {
                            Const.Instance.AirDensityForceMagnitude = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "rho_0_air":
                        {
                            Const.Instance.AverageAirDensity = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                }
            }
        }
    }
}
