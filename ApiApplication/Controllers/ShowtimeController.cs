using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.DTOs.Showtime;
using ApiApplication.Exceptions;
using ApiApplication.Filters;
using ApiApplication.Helpers;
using ApiApplication.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiApplication.Extensions;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;
        private readonly IImdbHelper _imdbHelper;

        public ShowtimeController(IShowtimesRepository showtimesRepository,
            IMapper mapper,
            IImdbHelper imdbHelper)
        {
            this._showtimesRepository = showtimesRepository;
            this._mapper = mapper;
            this._imdbHelper = imdbHelper;
        }

        [HttpGet]
        [AuthorizedToken(AuthorizedFor.Read)]
        public async Task<IActionResult> GetAll()
        {
            var showtimes = await _showtimesRepository.GetCollectionAsync();
            var response = _mapper.Map<IList<ShowtimeDto>>(showtimes);

            return Ok(response);
        }

        [HttpGet]
        [Route("date/{Date:regex(^\\d{{4}}-((0\\d)|(1[[012]]))-(([[012]]\\d)|3[[01]])$)}")]
        [AuthorizedToken(AuthorizedFor.Read)]
        public async Task<IActionResult> GetByDate([FromRoute] GetByDateRequest request)
        {
            var showtimes = await _showtimesRepository.GetAsync(l => l.StartDate <= request.Date && l.EndDate >= request.Date);
            var response = _mapper.Map<IList<ShowtimeDto>>(showtimes);

            return Ok(response);
        }

        [HttpGet]
        [Route("title/{Title}")]
        [AuthorizedToken(AuthorizedFor.Read)]
        public async Task<IActionResult> GetByTitle([FromRoute] GetByTitleRequest request)
        {
            var showtimes = await _showtimesRepository.GetAsync(l => l.Movie.Title.ToLower().Contains(request.Title.ToLower()));
            var response = _mapper.Map<IList<ShowtimeDto>>(showtimes);

            return Ok(response);
        }

        [HttpPost]
        [ValidateModel]
        [AuthorizedToken(AuthorizedFor.Write)]
        public async Task<IActionResult> Create([FromBody] CreateRequest request)
        {
            var apiMovieData = await _imdbHelper.GetMovieInformationByIdAsync(request.Movie.ImdbId);

            var showtimeEntity = new ShowtimeEntity()
            {
                AuditoriumId = request.AuditoriumId,
                Movie = new MovieEntity().Convert(apiMovieData),
                Schedule = Array.ConvertAll(request.Schedule.Split(',', StringSplitOptions.RemoveEmptyEntries), p => p.Trim()),
                EndDate = request.EndDate,
                StartDate = request.StartDate
            };

            await _showtimesRepository.AddAsync(showtimeEntity);

            var reponse = new CreateResponse()
            {
                Id = showtimeEntity.Id
            };

            return Ok(reponse);
        }

        [HttpPut]
        [ValidateModel()]
        [AuthorizedToken(AuthorizedFor.Write)]
        public async Task<IActionResult> Update([FromBody] UpdateRequest request)
        {
            if (request.Id == 0)
            {
                throw new NotExistsException();
            }

            var existingShowtimeEntity = await _showtimesRepository.GetByIdAsync(request.Id);
            if (existingShowtimeEntity == null)
            {
                throw new NotExistsException();
            }

            MovieEntity movieEntity = null;
            if (request.Movie != null)
            {
                var apiMovieData = await _imdbHelper.GetMovieInformationByIdAsync(request.Movie.ImdbId);
                movieEntity = existingShowtimeEntity.Movie.Convert(apiMovieData);
            }

            existingShowtimeEntity.AuditoriumId = request.AuditoriumId;
            existingShowtimeEntity.Movie = movieEntity;
            existingShowtimeEntity.Schedule = Array.ConvertAll(request.Schedule.Split(',', StringSplitOptions.RemoveEmptyEntries), p => p.Trim());
            existingShowtimeEntity.EndDate = request.EndDate;
            existingShowtimeEntity.StartDate = request.StartDate;

            await _showtimesRepository.UpdateAsync(existingShowtimeEntity);
            var reponse = new UpdateResponse()
            {
                Id = existingShowtimeEntity.Id
            };

            return Ok(reponse);
        }

        [HttpDelete]
        [Route("{Id:int}")]
        [AuthorizedToken(AuthorizedFor.Write)]
        public async Task<IActionResult> Remove([FromRoute] RemoveRequest request)
        {
            if (request.Id <= 0)
            {
                throw new NotExistsException();
            }

            var existingShowtimeEntity = await _showtimesRepository.GetByIdAsync(request.Id);
            if (existingShowtimeEntity == null)
            {
                throw new NotExistsException();
            }

            await _showtimesRepository.DeleteAsync(existingShowtimeEntity);
            var reponse = new RemoveResponse()
            {
                Id = request.Id
            };

            return Ok(reponse);
        }

        [HttpPatch]
        public async Task<IActionResult> RaisError()
        {
            throw new UnkownException("For testing the error handling!");

            return Ok();
        }
    }
}
