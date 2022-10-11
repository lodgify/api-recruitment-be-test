using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Database
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;
        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public ShowtimeEntity Add(ShowtimeEntity showtimeEntity)
        {
            var newEntry = _context.Showtimes.Add(showtimeEntity);
            var id = _context.SaveChanges();
            return GetCollection(s => s.Id == newEntry.Entity.Id).FirstOrDefault();
        }

        public ShowtimeEntity Delete(int id)
        {
            var entityToDelete = _context.Showtimes
                .Include(s => s.Movie)
                .FirstOrDefault(s => s.Id == id);
            if (entityToDelete != null)
            {
                _context.Showtimes.Remove(entityToDelete);
                _context.SaveChanges();
            }
            return entityToDelete;
        }

        public ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ShowtimeEntity> GetCollection()
        {
            return GetCollection(null);
        }

        public IEnumerable<ShowtimeEntity> GetCollection(Func<ShowtimeEntity, bool> filter)
        {
            return _context.Showtimes
                .Include(s => s.Movie)
                .Where(filter)
                .ToList();
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            var trackedEntity = _context.Showtimes
                .Include(s => s.Movie).FirstOrDefault(s => s.Id == showtimeEntity.Id);
            if (trackedEntity != null)
            {
                trackedEntity.Schedule = showtimeEntity.Schedule;
                trackedEntity.StartDate = showtimeEntity.StartDate;
                trackedEntity.EndDate = showtimeEntity.EndDate;
                trackedEntity.AuditoriumId = showtimeEntity.AuditoriumId;
                if (showtimeEntity.Movie != null)
                {
                    trackedEntity.Movie = showtimeEntity.Movie;
                }
            }
            _context.SaveChanges();
            return trackedEntity;
        }

    }
}
