using ApiApplication.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace ApiApplication.Tests.Infrastructure
{
    public class TestApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<CinemaContext>>();
                services.AddDbContext<CinemaContext>(options =>
                {
                    options.UseInMemoryDatabase("CinemaDb")
                        .EnableSensitiveDataLogging()
                        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            });
        }

        protected override IHostBuilder CreateHostBuilder() => base.CreateHostBuilder().UseEnvironment("Development");

    }
}
