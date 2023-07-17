using ApiApplication.Database.Entities;
using ApiApplication.DTO;
using ApiApplication.IMDb;
using AutoMapper;
using System;

namespace ApiApplication
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            MapDto();
            MapCommand();
        }

        private void MapDto()
        {
            CreateMap<TitleDataDto, MovieEntity>()
                 .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title))
                 .ForMember(d => d.Stars, opt => opt.MapFrom(src => src.Stars))
                 .ForMember(d => d.Id, opt => opt.MapFrom(src => 6))
                 .ForMember(x => x.ReleaseDate,
                                 opt => opt.MapFrom(src => (Convert.ToDateTime(src.ReleaseDate))));

        }
        private void MapCommand()
        {

            CreateMap<ShowtimeCommand, ShowtimeEntity>()
               .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(d => d.AuditoriumId, opt => opt.MapFrom(src => src.Auditorium_id))
               .ForMember(d => d.Movie, opt => opt.MapFrom(src => src.Movie))
               .ForMember(d => d.StartDate, opt => opt.MapFrom(src => src.Start_date))
               .ForMember(d => d.EndDate, opt => opt.MapFrom(src => src.End_date));


            CreateMap<MovieCommand, MovieEntity>()
               .ForMember(d => d.ImdbId, opt => opt.MapFrom(src => src.Imdb_id))
               .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(d => d.Stars, opt => opt.MapFrom(src => src.Starts))
               .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(src => src.Release_date));
        }

    }
}
