using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Entitie;
using Lodgify.Cinema.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lodgify.Cinema.Infrastructure.Data.Repositorie
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
            return showtimeEntity;
        }

        public ShowtimeEntity Delete(int id)
        {
            var entitie = _context.Showtimes.Single(s => s.Id == id);
            _context.Showtimes.Remove(entitie);
            return entitie;
        }

        public ShowtimeEntity GetByMovie(Func<MovieEntity, bool> filter)
        {
            var movie = _context.Movies.FirstOrDefault(movie => filter(movie));
            return movie == null 
                          ? null 
                          :_context.Showtimes.FirstOrDefault(showTime => showTime.Movie.Id == movie.Id);
        }

        public IEnumerable<ShowtimeEntity> GetCollection()
        {
            return _context.Showtimes;
        }

        public IEnumerable<ShowtimeEntity> GetCollection(Func<ShowtimeEntity, bool> filter)
        {
            return _context.Showtimes.Where(showTime => filter(showTime));
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            _context.Showtimes.Attach(showtimeEntity);
            _context.Entry(showtimeEntity).State = EntityState.Modified;
            return showtimeEntity;
        }
    }
}
