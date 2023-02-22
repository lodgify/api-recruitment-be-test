using CinemaApplication.Core.Models.ServiceResults;
using CinemaApplication.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Abstractions
{
    public interface IShowtimeService
    {
        Task<ServiceDataResult<IEnumerable<ShowtimeDto>>> GetAllAsync(ShowtimeQuery query);

        Task<ServiceDataResult<int>> AddAsync(NewShowtimeDto showtime);

        Task<ServiceResult> UpdateAsync(ShowtimeDto showtime);

        Task<ServiceResult> DeleteAsync(int showtimeId);
    }
}
