using System;
using System.IO;

namespace AnimatingHair
{
    class Const
    {
        public static readonly Const Instance = new Const();

        private Const()
        {
            Seed = 0;
            TimeStep = 0.01f;
            Parallel = true;

            AirFriction = 0.1f;
            Gravity = 0.7f;

            HairParticleCount = 2000;
            HairLength = 2.6f;
            MaxRootDepth = 0.5f;

            MaxNeighbors = 12;
            NeighborAlignmentTreshold = 0.5f;

            DensityOfHairMaterial = 40f;
            ElasticModulus = 3000;
            //ElasticModulus = 250000000f; // elastic modulus - zodpoveda materialu
            SecondMomentOfArea = 0.00005f;
            //SecondMomentOfArea = 1.5e-10f;
            HairMassFactor = 1000;
            //HairMassFactor = 6.8e-11f;

            CollisionDamp = 0.9f;
            FrictionDamp = 0.9f;
            AverageHairDensity = 220f;
            HairDensityForceMagnitude = 1f;

            AirParticleCount = 0;
            DragCoefficient = 0.1f;
            AverageAirDensity = 1f;
            AirDensityForceMagnitude = 0.1f;
            AirMassFactor = 100;
        }

        #region Simulation variables

        public int Seed { get; set; }

        public float TimeStep { get; set; }
        public bool Parallel { get; set; }

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
            // TODO: netreba sem zaratat aj density of hair material?
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
            stringWriter.WriteLine( "TimeStep = " + Const.Instance.TimeStep );

            stringWriter.WriteLine( "AirFriction = " + Const.Instance.AirFriction );
            stringWriter.WriteLine( "Gravity = " + Const.Instance.Gravity );

            stringWriter.WriteLine( "HairParticleCount = " + Const.Instance.HairParticleCount );
            stringWriter.WriteLine( "HairLength = " + Const.Instance.HairLength );
            stringWriter.WriteLine( "MaxRootDepth = " + Const.Instance.MaxRootDepth );

            stringWriter.WriteLine( "MaxNeighbors = " + Const.Instance.MaxNeighbors );
            stringWriter.WriteLine( "NeighborAlignmentTreshold = " + Const.Instance.NeighborAlignmentTreshold );

            stringWriter.WriteLine( "DensityOfHairMaterial = " + Const.Instance.DensityOfHairMaterial );
            stringWriter.WriteLine( "ElasticModulus = " + Const.Instance.ElasticModulus );
            stringWriter.WriteLine( "SecondMomentOfArea = " + Const.Instance.SecondMomentOfArea );
            stringWriter.WriteLine( "HairMassFactor = " + Const.Instance.HairMassFactor );

            stringWriter.WriteLine( "CollisionDamp = " + Const.Instance.CollisionDamp );
            stringWriter.WriteLine( "FrictionDamp = " + Const.Instance.FrictionDamp );
            stringWriter.WriteLine( "AverageHairDensity = " + Const.Instance.AverageHairDensity );
            stringWriter.WriteLine( "HairDensityForceMagnitude = " + Const.Instance.HairDensityForceMagnitude );

            stringWriter.WriteLine( "AirParticleCount = " + Const.Instance.AirParticleCount );
            stringWriter.WriteLine( "DragCoefficient = " + Const.Instance.DragCoefficient );
            stringWriter.WriteLine( "AverageAirDensity = " + Const.Instance.AverageAirDensity );
            stringWriter.WriteLine( "AirDensityForceMagnitude = " + Const.Instance.AirDensityForceMagnitude );
            stringWriter.WriteLine( "AirMassFactor = " + Const.Instance.AirMassFactor );

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
                    case "timestep":
                        {
                            Const.Instance.TimeStep = float.Parse( lineParts[ 1 ] );
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
                    case "maxrootdepth":
                        {
                            Const.Instance.MaxRootDepth = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "maxneighbors":
                        {
                            Const.Instance.MaxNeighbors = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "densityofhairmaterial":
                        {
                            Const.Instance.DensityOfHairMaterial = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "elasticmodulus":
                        {
                            Const.Instance.ElasticModulus = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "secondmomentofarea":
                        {
                            Const.Instance.SecondMomentOfArea = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "hairmassfactor":
                        {
                            Const.Instance.HairMassFactor = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "neighboralignmenttreshold":
                        {
                            Const.Instance.NeighborAlignmentTreshold = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "collisiondamp":
                        {
                            Const.Instance.CollisionDamp = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "frictiondamp":
                        {
                            Const.Instance.FrictionDamp = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "averagehairdensity":
                        {
                            Const.Instance.AverageHairDensity = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "hairdensityforcemagnitude":
                        {
                            Const.Instance.HairDensityForceMagnitude = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "airparticlecount":
                        {
                            Const.Instance.AirParticleCount = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "dragcoefficient":
                        {
                            Const.Instance.DragCoefficient = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "averageairdensity":
                        {
                            Const.Instance.AverageAirDensity = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "airdensityforcemagnitude":
                        {
                            Const.Instance.AirDensityForceMagnitude = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "airmassfactor":
                        {
                            Const.Instance.AirMassFactor = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "":
                        break;
                    default:
                        throw new Exception( "Wrong file format." );
                }
            }
        }
    }
}
