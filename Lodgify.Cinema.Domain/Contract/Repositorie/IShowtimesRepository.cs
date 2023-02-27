using Lodgify.Cinema.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lodgify.Cinema.Domain.Contract.Repositorie

{
    public interface IShowtimesRepository
    {
        IEnumerable<ShowtimeEntity> GetCollection();
        IEnumerable<ShowtimeEntity> GetCollection(Func<ShowtimeEntity, bool> filter);
        ShowtimeEntity GetByMovie(Func<MovieEntity, bool> filter);
        ShowtimeEntity Add(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Update(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Delete(int id);
    }
}
