using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using AutoMapper;
using IMDbApiLib;
using IMDbApiLib.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class IMDBHttpClientManager : IIMDBHttpClientManager
    {
        private readonly IConfiguration _configuration;

        public IMDBHttpClientManager( IConfiguration configuration)
        {            
            _configuration = configuration;
        }

        public async Task<TitleData> GetIMDBJObject(string imdbID)
        {
            var imdbApi = new ApiLib(_configuration.GetValue<string>("IMDBApiKey"));

            TitleData data = await imdbApi.TitleAsync(imdbID, Language.en);

            return data;
        }
    }
}
