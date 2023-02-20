using CinemaApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplication.DAL.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _dbContext;

        public MovieRepository(CinemaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MovieEntity> GetAsync(string imdbId)
        {
            var movie = await _dbContext.Movies
                 .SingleOrDefaultAsync(m => m.ImdbId == imdbId);

            return movie;
        }
    }
}
