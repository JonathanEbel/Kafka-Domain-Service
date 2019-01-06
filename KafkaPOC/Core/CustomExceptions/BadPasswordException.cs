using System;

namespace Core.CustomExceptions
{
    public class BadPasswordException : Exception
    {
        public BadPasswordException()
        {

        }

        public BadPasswordException(string message) : base(message)
        {

        }
    }
}
