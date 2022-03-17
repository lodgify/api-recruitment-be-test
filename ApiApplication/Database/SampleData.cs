using ApiApplication.Database.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ApiApplication.Database
{
    public class SampleData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<CinemaContext>();
            context.Database.EnsureCreated();          
            

            context.Auditoriums.Add(new AuditoriumEntity
            {
                Id = 1,
                Showtimes = new List<ShowtimeEntity> 
                { 
                    new ShowtimeEntity
                    {
                        Id = 1,
                        StartDate = new DateTime(2022, 1, 1),
                        EndDate = new DateTime(2022, 4, 1),
                        Movie = new MovieEntity
                        {
                            Id = 1,
                            Title = "Inception",
                            ImdbId = "tt1375666",
                            ReleaseDate = new DateTime(2010, 01, 14),
                            Stars = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"                            
                        },
                        Schedule = new List<string> { "16:00", "17:00", "18:00", "18:30", "19:00", "22:00" },
                        AuditoriumId = 1
                    } 
                },
                Seats = 56
            });

            context.Auditoriums.Add(new AuditoriumEntity
            {
                Id = 2,
                Seats = 78
            });

            context.Auditoriums.Add(new AuditoriumEntity
            {
                Id = 3,
                Seats = 48
            });

            context.SaveChanges();
        }
    }
}
