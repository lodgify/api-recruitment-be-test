using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.Filters;
using ApiApplication.Services;
using ApiApplication.Services.Facade;
using ApiApplication.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));                
            });

         
            services.AddAutoMapper(typeof(Startup));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Write", policy => policy.RequireClaim(ClaimTypes.Role,"Write"));
                options.AddPolicy("Read", policy => policy.RequireClaim(ClaimTypes.Role, "Write","Read"));
            });

            services.AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>();
            services.AddAuthentication(options =>
            {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;                
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });

            services.AddControllers().AddJsonOptions(o => 
            o.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            );

            services.AddControllers(config =>
            {
                config.Filters.Add<ActionFilterAsyncLog>();
                config.Filters.Add<ActionFilterAsyncValidation>();
            });
            
            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddTransient<IShowtimeService, ShowtimeService>();          
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient<IImdbFacade, ImdbFacade>();
            services.AddHttpClient<HttpClientWrapper>();

            SwaggerConfig.Configure(services);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
                });
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
