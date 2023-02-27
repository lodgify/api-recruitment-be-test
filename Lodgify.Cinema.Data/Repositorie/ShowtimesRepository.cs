using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Entitie;
using Lodgify.Cinema.Domain.Pagination;
using Lodgify.Cinema.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lodgify.Cinema.Infrastructure.Data.Repositorie
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;
        private readonly IPaginatedRequest _paginatedRequest;
        public ShowtimesRepository(CinemaContext context, IPaginatedRequest paginatedRequest)
        {
            _context = context;
            _paginatedRequest = paginatedRequest;
        }

        public async Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity, CancellationToken cancellationToken)
        {
            await _context.Showtimes.AddAsync(showtimeEntity, cancellationToken);
            return showtimeEntity;
        }

        public ShowtimeEntity Delete(int id)
        {
            var entitie = _context.Showtimes.Single(s => s.Id == id);
            _context.Showtimes.Remove(entitie);
            return entitie;
        }

        public async Task<ShowtimeEntity> GetByMovieAsync(Func<MovieEntity, bool> filter, CancellationToken cancellationToken)
        {
            var movie = _context.Movies.FirstOrDefault(movie => filter(movie));
            return movie == null
                          ? null
                          : await _context.Showtimes.FirstOrDefaultAsync(showTime => showTime.Movie.Id == movie.Id, cancellationToken);
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
            _context.Showtimes.Update(showtimeEntity);
            return showtimeEntity;
        }
    }
}
