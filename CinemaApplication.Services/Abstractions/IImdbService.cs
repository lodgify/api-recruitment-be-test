using CinemaApplication.Services.Models;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Abstractions
{
    public interface IImdbService
    {
        Task<ServiceDataResult<bool>> GetImdbStatus();

        Task<ServiceDataResult<ImdbMovie>> GetMovieAsync(string imdbId);
    }
}
