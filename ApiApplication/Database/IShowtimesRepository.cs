using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiApplication.Database
{
    public interface IShowtimesRepository
    {
        Task<ShowtimeEntity> GetByIdAsync(int id);

        Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync();

        Task<IEnumerable<ShowtimeEntity>> GetAsync(Expression<Func<ShowtimeEntity, bool>> filter);

        Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync(Func<IQueryable<ShowtimeEntity>, bool> filter);

        Task<ShowtimeEntity> GetByMovieAsync(Expression<Func<MovieEntity, bool>> filter);

        Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity);

        Task<ShowtimeEntity> UpdateAsync(ShowtimeEntity showtimeEntity);

        Task<ShowtimeEntity> DeleteAsync(int id);

        Task<ShowtimeEntity> DeleteAsync(ShowtimeEntity showtimeEntity);
    }
}
