using ApiApplication.ImdbService.Models;
using System.Threading.Tasks;

namespace ApiApplication.ImdbService.Service
{
    public interface IImdbService
    {
         Task<Movie> FetchMovieInformation(string imdbId);
    }
}
