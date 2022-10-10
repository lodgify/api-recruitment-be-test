﻿using ApiApplication.Database.Entities;
using ApiApplication.Models;
using AutoMapper;

namespace ApiApplication.Mappings
{
    public class ShowtimeProfile : Profile
    {
        public ShowtimeProfile() 
        {
            CreateMap<MovieEntity, Movie>()
                .ReverseMap();
            CreateMap<ShowtimeEntity, Showtime>()
                .ForMember(model => model.Schedule, opt => opt.MapFrom(ent => string.Join(",", ent.Schedule)))
                .ForMember(model => model.Movie, opt => opt.MapFrom(ent => ent.Movie))
                .ReverseMap();                
        }
    }
}