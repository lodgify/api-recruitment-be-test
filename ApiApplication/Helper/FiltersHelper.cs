using ApiApplication.Database.Entities;
using System.Linq;
using System;

namespace ApiApplication.Helpers
{
    public static class FilterHelper
    {
        public static Func<IQueryable<ShowtimeEntity>, bool> ShowtimeFilter(int showtimeId)
        {
            return query => query.Any(e => e.Id == showtimeId);
        }
        public static Func<IQueryable<ShowtimeEntity>, bool> ShowtimeFilter(DateTime? date, string movieTitle)
        {
            if (date.HasValue && movieTitle is null)            
                return query => query.Any(e => e.StartDate <= date && e.EndDate >= date);
            
            else if (movieTitle is not null && date is null)            
                return query => query.Any(e => e.Movie.Title.Contains(movieTitle));
            
            else if (date.HasValue && movieTitle is not null)            
                return query => query.Any(e => e.StartDate <= date && e.EndDate >= date && e.Movie.Title.Contains(movieTitle));
            
            return query => false;
        }

        public static Func<IQueryable<MovieEntity>, bool> MovieImdbIdFilter(string imdbId)
        {
            return query => query.Any(e => e.ImdbId == imdbId);
        }
    }
}
