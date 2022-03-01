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
        Task<IEnumerable<ShowtimeEntity>> GetCollection();
        Task<IEnumerable<ShowtimeEntity>> GetCollection(Expression<Func<ShowtimeEntity, bool>> filter);
        Task<ShowtimeEntity> GetByMovie(Func<IQueryable<MovieEntity>, bool> filter);
        Task<ShowtimeEntity> Add(ShowtimeEntity showtimeEntity);
        Task<ShowtimeEntity> Update(ShowtimeEntity showtimeEntity);
        Task<ShowtimeEntity> Delete(int id);
        Task<IEnumerable<ShowtimeEntity>> GetByAuditoriumId(int auditorumId);
        void ThrowErrorMethod();
    }
}
