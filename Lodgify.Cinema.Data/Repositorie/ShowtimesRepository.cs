using ApiApplication.Database.Entities;
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
            throw new System.NotImplementedException();
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

        public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}
