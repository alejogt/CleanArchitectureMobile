using System;
namespace poc.providers.api.Exceptions
{
    public class NoOptionalException : Exception
    {
        public NoOptionalException(string message) : base(message)
        { }
    }
}
