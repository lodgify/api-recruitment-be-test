using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Helpers;
using ApiApplication.Models;
using ApiApplication.Models.Showtimes;
using ApiApplication.Services.IMDB;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiApplication.Services.Showtimes
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IImdbService _imdbService;
        private readonly ILogger<ShowtimeService> _logger;
        private readonly IMapper _mapper;

        public ShowtimeService(IShowtimesRepository showtimesRepository,
                               IImdbService imdbService,
                               IMapper mapper,
                               ILogger<ShowtimeService> logger)
        {
            _showtimesRepository = showtimesRepository;
            _imdbService = imdbService;
            _logger = logger;
            _mapper = mapper;
        }

        public ResponseModel<ShowtimeModel> Add(AddShowtimeModel model)
        {
            var response = new ResponseModel<ShowtimeModel>();

            try
            {
                var movie = _imdbService.GetByImdbId(model.Movie.ImdbId);

                var entity = _mapper.Map<ShowtimeEntity>(model);

                entity.Movie = _mapper.Map<MovieEntity>(movie.Result);

                var result = _showtimesRepository.Add(entity);

                var resultMapped = _mapper.Map<ShowtimeModel>(result);

                response.Ok(resultMapped);

            }

            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
                response.InternalServerError();
            }

            return response;
        }

        public ResponseModel<bool> Delete(int id)
        {

            var response = new ResponseModel<bool>();

            try
            {

                var result = _showtimesRepository.Delete(id);

                response.Ok(true);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
                response.InternalServerError();
            }

            return response;
        }

        public ResponseModel<IList<ShowtimeModel>> GetAll(string title, DateTime date)
        {
            var response = new ResponseModel<IList<ShowtimeModel>>();

            try
            {
                var filter = QueryFilter(title, date);

                var result = _showtimesRepository.GetCollection(filter).ToList();

                var resultMapped = _mapper.Map<List<ShowtimeModel>>(result);

                response.Ok(resultMapped);

            }
            catch (Exception exception)
            {

                _logger.LogError(exception.Message, exception);
                response.InternalServerError();
            }
            return response;
        }

        public ResponseModel<ShowtimeModel> GetById(int id)
        {
            var response = new ResponseModel<ShowtimeModel>();

            try
            {

                var result = _showtimesRepository.GetById(id);

                var resultMapped = _mapper.Map<ShowtimeModel>(result);

                response.Ok(resultMapped);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                response.InternalServerError();
            }

            return response;
        }

        public ResponseModel<ShowtimeModel> Update(ShowtimeModel model)
        {
            var response = new ResponseModel<ShowtimeModel>();

            try
            {
                var entity = _mapper.Map<ShowtimeEntity>(model);

                if (model.Movie != null)
                {
                    var movie = _imdbService.GetByImdbId(model.Movie.ImdbId);

                    entity.Movie = _mapper.Map<MovieEntity>(movie.Result);
                }

                var result = _showtimesRepository.Update(entity);

                var resultMapped = _mapper.Map<ShowtimeModel>(result);

                response.Ok(resultMapped);
            }

            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                response.InternalServerError();
            }


            return response;

        }


        private Expression<Func<ShowtimeEntity, bool>> QueryFilter(string title, DateTime date)
        {
            Expression<Func<ShowtimeEntity, bool>> filter = null;

            if (title != null)
            {
                filter = PredicateExtension.False<ShowtimeEntity>();
                filter = filter.Or(x => x.Movie.Title == title);
            }

            if (date != DateTime.MinValue)
            {
                if (filter != null)
                    filter = filter.Or(x => x.StartDate.Date <= date.Date && x.EndDate >= date.Date);
                else
                {
                    filter = PredicateExtension.False<ShowtimeEntity>();
                    filter = filter.Or(x => x.StartDate.Date <= date.Date && x.EndDate >= date.Date);
                }
            }

            return filter;

        }
    }
}
