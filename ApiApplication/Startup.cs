using ApiApplication.Auth;
using ApiApplication.Database;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ApiApplication.Resources;
using ApiApplication.Services;
using Quartz;
using ApiApplication.Jobs;

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
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));                
            });
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;                
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });
            services.AddScoped<IShowtimesRepository, ShowtimesRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IImdbRepository, ImdbRepository>();
            services.AddScoped<ShowtimeService>();
            services.AddSingleton<IImdbPageStatus, ImdbPageStatus>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("ImdbJob");
                // base quartz scheduler, job and trigger configuration
                q.AddJob<ImdbJob>(opt => opt.WithIdentity(jobKey));
                q.AddTrigger(t => t
                .ForJob(jobKey).StartNow()
                .WithIdentity("ImdbJob-trigger")
                //.WithCronSchedule("* * * * * ?"));//run every 60 seconds
                .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(60)).RepeatForever()));
            });

            //services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

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
