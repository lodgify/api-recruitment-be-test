using ApiApplication.Database;
using ApiApplication.Dtos;
using ApiApplication.Services.RemoteServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Services.ScheduledJobs
{
    public class ImdbStatusScheduledJob : BackgroundService
    {
        private readonly IOptions<AppSettingsModel> _settings;
        private ImdbRemoteService _imdbRemoteService;
        private ImdbStatusService _imdbStatusService;
        public ImdbStatusScheduledJob(IOptions<AppSettingsModel> settings,
            ImdbRemoteService imdbRemoteService,
            ImdbStatusService imdbStatusService)
        {
            _settings = settings;
            _imdbRemoteService = imdbRemoteService;
            _imdbStatusService = imdbStatusService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var apiResult = await _imdbRemoteService.GetMovieInformation(_settings.Value.TestMovieId);
                if (apiResult == null)
                {
                    _imdbStatusService.Up = false;
                }
                else
                {
                    _imdbStatusService.Up = true;
                }
                _imdbStatusService.Last_call = DateTime.Now;
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
