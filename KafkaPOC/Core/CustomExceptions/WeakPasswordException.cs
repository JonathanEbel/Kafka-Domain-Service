using System;

namespace Core.CustomExceptions
{
    public class WeakPasswordException : Exception
    {
        public WeakPasswordException()
        {

        }

        public WeakPasswordException(string message) : base(message)
        {

        }
    }
}
