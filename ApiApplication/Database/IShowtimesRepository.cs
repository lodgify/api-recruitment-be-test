using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApiApplication.Models.Showtime;

namespace ApiApplication.Database
{
    public interface IShowtimesRepository
    {
        Task<IEnumerable<ShowtimeEntity>> GetAsync();
        Task<IEnumerable<ShowtimeEntity>> GetAsync(Expression<Func<ShowtimeEntity, bool>> filter);
        Task<IEnumerable<ShowtimeEntity>> GetByShowtimeRequestAsync(GetAllShowtimesRequest request);
        ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter);
        ShowtimeEntity Add(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Update(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Delete(int id);
    }
}
