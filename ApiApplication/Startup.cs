using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using ApiApplication.Middleware;
using AutoMapper;
using IMDbApiLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReadOnlyToken", policy =>
                {
                    policy.Requirements.Add(new ReadToken("Read", "1234"));


                });

                options.AddPolicy("WriteOnlyToken", policy =>
                {
                    policy.Requirements.Add(new WriteToken("Write", "7894"));


                });
            });

            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });



            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
            services.AddSingleton(provider => new ApiLib(Configuration.GetValue<string>("IMDBApiKey")));

            services.AddHttpClient("ApiClient", httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration.GetValue<string>("ApiEndpoint"));
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ShowtimeEntity, ShowTimeDTO>().ReverseMap();

                config.CreateMap<MovieEntity, MovieDTO>().ReverseMap();

            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);

            services.AddSingleton<IAuthorizationHandler, ReadTokenHandler>();

            services.AddSingleton<IAuthorizationHandler, WriteTokenHandler>();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<RequestTimeCalculatorMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SampleData.Initialize(app);
        }


    }
}
