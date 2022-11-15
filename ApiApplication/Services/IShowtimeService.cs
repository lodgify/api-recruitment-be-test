using ApiApplication.Database.Entities;
using ApiApplication.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IShowtimeService
    {
        Task<ShowtimeEntity> Add(ShowtimeCommand showtime);
        Task<ShowtimeEntity> Update(ShowtimeCommand showtime);
        IEnumerable<ScheduleDTO> GetShowTimeSchedule(string title = null, DateTime? date = null);
    }
}
