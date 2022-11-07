using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ApiApplication.Database.Entities;

namespace ApiApplication.Database {
    public interface IShowtimesRepository {
        IEnumerable<ShowtimeEntity> GetCollection();
        //IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter);
        IEnumerable<ShowtimeEntity> GetCollection(Func<ShowtimeEntity, bool> filter);
        ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter);
        ShowtimeEntity GetByMovie(Func<MovieEntity, bool> filter);
        Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Update(ShowtimeEntity showtimeEntity);
        Task<ShowtimeEntity> DeleteAsync(int id);
        bool AuditoriumExists(int id);
    }
}
