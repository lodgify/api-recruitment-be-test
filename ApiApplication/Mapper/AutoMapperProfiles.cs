using ApiApplication.Database.Entities;
using ApiApplication.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Showtime, ShowtimeEntity>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie))
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => MapSchedule(src.Schedule)))
                .ForMember(dest => dest.AuditoriumId, opt => opt.MapFrom(src => src.AuditoriumId));

            CreateMap<Movie, MovieEntity>()                
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.ImdbId))                
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))                
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars));


            CreateMap<MovieEntity, Movie>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.ImdbId))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars));
        }

        private IEnumerable<string> MapSchedule(string schedule)
        {
            if (string.IsNullOrEmpty(schedule))
                return Enumerable.Empty<string>();

            return schedule.Split(", ");
        }
    }
}
