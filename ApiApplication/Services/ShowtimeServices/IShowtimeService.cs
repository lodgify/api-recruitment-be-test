using ApiApplication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services.ShowtimeServices
{
    public interface IShowtimeService
    {
        Task<ShowtimeDto> Add(ShowtimeDto showtime);
        Task<IEnumerable<ShowtimeDto>> GetCollection();
    }
}
