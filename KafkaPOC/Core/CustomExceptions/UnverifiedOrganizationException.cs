using System;

namespace Core.CustomExceptions
{
    public class UnverifiedOrganizationException : Exception
    {
        public UnverifiedOrganizationException()
        {

        }

        public UnverifiedOrganizationException(string message) : base(message)
        {

        }
    }
}
