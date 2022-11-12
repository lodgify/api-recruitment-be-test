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
            ShowtimeEntity dbShowTime = _context.Showtimes.Find(id);

            if (dbShowTime == null)
                return null;

            _context.Showtimes.Remove(dbShowTime);
            _context.SaveChanges();

            return dbShowTime;
        }

        public ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ShowtimeEntity> GetCollection()
        {
            return GetCollection(null);
        }

        // please review this as well . I didnt found filter of type Func<IQueryable<ShowTimeEntity>,bool> and there is no dbSet.where which takes in such lambda/expression.
        //public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, IQueryable<ShowtimeEntity>> filter)
        //{
        //    var query = filter != null ? filter(_context.Showtimes) : _context.Showtimes;

        //    return query.AsEnumerable();

        //}

        public IEnumerable<ShowtimeEntity> GetCollection(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            var r = filter != null ? _context
                .Showtimes
                .Include(x => x.Movie)
                .Where(filter)
                .AsEnumerable()
                : _context.Showtimes.Include(x=>x.Movie);

            return r;
        }



        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            var dbShowTime = _context.Showtimes
                                     .Where(showTime => showTime.Id == showtimeEntity.Id)
                                     .Include(x => x.Movie)
                                     .Single();

            if (dbShowTime == null)
                return null;

            dbShowTime.AuditoriumId = showtimeEntity.AuditoriumId;
            dbShowTime.Schedule = showtimeEntity.Schedule;
            dbShowTime.StartDate = showtimeEntity.StartDate;
            dbShowTime.EndDate = showtimeEntity.EndDate;

            if (showtimeEntity.Movie != null)
            {
                dbShowTime.Movie.Stars = showtimeEntity.Movie.Stars;
                dbShowTime.Movie.ShowtimeId = showtimeEntity.Id;
                dbShowTime.Movie.ImdbId = showtimeEntity.Movie.ImdbId;
                dbShowTime.Movie.ReleaseDate = showtimeEntity.Movie.ReleaseDate;
                dbShowTime.Movie.Title = showtimeEntity.Movie.Title;
            }

            _context.SaveChanges();

            return dbShowTime;
        }


    }
}
