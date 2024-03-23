using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twabel.CrossCutting.IHelpers
{
    public interface IHttpHelper
    {
        Task<T> Get<T>(string address, int secondsTimeOut = 5);
        Task<T> Get<T>(string address, string bearerToken, int secondsTimeOut = 5);
        Task<HttpResponseMessage> Post(string address, int secondsTimeOut = 5);
        Task<HttpResponseMessage> Post(object input, string address, int secondsTimeOut = 5);
        Task<T> Post<T, TT>(TT input, string apiAddress, int secondsTimeOut = 5);
        Task<T> Post<T, TT>(TT input, string apiAddress, string bearerToken, int secondsTimeOut = 5);
        Task<T> GetAsync<T>(string apiAddress, int secondsTimeOut = 5);
    }
}