﻿using Lodgify.Cinema.Domain.Entitie;
using Lodgify.Cinema.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lodgify.Cinema.Infrastructure.Data.Database
{
    public class SampleData
    {
        /// <summary>
        /// It's not that simple to disable parallel test execution in SpecFlow, I did it just at the discretion of tests, if it was parallel multithreaded access I would use aa ConcurrentLibraries
        /// </summary>
        private static Object lockingObj = new object();

        public static void Initialize(CinemaContext context)
        {
            lock (lockingObj)
            {
                if (context.Auditoriums.Any())
                    return;

                context.Database.EnsureDeleted();
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
}