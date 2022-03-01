using Cinema.Business.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Helpers.HostedService
{
    /// <summary>
    /// Calls the Imdb Api for every 60 seconds and updates the singleton object
    /// </summary>
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer _timer = null!;
        IStatusService _statusService;

        public TimedHostedService(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);
            _statusService.SetStatus();
           
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
