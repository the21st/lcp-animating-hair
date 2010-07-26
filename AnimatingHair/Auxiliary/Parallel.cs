using System;

namespace AnimatingHair.Auxiliary
{
    public delegate void ForDelegate( int i );

    static class Parallel
    {
        private delegate void ThreadDelegate();

        /// <summary>
        /// ChunkSize = 1 makes items to be processed in order.
        /// Bigger chunk size should reduce lock waiting time and thus
        /// increase paralelism.
        /// </summary>
        const int chunkSize = 30;

        /// <summary>
        /// The number of process() threads
        /// </summary>
        private static readonly int threadCount = Environment.ProcessorCount;

        /// <summary>
        /// Parallel for loop. Invokes given action, passing arguments
        /// fromInclusive - toExclusive on multiple threads.
        /// Returns when loop finished.
        /// </summary>
        public static void For( int fromInclusive, int toExclusive, ForDelegate action )
        {
            int cnt = fromInclusive - chunkSize;

            // processing function
            // takes next chunk and processes it using action
            ThreadDelegate process = delegate()
            {
                while ( true )
                {
                    int cntMem;
                    lock ( typeof( Parallel ) )
                    {
                        // take next chunk
                        cnt += chunkSize;
                        cntMem = cnt;
                    }
                    // process chunk
                    // here items can come out of order if chunkSize > 1
                    for ( int i = cntMem; i < cntMem + chunkSize; ++i )
                    {
                        if ( i >= toExclusive ) return;
                        action( i );
                    }
                }
            };

            // launch process() threads
            IAsyncResult[] asyncResults = new IAsyncResult[ threadCount ];
            for ( int i = 0; i < threadCount; ++i )
            {
                asyncResults[ i ] = process.BeginInvoke( null, null );
            }
            // wait for all threads to complete
            for ( int i = 0; i < threadCount; ++i )
            {
                process.EndInvoke( asyncResults[ i ] );
            }
        }
    }

    static class Parallel2
    {
        private delegate void ThreadDelegate( int from );

        /// <summary>
        /// The number of process() threads
        /// </summary>
        private const int threadCount = 4;

        /// <summary>
        /// Parallel for loop. Invokes given action, passing arguments
        /// fromInclusive - toExclusive on multiple threads.
        /// Returns when loop finished.
        /// </summary>

        public static void For( int fromInclusive, int toExclusive, ForDelegate action )
        {
            int chunkSize = (int)Math.Ceiling( (toExclusive - fromInclusive) / (double)threadCount );

            // processing function
            // takes next chunk and processes it using action
            ThreadDelegate process = delegate( int from )
            {
                // process chunk
                // here items can come out of order if ChunkSize > 1
                for ( int i = from; i < from + chunkSize; ++i )
                {
                    if ( i >= toExclusive ) return;
                    action( i );
                }
            };

            // launch process() threads
            IAsyncResult[] asyncResults = new IAsyncResult[ threadCount ];
            for ( int i = 0; i < threadCount; ++i )
            {
                asyncResults[ i ] = process.BeginInvoke( fromInclusive + i * chunkSize, null, null );
            }
            // wait for all threads to complete
            for ( int i = 0; i < threadCount; ++i )
            {
                process.EndInvoke( asyncResults[ i ] );
            }
        }
    }
}