using AutoMapper;
using Cinema.Entities.Concrete;
using Cinema.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Helpers.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MovieDto, MovieEntity>().ReverseMap();                  
            CreateMap<ShowtimeEntity, ShowtimeDto>().ReverseMap();
            CreateMap<ImdbMovieDto, MovieEntity>().ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.Id)).ForMember(x => x.Id, y => y.Ignore());
        }       
    }
}
