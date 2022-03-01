using AutoMapper;
using ApiApplication.Database.Entities;
using ApiApplication.Dtos;
using Newtonsoft.Json.Linq;
using System;

namespace ApiApplication.Database
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ShowtimeEntity, ShowtimeDto>()
                .ForMember(dest =>
                    dest.Start_date,
                    opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest =>
                    dest.End_date,
                    opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest =>
                    dest.Auditorium_id,
                    opt => opt.MapFrom(src => src.AuditoriumId)).ReverseMap();
            CreateMap<MovieEntity, MovieDto>()
                .ForMember(dest =>
                    dest.Imdb_id,
                    opt => opt.MapFrom(src => src.ImdbId))
                .ForMember(dest =>
                    dest.Release_date,
                    opt => opt.MapFrom(src => src.ReleaseDate))
                .ReverseMap();
            CreateMap<AuditoriumEntity, AuditoriumDto>().ReverseMap();
            CreateMap<JObject, MovieDto>()
                .ForMember(x => x.Imdb_id, y => y.MapFrom(j => j[".id"]))
                .ForMember(x => x.Title, y => y.MapFrom(j => j[".title"]))
                .ForMember(x => x.Stars, y => y.MapFrom(j => j[".stars"]))
                .ForMember(x => x.Release_date, y => y.MapFrom(j => Convert.ToDateTime(j[".releaseDate"])));
        }
    }
}
