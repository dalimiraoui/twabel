using System.Net;
using Twabel.CrossCutting;

namespace Twabel.CrossCutting.Exceptions
{
    public interface IReturnErrorClient
    {
        TwabelError TwabelError { get; }
        string Message { get; }
        HttpStatusCode StatusCode { get; }
    }
}
/*
The purpose of this interface is to define a common set of properties that an error response can implement. 
By implementing this interface, a class can provide a standardized way of returning error information, 
making it easier for clients to understand and handle errors in a consistent way.

In practice, a web service or API might return an object that implements this interface when an error occurs, 
allowing clients to extract the error information from the object and take appropriate action.

*/
