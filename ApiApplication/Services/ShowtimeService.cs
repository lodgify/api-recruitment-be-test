using System;
using System.Linq;
using ApiApplication.Resources;
using System.Collections.Generic;
using ApiApplication.Database;
using AutoMapper;
using System.Threading.Tasks;
using ApiApplication.Database.Entities;
using ApiApplication.Exceptions;

namespace ApiApplication.Services
{
    public class ShowtimeService
    {
        private IShowtimesRepository showTimeRepository;
        private IImdbRepository imdbRepository;
        private IMovieRepository movieRepository;
        protected IMapper mapper;

        public ShowtimeService(IMapper mapper, IShowtimesRepository showTimeRepository, IImdbRepository imdbRepository, IMovieRepository movieRepository)
        {
            this.showTimeRepository = showTimeRepository;
            this.imdbRepository = imdbRepository;
            this.movieRepository = movieRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShowTime>> GetShowtimes()
        {
            var shows = await showTimeRepository.GetCollection();
            var result = this.mapper.Map<IEnumerable<ShowTime>>(shows);
            return result;
        }

        public async Task<ShowTime> CreateShowtime(ShowTime showTime)
        {
            var title = await imdbRepository.GetTitle(showTime.Movie.ImdbId);
            var show = this.mapper.Map<ShowtimeEntity>(showTime);
            show.Movie = this.mapper.Map<MovieEntity>(title);
            var result = await showTimeRepository.Add(show);
            return this.mapper.Map<ShowTime>(show);
        }

        public async Task<ShowTime> UpdateShowtime(int id, ShowTime showTime)
        {
            var result = await showTimeRepository.GetById(id);
            if (result == null) throw new MovieNotFoundException();
            if (result.Movie==null && showTime.Movie!=null && string.IsNullOrEmpty(showTime.Movie.ImdbId)) new MovieNotFoundException();
            if (result.Movie == null || (result.Movie!=null && showTime.Movie!=null && result.Movie.ImdbId != showTime.Movie.ImdbId))
            {
                var movie = await movieRepository.GetByImdbId(showTime.Movie.ImdbId);
                if (movie==null)
                {
                    var title = await imdbRepository.GetTitle(showTime.Movie.ImdbId);
                    result.Movie = this.mapper.Map<MovieEntity>(title);
                }
                else
                {
                    result.Movie = movie;
                }
            }
            result.AuditoriumId = showTime.AuditoriumId;
            result.EndDate = DateTime.Parse(showTime.EndDate);
            result.Schedule = showTime.Schedule.Split(",");
            result.StartDate = DateTime.Parse(showTime.StartDate);
            await this.showTimeRepository.Update(result);
            return showTime;

        }
    }
}

