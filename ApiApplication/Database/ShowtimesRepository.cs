using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ShowtimeEntity> Add(ShowtimeEntity showtimeEntity)
        {
            await _context.Showtimes.AddAsync(showtimeEntity);
            await _context.SaveChangesAsync();
            return showtimeEntity;
        }

        public async Task<ShowtimeEntity> Delete(int id)
        {
            var q = await (from s in _context.Showtimes
                           where s.Id == id
                           select s).FirstOrDefaultAsync();
            if (q == null) return null;
            _context.Showtimes.Remove(q);
            await _context.SaveChangesAsync();
            return q;
        }

        public async Task<ShowtimeEntity> GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            var q = await (from s in _context.Showtimes
                           where filter(s.Movie.ToQueryable())
                           select s).FirstOrDefaultAsync();
            return q;
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollection()
        {
            return await GetCollection(null);
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter)
        {
            if (filter == null) return _context.Showtimes == null ? Enumerable.Empty<ShowtimeEntity>() : _context.Showtimes.Include(e=>e.Movie);
            return await _context.Showtimes.Include(e=>e.Movie).Where(s => filter(s.ToQueryable())).ToListAsync();
        }

        public async Task<ShowtimeEntity> Update(ShowtimeEntity showtimeEntity)
        {
            var q = await (from s in _context.Showtimes
                           where s.Id == showtimeEntity.Id
                           select s).FirstOrDefaultAsync();
            if (q == null) return null;
            q.AuditoriumId = showtimeEntity.AuditoriumId;
            q.EndDate = showtimeEntity.EndDate;
            q.Movie = showtimeEntity.Movie;
            q.Schedule = showtimeEntity.Schedule;
            q.StartDate = showtimeEntity.StartDate;
            _context.Update(q);
            await _context.SaveChangesAsync();
            return q;
        }
    }
}
