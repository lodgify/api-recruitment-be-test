using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var result = _context.Showtimes.Add(showtimeEntity);

            _context.SaveChanges();

            return result.Entity;
        }

        public ShowtimeEntity Delete(int id)
        {
            var entity = _context.Showtimes.Where(x => x.Id == id).FirstOrDefault();

            _context.Showtimes.Remove(entity);

            _context.SaveChanges();

            return entity;
        }

        public ShowtimeEntity GetById(int id)
        {
            return _context.Showtimes
                           .Include(x => x.Movie)
                           .Where(x => x.Id == id)
                           .FirstOrDefault();
        }

        public ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ShowtimeEntity> GetCollection()
        {
            return GetCollection(null);
        }

        public IEnumerable<ShowtimeEntity> GetCollection(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            var result = _context.Showtimes
                                .Include(x => x.Movie)
                                .AsQueryable();
            if (filter != null)
                result = result.Where(filter);


            return result;
        }


        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            var result = _context.Showtimes.Update(showtimeEntity);

            _context.SaveChanges();

            return result.Entity;
        }
    }
}
