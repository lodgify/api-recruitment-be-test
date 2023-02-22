using ApiApplication.DTOs.API;
using ApiApplication.DTOs.IMDB;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplicationUnitTests
{
    public class TestDataProvider
    {
        private static TestDataProvider _instance;
        private IMapper _mapper;
        private List<IMDBMovieInfo> _moviesInfo;
        private List<MovieEntity> _moviesEntities;
        private List<ShowtimeEntity> _showtimeEntities;

        private IMDBMovieInfo _newMovieInfo;
        private Movie _newMovie;
        private MovieEntity _newMovieEntity;
        private Showtime _newShowtime;
        private ShowtimeEntity _newShowtimeEntity;

        private TestDataProvider()
        {
            _mapper = new Mapper(
                new MapperConfiguration(config => config.CreateMap<IMDBMovieInfo, MovieEntity>()));

            #region InitializeData

            _moviesInfo = new List<IMDBMovieInfo>()
            {
                new IMDBMovieInfo()
                {
                    ImdbId = "imbd1",
                    Title = "The Best Movie Ever",
                    ReleaseDate = DateTime.Parse("2022-04-25"),
                    Stars = "John Smith, Carlos Pérez",
                },
                new IMDBMovieInfo()
                {
                    ImdbId = "imbd2",
                    Title = "The Best Movie Ever 2",
                    ReleaseDate = DateTime.Parse("2022-05-25"),
                    Stars = "David John, Austin Johnson, Jimmy Prasley"
                },
                new IMDBMovieInfo()
                {
                    ImdbId = "imbd3",
                    Title = "The Best Movie Ever 3",
                    ReleaseDate = DateTime.Parse("2022-05-01"),
                    Stars = "José Hernández, Arthur Spencer"
                }
            };

            _moviesEntities = new List<MovieEntity>()
            {
                _mapper.Map<MovieEntity>(_moviesInfo[0]),
                _mapper.Map<MovieEntity>(_moviesInfo[1]),
                _mapper.Map<MovieEntity>(_moviesInfo[2]),
            };

            _showtimeEntities = new List<ShowtimeEntity>()
            {
                new ShowtimeEntity()
                {
                    Id = 1,
                    Movie = _moviesEntities[0],
                    StartDate = DateTime.Parse("2022-05-01"),
                    EndDate = DateTime.Parse("2022-05-05"),
                    Schedule = new List<string>(){"17:00", "18:00", "19:00"},
                    AuditoriumId = 1
                },
                new ShowtimeEntity()
                {
                    Id = 2,
                    Movie = _moviesEntities[1],
                    StartDate = DateTime.Parse("2022-06-01"),
                    EndDate = DateTime.Parse("2022-08-15"),
                    Schedule = new List<string>(){"15:00", "16:00"},
                    AuditoriumId = 1
                },
                new ShowtimeEntity()
                {
                    Id = 3,
                    Movie = _moviesEntities[2],
                    StartDate = DateTime.Parse("2022-05-03"),
                    EndDate = DateTime.Parse("2022-05-08"),
                    Schedule = new List<string>(){"09:30", "12:30", "15:30", "17:30"},
                    AuditoriumId = 2
                },
                new ShowtimeEntity()
                {
                    Id = 4,
                    Movie = _moviesEntities[2],
                    StartDate = DateTime.Parse("2023-01-01"),
                    EndDate = DateTime.Parse("2023-12-31"),
                    Schedule = new List<string>(){"17:00"},
                    AuditoriumId = 3
                }
            };

            _newMovieInfo = new IMDBMovieInfo()
            {
                ImdbId = "imdbN",
                Title = "New Movie",
                ReleaseDate = DateTime.Parse("2023-12-31"),
                Stars = "New Actor, New Actress"
            };

            _newMovie = new Movie()
            {
                ImdbId = _newMovieInfo.ImdbId,
                Title = _newMovieInfo.Title,
                ReleaseDate = _newMovieInfo.ReleaseDate,
                Stars = _newMovieInfo.Stars
            };

            _newMovieEntity = new MovieEntity()
            {
                Id = _moviesEntities.Count,
                ImdbId = _newMovie.ImdbId,
                Title = _newMovie.Title,
                ReleaseDate = _newMovie.ReleaseDate,
                Stars = _newMovie.Stars,
                ShowtimeId = _showtimeEntities.Count
            };

            _newShowtime = new Showtime()
            {
                Movie = _newMovie,
                StartDate = "2023-12-31",
                EndDate = "2023-12-31",
                Schedule = "12:00",
                AuditoriumId = 1
            };

            _newShowtimeEntity = new ShowtimeEntity()
            {
                Id = _showtimeEntities.Count,
                Movie = _newMovieEntity,
                StartDate = DateTime.Parse(_newShowtime.StartDate),
                EndDate = DateTime.Parse(_newShowtime.EndDate),
                Schedule = _newShowtime.Schedule.Split(',', StringSplitOptions.None).ToList(),
                AuditoriumId = _newShowtime.AuditoriumId
            };

            #endregion
        }

        public static TestDataProvider Instance => _instance ??= new TestDataProvider();

        public List<IMDBMovieInfo> GetMoviesInfos() => _moviesInfo;

        public List<MovieEntity> GetMovies() => _moviesEntities;

        public List<ShowtimeEntity> GetShowtimes() => _showtimeEntities;

        
        public IMDBMovieInfo GetNewMovieInfo() => _newMovieInfo;

        public Movie GetNewMovie() => _newMovie;

        public MovieEntity GetNewMovieEntity() => _newMovieEntity;

        public Showtime GetNewShowtime() => _newShowtime;

        public ShowtimeEntity GetNewShowtimeEntity() => _newShowtimeEntity;
    }
}
