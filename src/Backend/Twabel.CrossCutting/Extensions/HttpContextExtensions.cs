
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Twabel.CrossCutting.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Get remote ip address, optionally allowing for x-forwarded-for header check
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="allowForwarded">Whether to allow x-forwarded-for header check</param>
        /// <returns>IPAddress</returns>
        public static IPAddress GetRemoteIPAddress(this HttpContext context, bool allowForwarded = true)
        {
#if DEBUG
            return IPAddress.Parse("127.0.0.1");
#endif

            if (allowForwarded)
            {
                // if you are allowing these forward headers, please ensure you are restricting context.Connection.RemoteIpAddress
                // to cloud flare ips: https://www.cloudflare.com/ips/
                string header = (context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? context.Request.Headers["CF-Connecting-IP"].FirstOrDefault());
                if (IPAddress.TryParse(header, out IPAddress ip))
                {
                    return ip;
                }
            }
            return context.Connection.RemoteIpAddress;
        }
    }
}
/*
This is a C# code snippet defining an extension method for the HttpContext class to get the remote IP address of the client making the HTTP request. The method also allows for checking the x-forwarded-for header, which is useful when the web application is behind a proxy server or load balancer.

The method is called GetRemoteIPAddress and takes two arguments: context, which is the current HTTP context, and allowForwarded, which is a boolean indicating whether to allow checking of the x-forwarded-for header. By default, allowForwarded is set to true.

In the method, there is a preprocessor directive #if DEBUG, which indicates that if the code is running in a debug environment, the method will always return the loopback IP address (127.0.0.1). This is useful for testing purposes.

If allowForwarded is true, the method checks for the x-forwarded-for header first. If it exists, the method attempts to parse the first IP address in the header using IPAddress.TryParse. If the parsing succeeds, the method returns the IP address. Otherwise, the method falls back to returning context.Connection.RemoteIpAddress.

Overall, this extension method provides a convenient way to get the remote IP address of the client making the HTTP request, with the option to check for the x-forwarded-for header in case the web application is behind a proxy server or load balancer.



*/