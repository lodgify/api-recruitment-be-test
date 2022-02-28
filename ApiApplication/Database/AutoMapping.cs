using AutoMapper;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;

namespace ApiApplication.Database
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ShowtimeEntity, ShowtimeDto>();
            CreateMap<MovieEntity, MovieDto>();
            CreateMap<AuditoriumEntity, AuditoriumDto>();
        }
    }
}
