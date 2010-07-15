using OpenTK;

namespace AnimatingHair.Auxiliary
{
    /// <summary>
    /// A static class for calculating the smoothing kernel W specified in the method.
    /// </summary>
    static class KernelEvaluator
    {
        private const float piInv = 1 / MathHelper.Pi;

        // the inverses and powers of the two smoothing lengths precalculated for efficiency
        private static float h1Inv_1, h1Inv_3, h1Inv_4, h1Inv_5, h2Inv_1, h2Inv_3, h2Inv_4, h2Inv_5;

        /// <summary>
        /// Precomputed value of kernel W(0, H2) for efficiency
        /// </summary>
        public static float Kernel0H2;

        public static void SetH1( float h1 )
        {
            h1Inv_1 = 1 / h1;
            h1Inv_3 = h1Inv_1 * h1Inv_1 * h1Inv_1;
            h1Inv_4 = h1Inv_1 * h1Inv_1 * h1Inv_1 * h1Inv_1;
            h1Inv_5 = h1Inv_1 * h1Inv_1 * h1Inv_1 * h1Inv_1 * h1Inv_1;
        }

        public static void SetH2( float h2 )
        {
            h2Inv_1 = 1 / h2;
            h2Inv_3 = h2Inv_1 * h2Inv_1 * h2Inv_1;
            h2Inv_4 = h2Inv_1 * h2Inv_1 * h2Inv_1 * h2Inv_1;
            h2Inv_5 = h2Inv_1 * h2Inv_1 * h2Inv_1 * h2Inv_1 * h2Inv_1;

            Kernel0H2 = ComputeKernelH2( 0 );
        }

        public static float ComputeKernelH1( float r )
        {
            float s = r * h1Inv_1;

            if ( s < 1 )
            {
                float factor = 0.25f * piInv * h1Inv_3;
                return factor * (4 + 3 * s * s * (s - 2));
            }
            else if ( s < 2 )
            {
                float factor = 0.25f * piInv * h1Inv_3;
                float tmp = 2 - s;
                return factor * tmp * tmp * tmp;
            }

            return 0;
        }

        public static float ComputeKernelH2( float r )
        {
            float s = r * h2Inv_1;

            if ( s < 1 )
            {
                float factor = 0.25f * piInv * h2Inv_3;
                return factor * (4 + 3 * s * s * (s - 2));
            }
            else if ( s < 2 )
            {
                float factor = 0.25f * piInv * h2Inv_3;
                float tmp = 2 - s;
                return factor * tmp * tmp * tmp;
            }

            return 0;
        }

        public static Vector3 ComputeKernelGradientH1( Vector3 distanceVector )
        {
            float r = distanceVector.Length;
            float s = r * h1Inv_1;

            if ( s < 1 )
            {
                float c = 2.25f * piInv * h1Inv_5;
                return distanceVector * (c * (s - 4.0f / 3.0f));
            }
            else if ( s < 2 )
            {
                float c = -0.75f * piInv * h1Inv_4;
                float tmp = 2 - s;
                return distanceVector * (c * tmp * tmp / r);
            }
            return Vector3.Zero;
        }

        public static Vector3 ComputeKernelGradientH2( Vector3 distanceVector, float distance )
        {
            float r = distance;
            float s = r * h2Inv_1;

            if ( s < 1 )
            {
                float c = 2.25f * piInv * h2Inv_5;
                return distanceVector * (c * (s - 4.0f / 3.0f));
            }
            else if ( s < 2 )
            {
                float c = -0.75f * piInv * h2Inv_4;
                float tmp = 2 - s;
                return distanceVector * (c * tmp * tmp / r);
            }
            return Vector3.Zero;
        }
    }
}
