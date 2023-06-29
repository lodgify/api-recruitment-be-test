using ApiApplication.Database.Entities;
using ApiApplication.Models;
using ApiApplication.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiApplication.Database
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;
        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public  ShowtimeEntity Add(ShowtimeEntity showtimeEntity)
        {
                _context.Showtimes.Add(showtimeEntity);
                _context.SaveChanges();
                return showtimeEntity;
        }

        public ShowtimeEntity Delete(int id)
        {
            var showtimeEntity = _context.Showtimes.Find(id);
            
            if (showtimeEntity == null)            
                return null;            

            _context.Showtimes.Remove(showtimeEntity);
            _context.SaveChanges();

            return showtimeEntity;
        }

        public ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            var queryableMoviesEntities = _context.Movies.AsQueryable();
            var filteredMovieEntity = queryableMoviesEntities.Where(e => filter(queryableMoviesEntities));

            var showtimeEntity = _context.Showtimes.Include(s => s.Movie)
                .FirstOrDefault(s => s.Movie == filteredMovieEntity.FirstOrDefault());

            return showtimeEntity;
        }

        public IEnumerable<ShowtimeEntity> GetCollection()
        {
            return _context.Showtimes.Include(s => s.Movie).ToList();
        }

        public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter)
        {
            var queryableShowtimeEntities = GetCollection().AsQueryable();
            var filteredShowtimeEntity = queryableShowtimeEntities.Where(e => filter(queryableShowtimeEntities));
            return filteredShowtimeEntity;
        }

        public  ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {   
            var updatedShowtimeEntity = _context.Showtimes.Update(showtimeEntity);
            _context.SaveChanges();
            return updatedShowtimeEntity.Entity;
        }
    }
}
