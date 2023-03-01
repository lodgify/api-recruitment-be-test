using ApiApplication.Dtos;
using IMDbApiLib;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Worker
{
    public class ImdbBackground : IHostedService, IDisposable
    {
        private readonly ILogger<ImdbBackground> _logger;
        private readonly ApiLib _imdbClient;
        private Timer _timer;
        private int number;

        public ImdbBackground(ILogger<ImdbBackground> logger, ApiLib imdbClient)
        {
            _logger = logger;
            _imdbClient = imdbClient;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background imdb started");

            _timer = new Timer(this.TimerTick, Interlocked.Increment(ref number), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }

        private async void TimerTick(object value)
        {
            var data = await _imdbClient.UsageAsync();

            if (data != null)
            {
                IMDBStatus.SetValues(true, DateTime.Now);
            }
            else { IMDBStatus.SetValues(false, DateTime.Now); }

            _logger.LogInformation("Ping IMDB service");
        }

    }
}
