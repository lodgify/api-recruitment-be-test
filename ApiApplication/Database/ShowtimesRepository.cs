using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
            _context.Showtimes.Add(showtimeEntity);

            int id = _context.SaveChanges();

            return _context.Showtimes.Find(id);
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

        //public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, IQueryable<ShowtimeEntity>> filter)
        //{
        //    var query = filter != null ? filter(_context.Showtimes) : _context.Showtimes;

        //    return query.AsEnumerable();

        //}

        public IEnumerable<ShowtimeEntity> GetCollection(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            var r = filter != null ? _context.
                Showtimes
                .Include(x => x.Movie)
                .Where(filter)
                .AsEnumerable()
                : _context.Showtimes;

            return r;
        }



        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            var showTimeDB = _context.Showtimes.Find(showtimeEntity.Id);

            if (showTimeDB == null)
                throw new Exception($"ShowTime entity with id = {showtimeEntity.Id} not found");

            showTimeDB.AuditoriumId = showtimeEntity.AuditoriumId;
            showTimeDB.Schedule = showtimeEntity.Schedule;
            showTimeDB.StartDate = showtimeEntity.StartDate;
            showTimeDB.EndDate = showtimeEntity.EndDate;

            if (showtimeEntity.Movie != null)
            {
                showTimeDB.Movie.Stars = showtimeEntity.Movie.Stars;
                showTimeDB.Movie.ShowtimeId = showtimeEntity.Id;
                showTimeDB.Movie.ImdbId = showtimeEntity.Movie.ImdbId;
                showTimeDB.Movie.ReleaseDate = showtimeEntity.Movie.ReleaseDate;
                showTimeDB.Movie.Title = showtimeEntity.Movie.Title;
            }

            _context.SaveChanges();

            return showTimeDB;
        }


    }
}
