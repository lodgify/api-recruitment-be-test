using ApiApplication.Database.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity)
        {
            _context.Add(showtimeEntity);

            await _context.SaveChangesAsync();

            return showtimeEntity;
        }

        public async Task<ShowtimeEntity> DeleteAsync(int id)
        {
            var showtimeEntity = await _context.Showtimes.FirstOrDefaultAsync(x => x.Id == id);

            if (showtimeEntity == null)
            {
                throw new Exception("Showtime not found and can't be deleted by id");
            }

            _context.Remove(showtimeEntity);

            await _context.SaveChangesAsync();

            return showtimeEntity;
        }

        public async Task<ShowtimeEntity> GetByMovieAsync(Func<MovieEntity, bool> filter)
        {
            ShowtimeEntity showtimeEntity = await _context.Showtimes                
                .Where(showtime => filter(showtime.Movie))
                .FirstOrDefaultAsync();

            return showtimeEntity;
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync()
        {
            return await GetCollectionAsync(null);
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            if (filter == null)
            {
                return await _context.Showtimes.ToListAsync();
            }

            return await _context.Showtimes.Include(showtime => showtime.Movie).Where(filter).ToListAsync();
        }

        public async Task<ShowtimeEntity> UpdateAsync(ShowtimeEntity showtimeEntity)
        {
            _context.Showtimes.Update(showtimeEntity);
            await _context.SaveChangesAsync();
            return showtimeEntity;
        }
    }
}
