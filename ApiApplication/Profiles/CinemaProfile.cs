using ApiApplication.Common;
using ApiApplication.Database.Entities;
using ApiApplication.Resources;
using AutoMapper;

namespace ApiApplication.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            CreateMap<Movie, MovieEntity>().ReverseMap();
            CreateMap<Auditorium, AuditoriumEntity>().ReverseMap();
            CreateMap<ImdbStatus, IMDBStatus>().ReverseMap();
            CreateMap<Showtime, ShowtimeEntity>().ReverseMap();
        }
    }
}
