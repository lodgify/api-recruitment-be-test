using ApiApplication.Database.Entities;
using ApiApplication.IMDb;
using ApiApplication.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Services.Facade
{
    public class ImdbFacade: IImdbFacade
    {
        private readonly IHttpClientWrapper _service;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ImdbFacade(IHttpClientWrapper httpClientWrapper, IConfiguration configuration  , IMapper mapper)
        {
            _service = httpClientWrapper;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<HttpResponseMessage> GetMovieHttpResponseMessage(CriteriaImdb criteria)
        {

            var query = criteria.Language + _configuration.GetSection("QueryMovie").Value +
                 _configuration.GetSection("Apikey").Value + "/" + criteria.Id;


            var response = await _service.GetAsyncyHttpResponseMessage<HttpResponseMessage>(
                _configuration.GetSection("UriMovieInfo").Value,
                query);

          

            return response;
        }

        public async Task<MovieEntity> DiscoverMovie(CriteriaImdb criteria)
        {

            var query = criteria.Language  +  _configuration.GetSection("QueryMovie").Value + 
                 _configuration.GetSection("Apikey").Value + "/" + criteria.Id;


            var response = await _service.GetAsync<TitleDataDto>(
                _configuration.GetSection("UriMovieInfo").Value,
                query);

            var res = _mapper.Map<TitleDataDto,MovieEntity>(response);

            return res;
        }

        private MovieEntity map(TitleDataDto response)
        {
            var movie = new MovieEntity();
            movie.Stars = response.Stars;
            movie.Title = response.Title;
            return movie;
        }

        private IEnumerable<KeyValuePair<string, string>> CriteriaToParams(CriteriaImdb criteria)
        {
            var values = new List<KeyValuePair<string, string>>();

            values.Add(new KeyValuePair<string, string>("lang", criteria.Language));
            values.Add(new KeyValuePair<string, string>("id", criteria.Id));
            

            return values;
        }


    }
}
