using CinemaApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CinemaApplication.DAL.Repositories
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly CinemaContext _context;
        public ShowtimeRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity)
        {
            var newShowtimeEntity = await _context.Showtimes.AddAsync(showtimeEntity);
            newShowtimeEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();

            return showtimeEntity;
        }

        public async Task DeleteAsync(int showtimeId)
        {
            var removedEntity = _context.Showtimes.Remove(new ShowtimeEntity { Id = showtimeId });
            removedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetAllAsync()
            => await _context
                .Showtimes
                .Include(s => s.Movie)
                .ToListAsync();

        public async Task<IEnumerable<ShowtimeEntity>> GetAllAsync(Expression<Func<ShowtimeEntity, bool>> expression)
        {
            return await _context
                .Showtimes
                .Where(expression)
                .Select(s => new ShowtimeEntity
                {
                    Id = s.Id,
                    AuditoriumId = s.AuditoriumId,
                    EndDate = s.EndDate,
                    StartDate = s.StartDate,
                    Schedule = s.Schedule,
                    Movie = _context.Movies.Where(m => m.ShowtimeId == s.Id).SingleOrDefault()
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(ShowtimeEntity showtimeEntity)
        {
            var updatedEntity = _context.Update(showtimeEntity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public IQueryable<ShowtimeEntity> GetQueryable()
            => _context.Set<ShowtimeEntity>()
                .Include(s => s.Movie)
                .AsNoTracking();
    }
}
