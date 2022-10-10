using ApiApplication.Database.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

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
            var id =_context.SaveChanges();
            return GetCollection(s => s.Id == newEntry.Entity.Id).FirstOrDefault();
        }

        public ShowtimeEntity Delete(int id)
        {
            throw new System.NotImplementedException();
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
            var trackedEntity = _context.Showtimes.Find(showtimeEntity.Id);
            trackedEntity.Schedule = showtimeEntity.Schedule;
            trackedEntity.StartDate = showtimeEntity.StartDate;
            trackedEntity.EndDate = showtimeEntity.EndDate;
            trackedEntity.AuditoriumId = showtimeEntity.AuditoriumId;
            if (showtimeEntity.Movie != null)
                trackedEntity.Movie = showtimeEntity.Movie;
            var id = _context.SaveChanges();
            return trackedEntity;
        }

    }
}
