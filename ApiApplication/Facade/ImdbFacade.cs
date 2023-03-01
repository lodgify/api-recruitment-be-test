using ApiApplication.Database.Entities;
using ApiApplication.Database.Entities;
using ApiApplication.Models.ViewModels;
using ApiApplication.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Facade
{
    public class ImdbFacade : IImdbFacade
    {
        private readonly IHttpClientWrapper _service;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ImdbFacade(IHttpClientWrapper httpClientWrapper, IConfiguration configuration, IMapper mapper)
        {
            _service = httpClientWrapper;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<HttpResponseMessage> GetHttpResponseMessage(string baseUri, string searchCriteria)
        {
            var response = await _service.GetAsyncyHttpResponseMessage<HttpResponseMessage>(
                baseUri,
                searchCriteria);
            return response;
        }

        public async Task<MovieEntity> DiscoverMovie(CriteriaImdb criteria)
        {
            var query = criteria.Language + _configuration.GetSection("ImdbApi:QueryMovie").Value +
                 _configuration.GetSection("ImdbApi:Apikey").Value + "/" + criteria.Id;


            var response = await _service.GetAsync<TitleDataViewModel>(
                _configuration.GetSection("ImdbApi:BasePath").Value,
                query);

            var res = _mapper.Map<TitleDataViewModel, MovieEntity>(response);

            return res;
        }
    }
}
