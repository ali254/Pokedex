using System;

namespace Pokedex.Core.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) 
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public int StatusCode { get; set; }
    }
}
