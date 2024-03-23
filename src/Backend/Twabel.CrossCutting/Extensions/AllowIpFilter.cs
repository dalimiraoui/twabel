using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Twabel.CrossCutting.Extensions
{
    // this code provides a basic IP filtering mechanism that can be used to restrict access to a web application based on IP addresses.
    
    public class AllowIpAttribute : TypeFilterAttribute //used to apply a filter to a controller action or to a controller.
    {
        public AllowIpAttribute() : base(typeof(AllowIpFilter)) { }
    }

    public class AllowIpFilter : IAuthorizationFilter //used to authorize access to a resource
    {
        private readonly ILogger<AllowIpFilter> _logger;
        private readonly IConfiguration _configuration;

        public AllowIpFilter(
            ILogger<AllowIpFilter> logger,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _configuration = configuration;
        }
        // this method  is called before executing an action method. The method checks the x-forwarded-for header of the incoming request to get the IP address of the client. The IP address is then compared to a list of allowed IP addresses. If the IP address is not in the list of allowed IP addresses, then the request is denied and an UnauthorizedResult is returned.
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var forwardedForValue = context.HttpContext.Request.Headers["x-forwarded-for"];

            if (CannotPerformAction(forwardedForValue))
            {
                _logger.LogWarning($"Origin ip address {forwardedForValue} is not allowed");
                context.Result = new UnauthorizedResult();
            }
        }
        // this method checks whether the IP address is in the list of allowed IP addresses. If the list contains a wildcard "*", then all IP addresses are allowed. Otherwise, the IP address is compared to each IP address in the list of allowed IP addresses, and if it is not found, then the IP address is not allowed.
        private bool CannotPerformAction(string forwardedForValue)
        {
            List<string> ips = SplitIp(forwardedForValue, ',');

            string allowIp = _configuration["AllowIp"];
            List<string> allowIpList = SplitIp(allowIp, ';');

            if (allowIpList.Any(ip => "*".Equals(ip)))
            {
                return false;
            }

            return !allowIpList.Any(ip => ips.Contains(ip));
        }
        // this method is used to split the IP address string into a list of individual IP addresses. The IP addresses are separated by a delimiter (comma or semicolon).
        private List<string> SplitIp(string ipString, char delimiter)
        {
            var ips = new List<string>();

            if (!string.IsNullOrWhiteSpace(ipString))
            {
                ips = ipString.Split(delimiter).ToList();
            }

            return ips;
        }
    }
}
/*
This is a C# code snippet defining an IP filtering mechanism in a web application using ASP.NET Core framework. 
The purpose of this code is to restrict access to certain IP addresses based on a list of allowed IP addresses.

*/
