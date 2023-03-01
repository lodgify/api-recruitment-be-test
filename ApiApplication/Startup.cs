using ApiApplication.Auth;
using ApiApplication.Configurations;
using ApiApplication.Database;
using ApiApplication.Facade;
using ApiApplication.Filters;
using ApiApplication.Service;
using ApiApplication.Tasks;
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

        services.AddHostedService<ConsumeScopedServiceHostedService>();
        services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
        services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
        services.AddTransient<IShowtimeService, ShowTimeService>();
        services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
        services.AddTransient<IImdbFacade, ImdbFacade>();
        services.AddHttpClient<HttpClientWrapper>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Write", policy => policy.RequireClaim(ClaimTypes.Role, "Write"));
            options.AddPolicy("Read", policy => policy.RequireClaim(ClaimTypes.Role, "Read"));
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

        SwaggerConfig.Configure(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
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