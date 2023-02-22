using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IShowtimesRepository
    {
        Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync();
        Task<IEnumerable<ShowtimeEntity>> GetCollectionAsync(Expression<Func<ShowtimeEntity, bool>> filter = null);
        Task<ShowtimeEntity> GetByIdAsync(int id);
        Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity);
        Task<ShowtimeEntity> UpdateAsync(ShowtimeEntity showtimeEntity);
        Task DeleteAsync(int id);
    }
}
