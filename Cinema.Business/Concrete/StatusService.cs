using Cinema.Business.Abstract;
using Cinema.Business.ConfigurationHelper;
using Cinema.Business.Constants;
using Cinema.Core.Utilities.Results;
using Cinema.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Concrete
{
    public class StatusService : IStatusService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppConfiguration _appConfiguration;
        public StatusService(IHttpClientFactory httpClientFactory, IAppConfiguration appConfiguration)
        {
            _httpClientFactory = httpClientFactory;
            _appConfiguration = appConfiguration;
        }
        public IResult GetStatus()
        {
            return new SuccessDataResult<ImdbStatusDto>(new ImdbStatusDto() { LastCall = ImdbStatus.Instance.LastCall, Up = ImdbStatus.Instance.Up }, Messages.StatusUpdated);
        }

        /// <summary>
        /// Setting the singleton instance based on Imdb api call.
        /// </summary>
        /// <returns>Imdb status information.</returns>
        public async Task<IResult> SetStatus()
        {
            var result = await _httpClientFactory.CreateClient("CinemaHttpClient").GetAsync($"{_appConfiguration.BaseUrl}/{_appConfiguration.Key}/tt0133093");
            if (result.IsSuccessStatusCode)
            {
                ImdbStatus.Instance.Up = true;
                ImdbStatus.Instance.LastCall = DateTime.Now;
            }
            else
            {
                ImdbStatus.Instance.Up = false;
                ImdbStatus.Instance.LastCall = DateTime.Now;
            }
            return new SuccessDataResult<ImdbStatusDto>(new ImdbStatusDto() { LastCall = ImdbStatus.Instance.LastCall, Up = ImdbStatus.Instance.Up }, Messages.StatusUpdated);
        }
    }
}
