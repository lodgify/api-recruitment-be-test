using Lodgify.Cinema.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lodgify.Cinema.Domain.Contract.Repositorie

{
    public interface IShowtimesRepository
    {
        IEnumerable<ShowtimeEntity> GetCollection();
        IEnumerable<ShowtimeEntity> GetCollection(Func<ShowtimeEntity, bool> filter);
        Task<ShowtimeEntity> GetByMovieAsync(Func<MovieEntity, bool> filter, CancellationToken cancellationToken);
        Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity, CancellationToken cancellationToken);
        ShowtimeEntity Update(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Delete(int id);
    }
}
