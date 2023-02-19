using ApiApplication.Database.Entities;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public interface IMovieRepository
    {
        Task<MovieEntity> GetById(int id);
        Task<MovieEntity> GetByImdbId(string imdbId);
    }
}