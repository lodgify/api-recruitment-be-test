using ApiApplication.DTOs.API;
using ApiApplication.DTOs.IMDB;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace ApiApplication.Profilers
{
    public class MapperProfiler : Profile
    {
        public MapperProfiler()
        {
            CreateMap<ShowtimeEntity, Showtime>()
                .ForMember(dest => dest.StartDate, o => o.MapFrom(src => src.StartDate.ToString()))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(src => src.EndDate.ToString()))
                .ForMember(dest => dest.Schedule, o => o.MapFrom(src => src.Schedule.Join(",")));
            CreateMap<Showtime, ShowtimeEntity>()
                .ForMember(dest => dest.StartDate, o => o.MapFrom(src => DateTime.Parse(src.StartDate)))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(src => DateTime.Parse(src.EndDate)))
                .ForMember(dest => dest.Schedule, o => o.MapFrom(src => src.Schedule.Split(',', StringSplitOptions.None).ToList()))
                .ForMember(dest => dest.Movie, o => o.Ignore());

            CreateMap<MovieEntity, Movie>();
            CreateMap<Movie, MovieEntity>();

            CreateMap<IMDBMovieInfo, MovieEntity>();
        }
    }
}
