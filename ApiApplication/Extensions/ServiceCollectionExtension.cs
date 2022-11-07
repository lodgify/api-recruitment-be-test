using System;
using System.Linq;
using System.Net.Http;

using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.DTO;
using ApiApplication.HostedServices;
using ApiApplication.Infra;
using ApiApplication.Services;
using ApiApplication.Services.Validatores;
using ApiApplication.Utils;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ApiApplication.Extensions {
    public static class ServiceCollectionExtension {
        public static IServiceCollection AddCinema(this IServiceCollection services) {
            services.AddTransient<IShowtimesRepository, ShowtimesRepository>()
                    .AddTransient<IShowTimeService, ShowTimeService>()
                    .AddTransient<IShowTimeValidatore, ShowTimeValidator>()
                    .AddSingleton<IImdbService, ImdbService>()
                    .AddTransient<IUnitOfWork, UnitOfWork>()
                    .AddSingleton<ICustomAuthenticationTokenService, CustomAuthenticationTokenService>()
                    .AddTransient<IConfigureOptions<MvcOptions>, ResultFormatterSetup>();

            MapperConfiguration config = new MapperConfiguration(ConfigDataModels);
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper)
                    .AddSingleton<IObjectMapper, AutoMapperWrapper>()
                    .AddSingleton<HttpClient>();

            return services;
        }

        public static IServiceCollection AddImdbStatusChecker(this IServiceCollection services) {
            services.AddSingleton<ImdbStatus>()
                    .AddHostedService<ImdbStatusCheckerService>();
            return services;
        }

        public static void ConfigDataModels(IMapperConfigurationExpression mapper) {
            mapper.CreateMap<ShowTime, ShowtimeEntity>()
                  .ForMember(dest => dest.Schedule, opt => {
                      opt.AllowNull();
                      opt.PreCondition(c => !string.IsNullOrWhiteSpace(c.Schedule));
                      opt.MapFrom(src => src.Schedule.Split(',', System.StringSplitOptions.RemoveEmptyEntries));
                  });

            mapper.CreateMap<ShowtimeEntity, ShowTime>()
                  .ForMember(dest => dest.Schedule, opt => {
                      opt.AllowNull();
                      opt.PreCondition(c => c.Schedule?.Any() != false);
                      opt.MapFrom(src => string.Join(',', src.Schedule));
                  });


            mapper.CreateMap<Movie, MovieEntity>()
                  .ReverseMap();

            mapper.CreateMap<ImdbMovie, Movie>()
                  .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateTime.Parse(src.ReleaseDate)));
        }
    }
}
