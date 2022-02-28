using ApiApplication.Dtos;
using System.Collections.Generic;

namespace ApiApplication.Services.ShowtimeServices
{
    public interface IShowtimeService
    {
        IEnumerable<ShowtimeDto> GetCollection();
    }
}
