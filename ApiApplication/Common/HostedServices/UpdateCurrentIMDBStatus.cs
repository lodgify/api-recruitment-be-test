using IMDbApiLib;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Common.HostedServices
{
    public class UpdateCurrentIMDBStatus : IHostedService, IDisposable
    {
        private Timer? _timer = null;

        public UpdateCurrentIMDBStatus()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private async void ExecuteTask(object state)
        {
            var apiLib = new ApiLib("k_ai05h4st");
            try
            {
                var result = await apiLib.ComingSoonAsync();
                IMDBStatus.Instance.Up = true;
            }
            catch (Exception)
            {
                IMDBStatus.Instance.Up = false;
            }
            finally 
            {
                IMDBStatus.Instance.LastCall = DateTime.UtcNow;
            }
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
