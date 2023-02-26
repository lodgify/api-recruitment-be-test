using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
            _context.Add(showtimeEntity);
            _context.SaveChanges();
            return _context.Showtimes.Find(showtimeEntity.Id);
        }

        public void Delete(int id)
        {
            var showTime = _context.Showtimes.Find(id);

            if (showTime == null)
            {
                throw new Exception("Showtime Id doesn't exist.");
            }

            _context.Remove(showTime);
            _context.SaveChanges();
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
            IEnumerable<ShowtimeEntity> query = _context.Showtimes
                                                .Include( s => s.Movie);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            if (_context.Showtimes.Find(showtimeEntity.Id) == null)
            {
                return null;
            }

            _context.Update(showtimeEntity);
            _context.SaveChanges();
            return _context.Showtimes.Find(showtimeEntity.Id);
        }
    }
}
