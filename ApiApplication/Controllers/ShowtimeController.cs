using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using ApiApplication.Exceptions;
using AutoMapper;
using IMDbApiLib;
using IMDbApiLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {

        private readonly IShowtimesRepository _repository;
        private readonly IMapper _mapper;       
        private readonly ApiLib _imdbLib;

        public ShowtimeController(IShowtimesRepository repository,
            IMapper mapper,           
            ApiLib imdbLib
            )
        {
            _repository = repository;
            _mapper = mapper;            
            _imdbLib = imdbLib;

        }

        [HttpGet]
        [Authorize(Policy = "ReadOnlyToken")]
        public ActionResult<IEnumerable<ShowTimeDTO>> Get([FromQuery] ShowTimeCriteriaDTO criteria)
        {

            if (!criteria.ShowTime.HasValue && string.IsNullOrEmpty(criteria.MovieTitle))
            {
                return Ok(_mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(_repository.GetCollection()));
            }

            var moviesSchedulesEntities = _repository.GetCollection(x =>

           (string.IsNullOrEmpty(criteria.MovieTitle) || x.Movie.Title.Equals(criteria.MovieTitle))
           &&
           (!criteria.ShowTime.HasValue || (criteria.ShowTime.Value.Date >= x.StartDate.Date && criteria.ShowTime.Value.Date <= x.EndDate.Date)));

            IEnumerable<ShowTimeDTO> _movieShowTimes = _mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(moviesSchedulesEntities);

            if (!_movieShowTimes.Any())
                return NotFound();

            return Ok(_movieShowTimes);

        }

        [HttpPost]
        [Authorize(Policy = "WriteOnlyToken")]
        public async Task<ActionResult<ShowTimeDTO>> Post([FromBody] ShowTimeDTO showTimeDTO)
        {
            TitleData data = await _imdbLib.TitleAsync(showTimeDTO.Movie.ImdbId, Language.en);

            if (!string.IsNullOrEmpty(data.ErrorMessage))
                return BadRequest($"IMDB movie does not exit with id ={showTimeDTO.Movie.ImdbId}");

            ShowtimeEntity showTimeEntity = _mapper.Map<ShowTimeDTO, ShowtimeEntity>(showTimeDTO);

            showTimeEntity.Movie.Title = data.Title;

            showTimeEntity.Movie.ReleaseDate = DateTime.Parse(data.ReleaseDate);

            showTimeEntity.Movie.Stars = data.Stars;

            var dbShowTime = _repository.Add(showTimeEntity);

            return _mapper.Map<ShowtimeEntity, ShowTimeDTO>(dbShowTime);
        }


        [HttpPut]
        [Authorize(Policy = "WriteOnlyToken")]
        public async Task<ActionResult<ShowTimeDTO>> Put([FromBody] ShowTimeDTO showTimeDTO)
        {
            ShowtimeEntity showTimeEntity = _mapper.Map<ShowTimeDTO, ShowtimeEntity>(showTimeDTO);

            if (showTimeDTO.Movie != null)
            {
                TitleData data = await _imdbLib.TitleAsync(showTimeDTO.Movie.ImdbId, Language.en);

                if (!string.IsNullOrEmpty(data.ErrorMessage))
                    return BadRequest($"IMDB movie does not exit with id ={showTimeDTO.Movie.ImdbId}");

                showTimeEntity.Movie.Title = data.Title;

                showTimeEntity.Movie.ReleaseDate = DateTime.Parse(data.ReleaseDate);

                showTimeEntity.Movie.Stars = data.Stars;

            }

            var result = _repository.Update(showTimeEntity);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Policy = "WriteOnlyToken")]
        [Route("{id}")]
        public ActionResult<ShowTimeDTO> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ShowTime id");
            }

            var showTimeDTO = _repository.Delete(id);

            if (showTimeDTO == null)
                return NotFound($"ShowTime not found with id = {id}");

            return Ok(showTimeDTO);

        }

        [HttpPatch]
        [Authorize(Policy = "WriteOnlyToken")]
        public ActionResult Patch()
        {
            throw new ApiApplication.Exceptions.InternalServerException("Exception thrown from patch request", System.Net.HttpStatusCode.InternalServerError);
        }

    }
}
