using CinemaApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
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
            => await _dbContext.Movies
                 .SingleOrDefaultAsync(m => m.ImdbId == imdbId);

        public async Task UpdateAsync(MovieEntity movie)
        {
            var updatedMovie = _dbContext.Movies.Update(movie);
            updatedMovie.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
