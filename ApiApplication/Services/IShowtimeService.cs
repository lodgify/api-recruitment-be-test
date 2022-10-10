using ApiApplication.Models;
using System.Collections.Generic;

namespace ApiApplication.Services
{
    public interface IShowtimeService
    {
        List<Showtime> GetAll();
    }
}
