using System.Net;
using System.Threading.Tasks;

namespace BackgroundTaskImdb.Services
{
    public interface IImdbService
    {
        public Task<HttpStatusCode> GetImdbStatusAsync();
    }
}
