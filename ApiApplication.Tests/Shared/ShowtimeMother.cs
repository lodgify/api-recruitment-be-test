using System;
using ApiApplication.Resources;

namespace ApiApplication.Tests.Shared
{
    public sealed class ShowtimeMother
    {
        public static ShowTime Create(int Id, Movie movie)
        {
            return new ShowTime()
            {
                Id=Id,
                AuditoriumId = 3,
                StartDate = DateTime.Now.ToString(),
                EndDate = DateTime.Now.AddDays(7).ToString(),
                Schedule = "12:00, 15:00",
                Movie = movie
            };
        }
    }
}

