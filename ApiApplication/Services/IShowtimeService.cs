﻿using ApiApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IShowtimeService
    {
        Task<Showtime> Create(Showtime showtime);
        List<Showtime> GetAll(string movieName, DateTime? date);
        Task<Showtime> Update(Showtime showtime);
    }
}
