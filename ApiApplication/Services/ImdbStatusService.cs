using System.Net;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ImdbService : IImdbService
    {
        private readonly IImdb _imdb;

        public ImdbService(IImdb imdb) 
        { 
            _imdb = imdb;
        }
        HttpStatusCode IImdbService.GetStatus()
        {
            return _imdb.Status;
        }
    }
}
