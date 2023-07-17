using System;
using ApiApplication.Database.Entities;
using ApiApplication.DTO;

namespace TestData
{
    public class TestData
    {

        public static MovieEntity CreateMovie(int id = 1)
        {
            return new MovieEntity
            {
                Id = id,
                Title = "Inception",
                ImdbId = "tt1375666",
                ReleaseDate = new DateTime(2010, 01, 14),
                Stars = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"
            };

        }

        public static ShowtimeEntity CreateShowtimeWithMovie(int id = 1, int movieId=1)
        {

            return new ShowtimeEntity
            {
                Id = id,
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 4, 1),
                Movie = new MovieEntity
                {
                    Id = movieId,
                    Title = "Inception",
                    ImdbId = "tt1375666",
                    ReleaseDate = new DateTime(2010, 01, 14),
                    Stars = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"
                },
                Schedule = new List<string> { "16:00", "17:00", "18:00", "18:30", "19:00", "22:00" },
                AuditoriumId = 1
            };
        }

        public static AuditoriumEntity CreateAuditoriumEntityWithShowtimeWithMovie(int id = 1, int showTimeId =1)
        {
            return new AuditoriumEntity
            {
                Id = id,
                Showtimes = new List<ShowtimeEntity>
                {
                    new ShowtimeEntity
                    {
                        Id = showTimeId,
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
            };

        }

        public static ShowtimeCommand CreateCommandShowTime(int showtimeId = 1)
        {
            return new ShowtimeCommand
            {
                Id = showtimeId,
                Start_date = new DateTime(2022, 1, 1),
                End_date = new DateTime(2022, 4, 1),
                Movie = new MovieCommand
                {

                    Title = "Inception",
                    Imdb_id = "tt1375666",
                    Release_date = new DateTime(2010, 01, 14),
                    Starts = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"
                },

                Auditorium_id = 1
            };

        }
    }
}