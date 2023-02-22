using ApiApplication.Extensions;
using ApiApplication.Services;
using ApiApplication.Utils;
using CinemaApplication.DAL;
using CinemaApplication.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddConfigurations(Configuration)
                .AddServices()
                .AddHostedService<ImdbBackgroundService>()
                .AddSingleton<ImdbStatusModel>()
                .AddAuth()
                .AddSwager()
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app
               .AddMiddlewares()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints => { endpoints.MapControllers(); });

            SampleData.Initialize(app);
        }
    }
}