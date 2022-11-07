using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ApiApplication.Database.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiApplication.Database {
    public class ShowtimesRepository : IShowtimesRepository {
        private readonly CinemaContext _context;
        public ShowtimesRepository(CinemaContext context) {
            _context = context;
        }

        public async Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity) {
            EntityEntry<ShowtimeEntity> entity = await _context.AddAsync(showtimeEntity);
            if (entity.State != EntityState.Added)
                throw new InvalidOperationException("Adding new showtime entity failed");
            return entity.Entity;
        }

        public bool AuditoriumExists(int id) =>
            _context.Auditoriums.Any(a => a.Id == id);

        public async Task<ShowtimeEntity> DeleteAsync(int id) {
            ShowtimeEntity entity = await _context.Showtimes.FirstOrDefaultAsync(s => s.Id == id);
            if (entity != null)
                _context.Remove(entity);
            return entity;
        }

        public ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter) {
            throw new NotImplementedException();
        }

        public ShowtimeEntity GetByMovie(Func<MovieEntity, bool> filter) {
            return _context.Showtimes.Include(s => s.Movie)
                                     .FirstOrDefault(s => filter(s.Movie));
        }

        public IEnumerable<ShowtimeEntity> GetCollection() {
            return GetCollection(null);
        }

        //public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter) {
        //    throw new NotImplementedException();
        //}

        public IEnumerable<ShowtimeEntity> GetCollection(Func<ShowtimeEntity, bool> filter) {
            return _context.Showtimes.Include(s => s.Movie)
                                     .Where(filter ?? (_ => true))
                                     .ToList();
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity) {
            EntityEntry<ShowtimeEntity> entity = _context.Update(showtimeEntity);
            if (entity.State != EntityState.Modified)
                throw new InvalidOperationException("Update showtime entity failed");
            return entity.Entity;
        }
    }
}
