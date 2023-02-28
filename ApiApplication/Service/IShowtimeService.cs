using ApiApplication.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ApiApplication.Models.ViewModels;

namespace ApiApplication.Service
{
    public interface IShowtimeService
    {
        Task<ShowtimeEntity> Add(ShowtimeViewModel showtime);
        Task<ShowtimeEntity> Update(ShowtimeViewModel showtime);
        IEnumerable<ScheduleViewModel> GetShowTimeSchedule(string title = null, DateTime? date = null);
        ShowtimeEntity DeleteTimeSchedule(int id);
    }
}
