using System;
using System.Linq;
using System.Threading.Tasks;
using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _context;
        public MovieRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<MovieEntity> GetById(int id)
        {
            var q = await (from m in _context.Movies
                           where m.Id == id
                           select m).FirstOrDefaultAsync();
            return q;
        }

        public async Task<MovieEntity> GetByImdbId(string imdbId)
        {
            var q = await (from m in _context.Movies
                           where m.ImdbId == imdbId
                           select m).FirstOrDefaultAsync();
            return q;
        }
    }
}

