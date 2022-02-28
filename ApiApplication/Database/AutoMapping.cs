using AutoMapper;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;

namespace ApiApplication.Database
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //CreateMap<ShowtimeEntity, ShowtimeDto>();
            //CreateMap<MovieEntity, MovieDto>();
            //CreateMap<AuditoriumEntity, AuditoriumDto>();
            CreateMap<ShowtimeEntity, ShowtimeDto>().ReverseMap();
            CreateMap<MovieEntity, MovieDto>()
                .ForMember(dest =>
                    dest.ImdbId,
                    opt => opt.MapFrom(src => src.ImdbId))
                .ForMember(dest =>
                    dest.ReleaseDate,
                    opt => opt.MapFrom(src => src.ReleaseDate))
                .ReverseMap();
            CreateMap<AuditoriumEntity, AuditoriumDto>().ReverseMap();
        }
    }
}
