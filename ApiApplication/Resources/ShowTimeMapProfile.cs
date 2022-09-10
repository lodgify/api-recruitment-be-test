using System;
using System.Collections.Generic;
using ApiApplication.Database.Entities;
using AutoMapper;

namespace ApiApplication.Resources
{
    public class ShowTimeMapProfile: Profile
    {
        public ShowTimeMapProfile():base()
        {
            CreateMap<MovieEntity, Movie>();
            CreateMap<ShowtimeEntity, ShowTime>()
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => string.Join(",", new List<string>(src.Schedule))))
                .ForMember(dest=>dest.Movie, opt=> opt.MapFrom(src=>src.Movie));
        }
    }
}

