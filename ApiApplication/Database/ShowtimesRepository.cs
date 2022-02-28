using ApiApplication.Database.Entities;
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

        public async Task<ShowtimeEntity> Add(ShowtimeEntity showtimeEntity)
        {
            await _context.Showtimes.AddAsync(showtimeEntity);
            await _context.SaveChangesAsync();
            return showtimeEntity;
        }

        public async Task<ShowtimeEntity> Delete(int id)
        {
            var showtime = await _context.Showtimes.FindAsync(id);
            _context.Showtimes.Remove(showtime);
            await _context.SaveChangesAsync();
            return showtime;
        }

        public async Task<ShowtimeEntity> GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            return await _context.Showtimes.FindAsync(filter);
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetByAuditoriumId(int auditorumId)
        {
            return await _context.Showtimes.Where(s=> s.AuditoriumId == auditorumId).ToListAsync();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollection()
        {
            return await _context.Showtimes.ToListAsync();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollection(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            //return await _context.Showtimes.FindAsync(filter);
            //var r = _context.Showtimes.AsQueryable().Select(filter);
            //_context.Showtimes.AsQueryable().Include(filter);
            //return (IEnumerable<ShowtimeEntity>)await _context.Showtimes.FindAsync(filter);
            return await _context.Showtimes.Where(filter).ToListAsync();
        }

        public async Task<ShowtimeEntity> Update(ShowtimeEntity showtimeEntity)
        {
            var entity = await _context.Showtimes.FindAsync(showtimeEntity.Id);
            if (entity == null)
            {
                return null;
            }
            _context.Entry(entity).CurrentValues.SetValues(showtimeEntity);
            await _context.SaveChangesAsync();
            return showtimeEntity;

        }
    }
}
