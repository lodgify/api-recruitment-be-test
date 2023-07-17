using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.DTO;
using ApiApplication.Services.Facade;
using ApiApplication.Services.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ShowtimeService : IShowtimeService
    {

        private readonly IShowtimesRepository _repo;
        private readonly IImdbFacade _imdbFacade;
        private readonly IMapper _mapper;

        public ShowtimeService(IShowtimesRepository repo, IImdbFacade imdbFacade, IMapper mapper)
        {
            _repo = repo;
            _imdbFacade = imdbFacade;
            _mapper= mapper;
        }

        public async Task<ShowtimeEntity> Add(ShowtimeCommand command)
        {
            MovieEntity movie = null;

            var showtime = _mapper.Map<ShowtimeEntity>(command);

            if (showtime != null)
            {
                movie = await _imdbFacade.DiscoverMovie(new IMDb.CriteriaImdb(showtime.Movie.ImdbId));
            }

            if (movie == null)
            {

                throw new Exception(Constants.Exception.MoviewNotFoundInIMDb);
            }
      
            showtime.Movie = movie;
            showtime.Movie.Id = 10;
            showtime.Movie.ShowtimeId = showtime.Id;
            showtime.Movie.ImdbId = showtime.Movie.ImdbId;

            _repo.Add(showtime);

            return showtime;
        }

        public async Task<ShowtimeEntity> Update(ShowtimeCommand command)
        {

            var showtime = _mapper.Map<ShowtimeEntity>(command);

            MovieEntity movie = null;

            if (showtime != null)
            {
                movie = await _imdbFacade.DiscoverMovie(new IMDb.CriteriaImdb(showtime.Movie.ImdbId));
                showtime.Movie = movie;
            }

            if (movie== null)
            {

                throw new Exception(Constants.Exception.MoviewNotFoundInIMDb);
            }
           
            _repo.Update(showtime);


            return showtime;

        }
    
        public IEnumerable<ScheduleDTO> GetShowTimeSchedule(string title = null, DateTime? date = null)
        {

            var filter = new FilterSchedule();

            var c = _repo.GetSchedule(filter.GetFilterSchedule(title,date));

            return c.ToList();

        }
    }
}
