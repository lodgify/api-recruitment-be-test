using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApiApplication.Models.Showtime;
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

        public async Task<IEnumerable<ShowtimeEntity>> GetAsync()
        {
            return await GetAsync(_ => true);
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetAsync(Expression<Func<ShowtimeEntity, bool>> filter)
        {
            return await _context.Showtimes
                .Where(filter)
                .Include(entity => entity.Movie)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<ShowtimeEntity>> GetByShowtimeRequestAsync(GetAllShowtimesRequest request)
        {
            var titleFilter = request.Title != null ? new Func<ShowtimeEntity, bool>(entity =>
                entity.Movie.Title.ToLower().Contains(request.Title.ToLower())) : _ => true;

            var dateFilter = request.DateTime != null ? new Func<ShowtimeEntity, bool>(entity =>
                entity.StartDate <= request.DateTime && entity.EndDate >= request.DateTime) : _ => true;
            
            return await GetAsync();
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}
