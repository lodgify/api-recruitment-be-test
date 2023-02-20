using CinemaApplication.DAL.Models;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Abstractions
{
    public interface IImdbService
    {
        Task<bool> GetImdbStatus();

        Task<MovieEntity> GetMovieAsync(string id);
    }
}
