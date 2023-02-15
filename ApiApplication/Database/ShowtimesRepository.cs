using ApiApplication.Database.Entities;
using ApiApplication.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Database
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;
        private const string type = "showtime";

        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public ShowtimeEntity Add(ShowtimeEntity showtimeEntity)
        {
            _context.Add(showtimeEntity);
            _context.SaveChanges();
            return showtimeEntity;
        }

        public ShowtimeEntity Delete(int id)
        {
            var showtime = _context.Set<ShowtimeEntity>().Include(s => s.Movie).FirstOrDefault(s => s.Id == id);
            if (showtime == null)
                throw new NotFoundException(type, id);

            _context.Set<ShowtimeEntity>().Remove(showtime);
            _context.SaveChanges();
            return showtime;
        }

        public ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            return GetCollection().Where(s => {
                IEnumerable<MovieEntity> movieAsQueryable = new List<MovieEntity> { s.Movie };
                return filter(movieAsQueryable.AsQueryable<MovieEntity>());
            }).FirstOrDefault();   
        }

        public IEnumerable<ShowtimeEntity> GetCollection()
        {
            return _context.Set<ShowtimeEntity>().Include(s => s.Movie).ToList();
        }

        public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter)
        {
            var showtimes = _context.Set<ShowtimeEntity>().AsQueryable<ShowtimeEntity>();
            return filter(showtimes) ? GetCollection() : null;
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            _context.Entry(showtimeEntity).State = EntityState.Modified;
            if (showtimeEntity.Movie != null)
                _context.Entry(showtimeEntity.Movie).State = EntityState.Modified;

            _context.SaveChanges();
            return _context.Set<ShowtimeEntity>().Include(s => s.Movie).FirstOrDefault(s => s.Id == showtimeEntity.Id);
        }
    }
}
