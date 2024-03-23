using System;
using System.Net;
using Twabel.CrossCutting;

namespace Twabel.CrossCutting.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public virtual TwabelError TwabelError { get; set; } = TwabelError.Twabel_ERROR; //this represents the error type associated with the exception.

        public virtual HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

        public BusinessLogicException(string message) : base(message) 
        {
            //This constructor takes a single parameter message of type string, which is the message associated with the exception.
            // This constructor simply calls the base class constructor with the provided message.
        }

        public BusinessLogicException(string message, TwabelError TwabelError) : base(message)
        {
            TwabelError = TwabelError;
        }

        public BusinessLogicException(string message, TwabelError TwabelError, HttpStatusCode statusCode) : base(message)
        {
            TwabelError = TwabelError;
            StatusCode = statusCode;
        }
    }
}

/*
this code defines a custom exception class that can be used in business logic operations,
and allows for the specification of an error type and an HTTP status code associated with the exception.

*/


