﻿using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiApplication.Database
{
    public interface IShowtimesRepository
    {
        IEnumerable<ShowtimeEntity> GetCollection();
        IEnumerable<ShowtimeEntity> GetCollection(Expression<Func<ShowtimeEntity, bool>> filter);

        // IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, IQueryable<ShowtimeEntity>> filter);
        ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter);
        ShowtimeEntity Add(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Update(ShowtimeEntity showtimeEntity);
        ShowtimeEntity Delete(int id);

    }
}
