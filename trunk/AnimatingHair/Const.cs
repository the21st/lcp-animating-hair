using System;
using System.IO;

namespace AnimatingHair
{
    static class Const
    {
        public const float PI = (float)Math.PI;

        #region Simulation variables
        public static int Seed = 0;
        public static long Time = 0;
        public static float TimeStep = 0.1f;
        public static float H1;
        public static float H2;
        #endregion

        #region Global variables
        public static float AirFriction = 0.2f;
        public static float Gravity = 0.1f;
        #endregion

        #region Initialization constants

        public const int N_n = 12; // max neighbor particles
        public const float a_0 = 0.5f; // treshold pre slabo spojene pary

        #endregion

        #region Physical properties of hair

        public const float DensityOfHairMaterial = 120f;
        //public const float SecondMomentOfArea = 1.5e-10f;
        //public const float E = 6e9f; // elastic modulus - zodpoveda materialu
        public const float SecondMomentOfArea = 0.002f;
        public const float E = 1000; // elastic modulus - zodpoveda materialu
        //private const float hairMassFactor = 6.8e-11f;
        private const float hairMassFactor = 1000;
        public static float HairParticleMass()
        {
            return hairMassFactor * HairLength / HairParticleCount;
        }

        #endregion

        #region Custom properties of hair

        public static int HairParticleCount = 500;
        public static float HairLength = 2.5f;
        public static float s_r = 0.5f; // hlbka do ktorej maximalne idu root particles

        public static float d_c = 0.9f; // collision damping
        public static float d_f = 0.9f; // frictional damping
        public static float k_a = 0.1f; // magnitude of attraction-repulsion forces - pre udrziavanie density
        public static float rho_0 = 30f; // average density of hair

        #endregion

        #region Air
        public static int AirParticleCount = 6;
        public static float k_a_air = 0.1f; //
        public static float rho_0_air = 1f; // average density of air
        public static float DragCoefficient = 0.1f;
        private const float airMassFactor = 100;
        public static float AirParticleMass()
        {
            return airMassFactor / HairParticleCount;
        }
        #endregion

        public new static string ToString()
        {
            StringWriter stringWriter = new StringWriter();

            stringWriter.WriteLine( "Seed = " + Const.Seed );
            stringWriter.WriteLine( "HairParticleCount = " + Const.HairParticleCount );
            stringWriter.WriteLine( "HairLength = " + Const.HairLength );
            stringWriter.WriteLine( "s_r = " + Const.s_r );
            stringWriter.WriteLine( "AirParticleCount = " + Const.AirParticleCount );

            stringWriter.WriteLine( "AirFriction = " + Const.AirFriction );
            stringWriter.WriteLine( "Gravity = " + Const.Gravity );

            stringWriter.WriteLine( "d_c = " + Const.d_c );
            stringWriter.WriteLine( "d_f = " + Const.d_f );
            stringWriter.WriteLine( "k_a = " + Const.k_a );
            stringWriter.WriteLine( "rho_0 = " + Const.rho_0 );

            stringWriter.WriteLine( "DragCoefficient = " + Const.DragCoefficient );
            stringWriter.WriteLine( "k_a_air = " + Const.k_a_air );
            stringWriter.WriteLine( "rho_0_air = " + Const.rho_0_air );

            stringWriter.Flush();
            return stringWriter.ToString();
        }

        public static void FromString( string s )
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
                            Const.Seed = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "hairparticlecount":
                        {
                            Const.HairParticleCount = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "hairlength":
                        {
                            Const.HairLength = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "s_r":
                        {
                            Const.s_r = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "airparticlecount":
                        {
                            Const.AirParticleCount = int.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "airfriction":
                        {
                            Const.AirFriction = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "gravity":
                        {
                            Const.Gravity = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "d_c":
                        {
                            Const.d_c = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "d_f":
                        {
                            Const.d_f = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "k_a":
                        {
                            Const.k_a = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "rho_0":
                        {
                            Const.rho_0 = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "dragcoefficient":
                        {
                            Const.DragCoefficient = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "k_a_air":
                        {
                            Const.k_a_air = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                    case "rho_0_air":
                        {
                            Const.rho_0_air = float.Parse( lineParts[ 1 ] );
                            break;
                        }
                }
            }
        }
    }
}
