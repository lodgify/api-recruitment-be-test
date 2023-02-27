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
            var response =  _context.Showtimes.AsEnumerable();

            if (_paginatedRequest != null && _paginatedRequest.Since > 0)
                response = response.Skip((int)_paginatedRequest.Since).Take(_paginatedRequest.PageSize);

            return response;
        }

        public IEnumerable<ShowtimeEntity> GetCollection(Func<ShowtimeEntity, bool> filter)
        {
            var response =  _context.Showtimes.Where(showTime => filter(showTime));
            if (_paginatedRequest != null && _paginatedRequest.Since > 0)
                response = response.Skip((int)_paginatedRequest.Since).Take(_paginatedRequest.PageSize);

            return response;
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {
            _context.Showtimes.Update(showtimeEntity);
            return showtimeEntity;
        }
    }
}
