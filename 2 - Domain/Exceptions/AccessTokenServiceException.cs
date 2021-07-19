using System;

namespace arroba.suino.webapi.Domain.Exceptions
{
    public class AccessTokenServiceException : Exception
    {
        public AccessTokenServiceException() : base("")
        {
        }

        public AccessTokenServiceException(string message) : base(message)
        {

        }
    }
}