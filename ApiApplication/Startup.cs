using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.Mapper;
using ApiApplication.Middleware;
using ApiApplication.Models;
using ApiApplication.Services;
using ApiApplication.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

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
            services.AddLogging();
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            services.AddHttpClient("ImdbApi", client =>
            {
                client.BaseAddress = new System.Uri(Configuration.GetSection("ImdbService:BaseUrl").Value.Trim());
            });

            services.AddTransient<IServiceImdbApi, ServiceImdbApi>();
            services.AddTransient<IServiceShowtime, ServiceShowtime>();
            services.AddSingleton<ImdbStatus>();
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();

            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme,
                    CustomAuthenticationSchemeOptions.AuthenticationScheme);

                options.RequireAuthenticatedSignIn = true;
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });

            services.ConfigureAuthorizationPolicies(Configuration);
            
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddHostedService<ServiceImdbStatus>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();


            app.UseMiddleware<ErrorHandlingMiddleware>(loggerFactory);
            app.UseMiddleware<RequestTimingMiddleware>(loggerFactory);

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
