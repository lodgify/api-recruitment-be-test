using System.Net;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IImdbService
    {
        public HttpStatusCode GetStatus();
    }
}
