using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _context;
        public MovieRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<MovieEntity> Add(MovieEntity movieEntity)
        {
            await _context.Movies.AddAsync(movieEntity);
            await _context.SaveChangesAsync();
            return movieEntity;
        }

        public async Task<MovieEntity> Delete(int id)
        {
            var movieEntity = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movieEntity);
            await _context.SaveChangesAsync();
            return movieEntity;
        }

        public async Task<IEnumerable<MovieEntity>> GetCollection()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<MovieEntity> Get(int id)
        {
            var movieEntity = await _context.Movies.FindAsync(id);
            return movieEntity;
        }

        public async Task<MovieEntity> Update(MovieEntity movieEntity)
        {
            var entity = await _context.Showtimes.FindAsync(movieEntity.Id);
            if (entity == null)
            {
                return null;
            }
            _context.Entry(entity).CurrentValues.SetValues(movieEntity);
            await _context.SaveChangesAsync();
            return movieEntity;

        }

        public async Task<MovieEntity> GetShowtimeMovies(int showtimeId)
        {
            return await _context.Movies.Where(m => m.ShowtimeId == showtimeId).FirstOrDefaultAsync();
        }
    }
}
