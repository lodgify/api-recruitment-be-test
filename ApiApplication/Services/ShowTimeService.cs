using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Mappers;
using ApiApplication.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IImdbService _imdbService;
        private readonly int[] allowedAuditoriumIds = { 1, 2, 3 };

        public ShowTimeService(IShowtimesRepository showtimesRepository, IImdbService imdbService) 
        { 
            _showtimesRepository = showtimesRepository;
            _imdbService = imdbService;
        }

        public async Task Create(ShowTimeRequestModel showTime)
        {
            if (string.IsNullOrEmpty(showTime.Movie.ImdbId))
            {
                throw new Exception("Movie imdb id is invalid.");
            }

            if (!allowedAuditoriumIds.Contains(showTime.AuditoriumId))
            {
                throw new Exception("Auditorium Id is invalid.");
            }

            var movieEntity =  await _imdbService.GetMovieAsync(showTime.Movie.ImdbId);

            var showTimeEntity = ShowTimeMapper.MapToEntity(showTime, movieEntity);

            _showtimesRepository.Add(showTimeEntity);
        }

        public IEnumerable<ShowTimeResponseModel> Get(DateTime? date, string movieTitle)
        {
            var showTimes = _showtimesRepository.GetCollection();

            if (date != null)
            {
                showTimes = showTimes.Where(s => s.StartDate <= date && s.EndDate >= date);
            }

            if (movieTitle != null)
            {
                showTimes = showTimes.Where(s => s.Movie.Title.Contains(movieTitle));
            }

            return showTimes.Select(s => ShowTimeMapper.MapToModel(s));
        }

        public ShowTimeResponseModel Update(ShowTimeRequestModel showTime)
        {
            // pass this to validator and return no content modified
            if (showTime.Movie == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(showTime.Movie.ImdbId))
            {
                throw new Exception("Movie imdb id is invalid.");
            }

            if (!allowedAuditoriumIds.Contains(showTime.AuditoriumId))
            {
                throw new Exception("Auditorium Id is invalid.");
            }

            var showTimeEntity = ShowTimeMapper.MapToEntity(showTime);

            var showTimeUpdated = _showtimesRepository.Update(showTimeEntity);

            return ShowTimeMapper.MapToModel(showTimeUpdated);
        }

        public ShowTimeResponseModel Update(int id, JsonPatchDocument<ShowTimeRequestModel> showTimePatch)
        {
            throw new Exception("Error on partial update Showtime");

            //// pass this to validator and return no content modified
            //if (showTime.Movie == null)
            //{
            //    return null;
            //}

            //if (string.IsNullOrEmpty(showTime.Movie.ImdbId))
            //{
            //    throw new Exception("Movie imdb id is invalid.");
            //}

            //if (!allowedAuditoriumIds.Contains(showTime.AuditoriumId))
            //{
            //    throw new Exception("Auditorium Id is invalid.");
            //}

            //var showTimeEntity = _showtimesRepository.GetCollection( s => s.Id == id );

            //ShowTimeMapper.MapToEntity()

            //showTimePatch.ApplyTo(showTimeEntity);

            //var showTimeUpdated = _showtimesRepository.Update(showTimeEntity);

            //return ShowTimeMapper.MapToModel(showTimeUpdated);
        }


        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Showtime id is invalid.");
            }

            _showtimesRepository.Delete(id);
        }
    }
}
