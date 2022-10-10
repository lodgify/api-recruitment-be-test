using ApiApplication.Models;
using System;
using System.Collections.Generic;

namespace ApiApplication.Services
{
    public interface IShowtimeService
    {
        List<Showtime> GetAll(string movieName, DateTime? date);
    }
}
