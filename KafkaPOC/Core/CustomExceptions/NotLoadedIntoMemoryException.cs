using System;

namespace Core.CustomExceptions
{
    public class NotLoadedIntoMemoryException : Exception
    {
        public NotLoadedIntoMemoryException()
        {

        }

        public NotLoadedIntoMemoryException(string message) : base(message)
        {

        }

    }
}
