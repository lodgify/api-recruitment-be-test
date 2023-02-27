using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Database
{
    public interface IShowtimesRepository
    {
        IEnumerable<ShowtimeEntity> GetCollection();
        IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter);
        ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter);
        ShowtimeEntity Add(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Update(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Delete(int id);
    }
}
