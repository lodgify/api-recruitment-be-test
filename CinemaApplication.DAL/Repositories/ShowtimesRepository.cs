using CinemaApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CinemaApplication.DAL.Repositories
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
            var newShowtimeEntity = await _context.Showtimes.AddAsync(showtimeEntity);
            newShowtimeEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();

            return showtimeEntity;
        }

        public async Task DeleteAsync(ShowtimeEntity entity)
        {
            var removedEntity = _context.Showtimes.Remove(entity);
            removedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetAllAsync()
        {
            return await GetAllAsync(null);
        }

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
    }
}
