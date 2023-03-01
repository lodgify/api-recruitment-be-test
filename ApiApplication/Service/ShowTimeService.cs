using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Facade;
using ApiApplication.Filters;
using ApiApplication.Models.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Service
{
    public class ShowTimeService : IShowtimeService
    {
        private readonly IShowtimesRepository _repo;
        private readonly IImdbFacade _imdbFacade;
        private readonly IMapper _mapper;

        public ShowTimeService(IShowtimesRepository repo, IImdbFacade imdbFacade, IMapper mapper)
        {
            _repo = repo;
            _imdbFacade = imdbFacade;
            _mapper = mapper;
        }

        public async Task<ShowtimeEntity> Add(ShowtimeViewModel command)
        {
            MovieEntity movie = null;

            var showtime = _mapper.Map<ShowtimeEntity>(command);

            if (showtime != null)
            {
                movie = await _imdbFacade.DiscoverMovie(new CriteriaImdb(showtime.Movie.ImdbId));
            }

            if (movie == null)
            {

                throw new Exception(Constants.Exception.MoviewNotFoundInIMDb);
            }

            showtime.Movie = movie;
            showtime.Movie.Id = 0;
            showtime.Movie.ShowtimeId = showtime.Id;
            showtime.Movie.ImdbId = showtime.Movie.ImdbId;

            _repo.Add(showtime);

            return showtime;
        }

        public async Task<ShowtimeEntity> Update(ShowtimeViewModel command)
        {

            var showtime = _mapper.Map<ShowtimeEntity>(command);

            MovieEntity movie = null;

            if (showtime != null)
            {
                movie = await _imdbFacade.DiscoverMovie(new CriteriaImdb(showtime.Movie.ImdbId));
                movie.Id = showtime.Id;
                movie.ShowtimeId = showtime.Id;
                showtime.Movie = movie;
            }

            if (movie == null)
            {

                throw new Exception(Constants.Exception.MoviewNotFoundInIMDb);
            }

            _repo.Update(showtime);


            return showtime;

        }

        public IEnumerable<ScheduleViewModel> GetShowTimeSchedule(string title = null, DateTime? date = null)
        {

            var filter = new FilterSchedule();

            var c = _repo.GetSchedule(filter.GetFilterSchedule(title, date));

            return c.ToList();

        }

        public ShowtimeEntity DeleteTimeSchedule(int id)
        {
            var result = _repo.Delete(id);

            return result;

        }
    }
}
