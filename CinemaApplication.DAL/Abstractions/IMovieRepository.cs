using CinemaApplication.DAL.Models;
using System.Threading.Tasks;

namespace CinemaApplication.DAL.Repositories
{
    public interface IMovieRepository
    {
        Task<MovieEntity> GetAsync(string imdbId);

        Task UpdateAsync(MovieEntity movie);
    }
}
