using System;

namespace arroba.suino.webapi.Domain.Exceptions
{
    public class JwtServiceException : Exception
    {
        public JwtServiceException(string message) : base(message)
        {

        }
    }
}