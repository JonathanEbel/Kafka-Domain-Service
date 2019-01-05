using System;

namespace Core.CustomExceptions
{
    public class DuplicateResourceException : Exception
    {
        public DuplicateResourceException()
        {

        }

        public DuplicateResourceException(string message) : base(message)
        {

        }

    }
}
