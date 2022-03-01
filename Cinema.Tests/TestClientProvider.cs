using ApiApplication;
using Cinema.Business.Abstract;
using Cinema.Business.Concrete;
using Cinema.Business.ConfigurationHelper;
using Cinema.DataAccess.Abstract;
using Cinema.DataAccess.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Cinema.Tests
{
    class TestClientProvider
    {
        public HttpClient Client { get; set; }
        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder()
                .ConfigureTestServices(services =>
                {
                    services.AddScoped<IStatusService, StatusService>();
                    services.AddScoped<IAppConfiguration, AppConfiguration>();
                    services.AddScoped<IShowtimeService, ShowtimeService>();
                    services.AddScoped<IShowtimeEntityDal, ShowtimeEntityDal>();
                    services.AddScoped<IMovieEntityDal, MovieEntityDal>();
                    services.AddScoped<IAuditoriumEntityDal, AuditoriumEntityDal>();
                })

                .UseStartup<Startup>());

            Client = server.CreateClient();
        }
    }
}
