using ApiApplication.Database.Entities;
using ApiApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services.Implementation
{
    public interface IServiceShowtime
    {
        IEnumerable<ShowtimeEntity> GetAll(DateTime? date, string movieTitle);
        ShowtimeEntity GetExisting(int id);
        Task<ShowtimeEntity> Update(ShowtimeEntity existingShowtime, Showtime showtime);
        Task<ShowtimeEntity> Add(Showtime showtime);
        ShowtimeEntity Remove(int showtimeId);
    }
}