using ApiApplication.DTOs.API;
using ApiApplication.Options;
using ApiApplication.WebClients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Background
{
    /// <summary>
    /// Represents a class that checks the status for the IMDB Api in a background task.
    /// </summary>
    public class IMDBStatusBackgroundTask : IHostedService, IDisposable
    {
        private readonly IIMDBWebApiClient _webApiClient;
        private readonly WebApiClientOptions _options;
        private Timer _timer;

        public IMDBStatusBackgroundTask(IIMDBWebApiClient webApiClient, IOptions<WebApiClientOptions> options)
        {
            _webApiClient = webApiClient ?? throw new ArgumentNullException(nameof(webApiClient));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(webApiClient)); ;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async o =>
            {
                var lastCall = DateTime.Now;
                var status = await _webApiClient.GetStatus();

                var newStatus = new IMDBStatus()
                {
                    LastCall = lastCall,
                    Up = status == System.Net.HttpStatusCode.OK
                };

                IMDBStatusSingleton.Instance.Status = newStatus;
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(_options.WebApiStatusTimestamp));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
