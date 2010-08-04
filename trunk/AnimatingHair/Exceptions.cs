using System;

namespace AnimatingHair
{
    public class WrongFileFormatException : Exception
    {
        public WrongFileFormatException( string message ) : base( message ) { }
    }

    public class OpenGLException : Exception
    {
        public OpenGLException( string message ) : base( message ) { }
    }
}
