using System.Text.Json;

using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.Extensions;
using ApiApplication.Models;
using ApiApplication.Models.Configurations;

using JorgeSerrano.Json;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiApplication {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<CinemaContext>(options => {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddCinema()
                    .AddImdbStatusChecker();

            services.AddSingleton(Configuration.GetSection("imdbConfig").Get<IMdbConfig>());

            services.AddAuthentication(options => {
                options.AddScheme<CustomAuthenticationHandler>(CustomAuthenticationSchemeOptions.AuthenticationScheme, CustomAuthenticationSchemeOptions.AuthenticationScheme);
                options.RequireAuthenticatedSignIn = true;
                options.DefaultScheme = CustomAuthenticationSchemeOptions.AuthenticationScheme;
            });

            services.AddAuthorization(opt => {
                opt.AddPolicy("ReadPolicy", p => p.RequireRole("Read"));
                opt.AddPolicy("WritePolicy", p => p.RequireRole("Write"));
            });

            services.AddControllers()
                    .AddJsonOptions(opt => {
                        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        opt.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler(appError => {
                    appError.Run(async context => {
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        ILogger<Startup> logger = context.RequestServices.GetService<ILogger<Startup>>();
                        if (contextFeature != null) {
                            logger.LogError(contextFeature.Error.Message);
                            context.Response.StatusCode = 500;
                            context.Response.ContentType = "application/json";
                            Result result = new Result(ResultCode.ServerError, "Internal Server Error Occurred. Please Call Provider!");
                            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                        }
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseMiddleware<ExecutionLoggingMiddleware>(); // inorder to log execution time for all resources uncomment this middleware use and remove "ExecutionTracker" action filter from ShowTimeController
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            SampleData.Initialize(app);
        }
    }
}
