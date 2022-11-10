using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private readonly IShowtimesRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClient;
        private readonly IMapper _mapper;
        public ShowTimeService(IShowtimesRepository repository, IConfiguration configuration, IHttpClientFactory httpclient, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _httpClient = httpclient;
            _mapper = mapper;
        }
        public IEnumerable<ShowTimeDTO> GetCollection()
        {
            return _mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(_repository.GetCollection());
        }

        public IEnumerable<ShowTimeDTO> GetCollection(ShowTimeCriteriaDTO criteria)
        {
            var moviesSchedulesEntities = _repository.GetCollection(x =>

            (string.IsNullOrEmpty(criteria.MovieTitle) || x.Movie.Title.Equals(criteria.MovieTitle))
            &&
            (criteria.ShowTime.Value.Date >= x.StartDate.Date && criteria.ShowTime.Value.Date <= x.EndDate.Date)

            );

            IEnumerable<ShowTimeDTO> _movieShowTimes = _mapper.Map<IEnumerable<ShowtimeEntity>, IEnumerable<ShowTimeDTO>>(moviesSchedulesEntities);

            return _movieShowTimes;

            #region another possible way to implement client of GetCollection
            //var moviesSchedules = _repository.GetCollection(query =>
            //  {
            //      if (string.IsNullOrEmpty(filter.MovieTitle))
            //      {
            //          query.Where(x => x.Movie.Title.Equals(filter.MovieTitle));
            //      }

            //      if (filter.ShowTime.HasValue)
            //      {
            //          query.Where(x => x.StartDate.Equals(filter.ShowTime));
            //      }

            //      return query;

            //  }); 
            #endregion
        }

        public async Task<ShowTimeDTO> Create(ShowTimeDTO showTime)
        {

            JObject jObject = await GetIMDBMovie(showTime.Movie.ImdbId);

            ShowtimeEntity showTimeEntity = _mapper.Map<ShowTimeDTO, ShowtimeEntity>(showTime);

            showTimeEntity.Movie.Title = jObject["title"]?.ToString();

            showTimeEntity.Movie.ReleaseDate = Convert.ToDateTime(jObject["releaseDate"]);

            showTimeEntity.Movie.Stars = jObject["stars"]?.ToString();

            return _mapper.Map<ShowtimeEntity, ShowTimeDTO>(_repository.Add(showTimeEntity));

        }

        public ShowTimeDTO Update(ShowTimeDTO showTimeDTO)
        {
            ShowtimeEntity dbShowTime = _mapper.Map<ShowTimeDTO, ShowtimeEntity>(showTimeDTO);

            return _mapper.Map<ShowtimeEntity, ShowTimeDTO>(dbShowTime);
        }

        private async Task<JObject> GetIMDBMovie(string imdbID)
        {
            JObject jObject = null;

            var restClient = _httpClient.CreateClient("IMDBClient");

            string url = $"/Title/{_configuration.GetValue<string>("IMDBApiKey")}/{imdbID}";

            HttpResponseMessage response = await restClient.GetAsync(url);


            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                jObject = JsonConvert.DeserializeObject<JObject>(json);
            }

            return jObject;

        }        
    }
}
