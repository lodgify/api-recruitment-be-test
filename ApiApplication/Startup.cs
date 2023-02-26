using ApiApplication.Auth;
using ApiApplication.BgTaskImdb;
using ApiApplication.Database;
using ApiApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace ApiApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var apiApplicationSettings = Configuration.GetSection("ApiApplicationConfiguration").Get<ApiApplicationConfiguration>();

            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase(apiApplicationSettings.Database)
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));                
            });

            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddTransient<IImdbApiService, ImdbApiService>();
            services.AddTransient<IShowTimeService, ShowTimeService>();
            services.AddTransient<IImdbService, ImdbService>();

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema Api", Version = "v1" }));

            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;                
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiKeyReadPolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(CustomAuthenticationSchemeOptions.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Read");
                });

                options.AddPolicy("ApiKeyWritePolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(CustomAuthenticationSchemeOptions.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Write");
                });
            });

            services.AddControllers();

            services.AddSingleton<IImdb, Imdb>();
            services.AddHttpClient("imdbApi", client =>
            {
                client.BaseAddress = new Uri(apiApplicationSettings.ImdbApiBaseUrl);
            });
            services.AddHostedService<ImdbWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<TrackingTimeMiddleware> trackingLogger, 
                              ILogger<ExceptionHandlerMiddleware> exceptionLogger)
        {
            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage();                
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>(exceptionLogger);
            app.UseMiddleware<TrackingTimeMiddleware>(trackingLogger);

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SampleData.Initialize(app);

            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema v1"));
        }      
    }
}
