using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieEntity, MovieDto>();

            CreateMap<ShowtimeEntity, ShowtimeDto>()
                .ForMember(d => d.Schedule, o => o.MapFrom(s => string.Join(',', s.Schedule)));                  
        }
    }
}
