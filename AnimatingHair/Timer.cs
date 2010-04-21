using System;

namespace AnimatingHair
{
    /// <summary>
    /// Provides time measurements.
    /// </summary>
    static class Timer
    {
        // store last time sample
        private static int lastTime = Environment.TickCount;
        private static float etime;

        /// <summary>
        /// Calculate and return elapsed time since last call.
        /// </summary>
        /// <returns></returns>
        public static float GetETime()
        {
            etime = Environment.TickCount - lastTime;
            lastTime = Environment.TickCount;

            return etime;
        }
    }
}
