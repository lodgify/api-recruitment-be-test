using ApiApplication.Database.Entities;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IImdbService
    {
        public Task<MovieEntity> GetMovieAsync(string imdbId);
    }
}
