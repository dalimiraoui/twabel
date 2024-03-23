using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;

namespace Twabel.CrossCutting
{
    public class ErrorDetails
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TwabelError TwabelError { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum TwabelError
    {
        Twabel_ERROR = 0,
        UNAUTHORIZED = 1,
        INVALID_CUSTOMER = 3,
        EMAIL_NOT_AUTHORIZED = 4,
        NOT_ABLE_TO_PERFORM_ACTION = 5,
    }
}
/*
This is a C# code snippet that defines two classes: ErrorDetails and TwabelError.

ErrorDetails is a class that represents an error response returned by a web API. It has the following properties:

TwabelError: an instance of the TwabelError enum that indicates the type of error that occurred.
Message: a nullable string that provides additional details about the error.
StatusCode: an instance of the HttpStatusCode enum that indicates the HTTP status code of the response.
The ToString() method of the ErrorDetails class uses the JsonConvert.SerializeObject() method from the Newtonsoft.Json library to convert the object to a JSON string.

TwabelError is an enum that defines a set of error codes that can be returned by the API. Each member of the enum has a numeric value that corresponds to a specific error code.

The [JsonConverter(typeof(StringEnumConverter))] attribute on the TwabelError property of the ErrorDetails class specifies that the enum should be serialized as a string when converting the ErrorDetails object to JSON. This is necessary because JSON does not support enum values natively. The StringEnumConverter is a converter provided by the Newtonsoft.Json library that handles this conversion.

*/

/*
The crossCutting folder in your .NET project is likely a folder that contains files that are used across multiple layers or modules of the application.

In software development, the term "cross-cutting concerns" refers to features or aspects of an application that affect multiple parts of the codebase. Examples of cross-cutting concerns include logging, caching, security, error handling, and configuration.

To avoid duplication of code and promote code reusability, it is common to separate cross-cutting concerns from the rest of the application code and place them in a separate module or layer. The crossCutting folder in your project may be an example of this separation.

Typically, cross-cutting concerns are implemented using aspect-oriented programming (AOP) techniques such as interception, weaving, or code injection. The specific implementation details depend on the programming language, framework, and libraries used in the project.

*/
