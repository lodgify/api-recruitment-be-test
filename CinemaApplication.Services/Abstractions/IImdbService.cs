using CinemaApplication.Services.Models;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Abstractions
{
    public interface IImdbService
    {
        Task<bool> GetImdbStatus();

        Task<ImdbMovie> GetMovieAsync(string imdbId);
    }
}
