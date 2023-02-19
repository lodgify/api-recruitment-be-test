using System;
using System.Collections.Generic;
using System.Linq;
using ApiApplication.Database.Entities;
using AutoMapper;

namespace ApiApplication.Resources
{
    
    public class ShowTimeMapProfile: Profile
    {
        private static Random rnd = new Random();
        public ShowTimeMapProfile():base()
        {
            CreateMap<MovieEntity, Movie>();
            CreateMap<Movie, MovieEntity>();

            CreateMap<ShowtimeEntity, ShowTime>()
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => string.Join(",", new List<string>(src.Schedule))))
                .ForMember(dest=>dest.Movie, opt=> opt.MapFrom(src=>src.Movie));
            CreateMap<ShowTime, ShowtimeEntity>()
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Schedule.Trim()) ? new List<string>() : src.Schedule.Split(",", System.StringSplitOptions.None).ToList<string>()));

            CreateMap<ImdbTitleResponse, MovieEntity>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => rnd.Next(2, int.MaxValue)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

        }
    }
}

