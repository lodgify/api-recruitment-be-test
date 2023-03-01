using ApiApplication.Database;
using ApiApplication.Facade;
using ApiApplication.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Tasks
{
    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly IImdbFacade _imdbFacade;
        private readonly IConfiguration _configuration;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, IImdbFacade imdbFacade, IConfiguration configuration)
        {
            _logger = logger;
            _imdbFacade = imdbFacade;
            _configuration = configuration;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    Constants.Log.TraceScopedProcessingWorking + " {Count}", executionCount);

                await JobImdbApiCall();

                await Task.Delay(GetDelay(), stoppingToken);
            }
        }

        private async Task JobImdbApiCall()
        {
            var basePath = _configuration.GetSection("ImdbApi:BasePath").Value;
            var query = _configuration.GetSection("ImdbApi:Language").Value + _configuration.GetSection("ImdbApi:Query").Value +
                 _configuration.GetSection("ImdbApi:Apikey").Value;
            DateTime lastCall = DateTime.Now;

            var res = await _imdbFacade.GetHttpResponseMessage(basePath, query);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                StatusImdb.SetStatusImdb(true, lastCall);
            }
            else
            {
                StatusImdb.SetStatusImdb(false, lastCall);
            }
        }

        private int GetDelay()
        {
            //TODO
            var miliSecondsDelay = 60000;

            return miliSecondsDelay;


        }
    }
}