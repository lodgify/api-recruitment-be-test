using CinemaApplication.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Abstractions
{
    public interface IShowtimeService
    {
        Task<IEnumerable<ShowtimeDto>> GetAllAsync();

        Task<int> CreateAsync(NewShowtimeDto showtime);

        Task UpdateAsync(ShowtimeDto showtime);

        Task DeleteAsync(int showtimeId);
    }
}
