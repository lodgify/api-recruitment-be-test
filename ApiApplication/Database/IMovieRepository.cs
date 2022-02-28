using ApiApplication.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public interface IMovieRepository
    {
        Task<MovieEntity> Add(MovieEntity movieEntity);

        Task<MovieEntity> Delete(int id);

        Task<IEnumerable<MovieEntity>> GetCollection();

        Task<MovieEntity> Get(int id);

        Task<MovieEntity> Update(MovieEntity movieEntity);
        Task<MovieEntity> GetShowtimeMovies(int showtimeId);
    }
}
