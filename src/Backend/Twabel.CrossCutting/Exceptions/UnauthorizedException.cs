using System.Net;
using Twabel.CrossCutting;

namespace Twabel.CrossCutting.Exceptions
{
    public class UnauthorizedException : BusinessLogicException, IReturnErrorClient
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
        public override TwabelError TwabelError => TwabelError.UNAUTHORIZED; // This indicates that the exception represents an unauthorized access error.
        public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized; // This indicates that the HTTP status code associated with the exception is 401 Unauthorized.
        
    }
}
/*
this code defines a custom exception class that represents an unauthorized access error, 
and provides a standardized way to return error information using the IReturnErrorClient interface.

*/


