using ApiApplication.Core.Worker;
using Lodgify.Cinema.Infrastructure.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiApplication
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureEnvironment()
                    .ConfigureBrotliAndGzipResponseCompression()
                    .ConfigureFilters()
                    .ConfigureNotificationAndLog()
                    .ConfigureIocDbDependencies()
                    .ConfigureBusinessDependencies()
                    .ConfigureAuthentication()
                    .AddResponseCaching()
                    .AddHostedService(s => new ImdbHealtCheckWebWorker(services))
                    .AddHealthChecks();

            services.AddResponseCaching();

            services.AddControllers()
                    .ApplySnakeCasePolicie();

            services.ConfigureSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerAndUi();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/healthz");
            app.Initialize();
        }
    }
}