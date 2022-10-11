using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.ImdbService.Service;
using ApiApplication.Services;
using ApiApplication.Services.Implementors;
using ApiApplication.Utils;
using IMDbApiLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Security.Claims;

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
            var imdbApiKey = Configuration.GetValue<string>("ImdbApi:ApiKey");
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services
                .AddMvc()
                .AddJsonOptions(x => {
                    x.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();   
                });
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));                
            });
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddTransient<IImdbService, ImdbService.Service.Implementors.ImdbService>();
            services.AddTransient<IShowtimeService, ShowtimeService>();
            services.AddTransient<IImdbApiStatusService, ImdbApiStatusService>();
            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
            services.AddSingleton<IImdbApiStatusService, ImdbApiStatusService>();
            services.AddSingleton(new ApiLib(imdbApiKey));
            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;                
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });
            services.AddHostedService<ImdbStatusCheckerService>();
            services.AddAuthorization(options => {
                options.AddPolicy("Read", policy => policy.RequireClaim(claimType: ClaimTypes.Role, "Read"));
                options.AddPolicy("Write", policy => policy.RequireClaim(claimType: ClaimTypes.Role, "Write"));
                }
            );
            services.AddControllers(config => {
                config.Filters.Add(new ExecutionTrackingFilter());
                config.Filters.Add(new HttpResponseExceptionFilter());
            });
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
