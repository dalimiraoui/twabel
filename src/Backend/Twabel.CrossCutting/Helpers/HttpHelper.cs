using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twabel.CrossCutting.Extensions;
using Twabel.CrossCutting.IHelpers;

namespace Twabel.CrossCutting.Helpers
{
    public class HttpHelper : IHttpHelper
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<IHttpHelper> _logger;


        public HttpHelper(IHttpClientFactory clientFactory, IWebHostEnvironment env, ILogger<IHttpHelper> logger)
        {
            _clientFactory = clientFactory;
            _env = env;
            _logger = logger;
        }

        private HttpClient GetHttpClient()
        {
            return this._env.IsDevelopment() ? _clientFactory.CreateClient("HttpClientWithSSLUntrusted") : new HttpClient();
        }

        public async Task<T> Get<T>(string address, int secondsTimeOut = 5)
        {
            using var client = GetHttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, address);
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOut);
            using var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<T> Get<T>(string address, string bearerToken, int secondsTimeOut = 5)
        {
            using var client = GetHttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, address);
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOut);
            if (!string.IsNullOrWhiteSpace(bearerToken))
            {
                bearerToken = bearerToken.Replace(@"Bearer ", "");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }
            using var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<HttpResponseMessage> Post(string apiAddress, int secondsTimeOut = 5)
        {
            using var client = GetHttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, apiAddress);
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOut);

            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> Post(object input, string apiAddress, int secondsTimeOut = 5)
        {
            using var client = GetHttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, apiAddress);
            using var httpContent = HttpClientExtentions.CreateHttpContent(input);
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOut);
            request.Content = httpContent;

            return await client.SendAsync(request);
        }

        public async Task<T> Post<T, TT>(TT input, string apiAddress, int secondsTimeOut = 5)
        {
            using var client = GetHttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, apiAddress);
            using var httpContent = HttpClientExtentions.CreateHttpContent(input);
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOut);
            request.Content = httpContent;

            using var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<T> Post<T, TT>(TT input, string apiAddress, string bearerToken, int secondsTimeOut = 5)
        {
            using var client = GetHttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, apiAddress);
            using var httpContent = HttpClientExtentions.CreateHttpContent(input);
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOut);
            request.Content = httpContent;
            if (!string.IsNullOrWhiteSpace(bearerToken))
            {
                bearerToken = bearerToken.Replace(@"Bearer ", "");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            using var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<T> GetAsync<T>(string apiAddress, int secondsTimeOut = 5)
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(secondsTimeOut);
            using var request = new HttpRequestMessage(HttpMethod.Get, apiAddress);
            using var response = await client
                .SendAsync(request)
                .ConfigureAwait(true);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}
/*
This is a C# class named "HttpHelper" that implements the "IHttpHelper" interface. 
The class provides methods for making HTTP requests and deserializing JSON responses.

The constructor takes in an "IHttpClientFactory", an "IWebHostEnvironment", 
and an "ILogger<IHttpHelper>" as parameters.

The class has several methods, including:

Get: A method for making a GET request to a given address and deserializing the response to the specified type.
Post: A method for making a POST request to a given address with or without a payload, and returning the response 
as an HttpResponseMessage or deserializing it to the specified type.
GetAsync: A method for making a GET request to a given address and returning the response as an HttpResponseMessage.
The class also has a private method named "GetHttpClient" that returns an HttpClient object based on whether 
the environment is set to Development or not.

Overall, this class provides a convenient and reusable way to make HTTP requests and handle JSON responses in a 
.NET Core application.

*/