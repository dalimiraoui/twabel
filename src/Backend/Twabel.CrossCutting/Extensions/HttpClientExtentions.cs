using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Twabel.CrossCutting.Extensions
{
    public static class HttpClientExtentions
    {
        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Newtonsoft.Json.Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        public static HttpContent CreateHttpContent(object content)
        {
            HttpContent? httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        public static HttpContent CreateEmptyStringContent()
        {
            return new StringContent(string.Empty, Encoding.UTF8, "application/json");
        }
    }
}
/*
This is a C# code snippet defining three extension methods for the HttpClient class to handle HTTP request content.

The first method, SerializeJsonIntoStream, serializes an object into JSON and writes it to a stream. 
The method takes two arguments: value, which is the object to be serialized, and stream, 
which is the stream to write the serialized JSON to. The method uses a StreamWriter to write the JSON 
to the stream and a JsonSerializer to serialize the object into JSON.

The second method, CreateHttpContent, creates an HttpContent object from an object that can be serialized into JSON. 
The method takes one argument: content, which is the object to be serialized. If content is not null, 
the method creates a MemoryStream, serializes the object into JSON using the SerializeJsonIntoStream method, 
sets the ContentType header of the HttpContent object to "application/json", and returns the HttpContent object. 
If content is null, the method returns null.

The third method, CreateEmptyStringContent, creates an empty HttpContent object with a content type of "application/json". 
The method returns a StringContent object with an empty string as its content.

Overall, these extension methods provide a convenient way to create and handle HTTP request content in JSON format using the HttpClient class.



*/
