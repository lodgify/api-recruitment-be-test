using ApiApplication.Database.Entities;
using ApiApplication.ImdbApi.Models;
using ApiApplication.Models;
using AutoMapper;
using System.Linq;

namespace ApiApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Showtime, ShowtimeEntity>()
                .ForMember(des => des.Schedule, opt => opt.MapFrom(src => src.Schedule.Split(",", System.StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<ShowtimeEntity, Showtime>()
                .ForMember(des => des.Schedule, opt => opt.MapFrom(src => string.Join(",", src.Schedule)));

            CreateMap<Movie, MovieEntity>()
                .ForMember(des => des.Stars, opt => opt.MapFrom(src => src.Starts))
                .ReverseMap();

            CreateMap<ImdbMovie, Movie>()
                .ForMember(des => des.ImdbId, opt => opt.Ignore());
        }
    }
}
