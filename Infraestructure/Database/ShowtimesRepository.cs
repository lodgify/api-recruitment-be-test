using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infraestructure.Database
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;
        private DbSet<ShowtimeEntity> _dbSet;

        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
            _dbSet = _context.Set<ShowtimeEntity>();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync()
        {
            IQueryable<ShowtimeEntity> query = _dbSet;
            return await query.Include(showtime => showtime.Movie).ToListAsync();
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            IQueryable<ShowtimeEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Include(showtime => showtime.Movie).ToListAsync();
        }

        public async Task<ShowtimeEntity> GetByIdAsync(int id)
        {
            IQueryable<ShowtimeEntity> query = _dbSet;
            query = query.Where(showtime => showtime.Id == id);

            return await query.Include(showtime => showtime.Movie).FirstOrDefaultAsync();
        }

        public async Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity)
        {
            var result = _dbSet.Add(showtimeEntity).Entity;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<ShowtimeEntity> UpdateAsync(ShowtimeEntity showtimeEntity)
        {
            var result = _dbSet.Update(showtimeEntity).Entity;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = _dbSet.Find(id);
            _dbSet.Remove(entityToRemove);

            await _context.SaveChangesAsync();
        }
    }
}
