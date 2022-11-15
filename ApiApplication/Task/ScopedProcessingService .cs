using ApiApplication.IMDb;
using ApiApplication.Services.Facade;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication
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
         

           
            var criteria = new CriteriaImdb(_configuration.GetSection("DefaultParameterIdMovieImdbApi").Value);
            DateTime lastCall = DateTime.Now;

            var res = await _imdbFacade.GetMovieHttpResponseMessage(criteria);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                StatusImdb.SetStatusImdb(true, lastCall);
            }
            else
            {
                StatusImdb.SetStatusImdb(false, lastCall);
            }

        }

        private int  GetDelay()
        {
            int result;
            if (int.TryParse(_configuration.GetSection("StatusTaskExecutionFrequency").Value, out result) == false)
            {
                throw new Exception(Constants.Exception.CanNotReadStatusTaskExecutionFrequencyInConfigurationFile);
            }
            var miliSecondsDelay = int.Parse(_configuration.GetSection("StatusTaskExecutionFrequency").Value);

            return miliSecondsDelay;


        }
    }
}
