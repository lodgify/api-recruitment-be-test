using ApiApplication.Database.Entities;
using ApiApplication.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;
        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<ShowtimeEntity> GetByIdAsync(int id)
        {
            return await _context.Showtimes.Where(l => l.Id == id).Include(l => l.Movie).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetAsync(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            return await _context.Showtimes.Where(filter ?? (l => true)).Include(l => l.Movie).ToListAsync();
        }

        public async Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity)
        {
            _context.Showtimes.Add(showtimeEntity);
            await _context.SaveChangesAsync();

            return showtimeEntity;
        }

        public async Task<ShowtimeEntity> DeleteAsync(int id)
        {
            var removable = await _context.Showtimes.Where(l => l.Id == id).FirstOrDefaultAsync();

            _context.Remove(removable);
            await _context.SaveChangesAsync();

            return removable;
        }

        public async Task<ShowtimeEntity> DeleteAsync(ShowtimeEntity showtimeEntity)
        {
            _context.Remove(showtimeEntity);
            await _context.SaveChangesAsync();

            return showtimeEntity;
        }

        public async Task<ShowtimeEntity> GetByMovieAsync(Expression<Func<MovieEntity, bool>> filter)
        {
            return await _context
                .Showtimes
                .Where(s => 
                    _context.Showtimes
                        .Select(l => l.Movie)
                        .Where(filter)
                        .Select(m => m.ShowtimeId)
                        .Contains(s.Id))
                .Include(l => l.Movie)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync()
        {
            return await GetAsync(null);
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync(Func<IQueryable<ShowtimeEntity>, bool> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<ShowtimeEntity> UpdateAsync(ShowtimeEntity showtimeEntity)
        {
            _context.Showtimes.Update(showtimeEntity);
            await _context.SaveChangesAsync();

            return showtimeEntity;
        }
    }
}
