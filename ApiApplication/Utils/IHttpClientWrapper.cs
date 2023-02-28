using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Utils
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsyncyHttpResponseMessage<T>(string uri, string requestUri);
        Task<T> GetAsync<T>(string uri, string requestUri);
    }
}
