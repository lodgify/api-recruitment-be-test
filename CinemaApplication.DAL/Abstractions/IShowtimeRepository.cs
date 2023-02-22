﻿using CinemaApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CinemaApplication.DAL.Repositories
{
    public interface IShowtimeRepository
    {
        Task<ShowtimeEntity> GetAsync(int id);
        Task<IEnumerable<ShowtimeEntity>> GetAllAsync();
        Task<IEnumerable<ShowtimeEntity>> GetAllAsync(Expression<Func<ShowtimeEntity, bool>> expression);
        Task<ShowtimeEntity> AddAsync(ShowtimeEntity showtimeEntity);
        Task UpdateAsync(ShowtimeEntity showtimeEntity);
        Task DeleteAsync(int showtimeId);
        IQueryable<ShowtimeEntity> GetQueryable();
    }
}
