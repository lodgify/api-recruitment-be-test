using ApiApplication.Database.Entities;
using ApiApplication.Models.Movies;
using ApiApplication.Models.Showtimes;
using AutoMapper;

namespace ApiApplication.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShowtimeModel, ShowtimeEntity>().ReverseMap();
            CreateMap<AddShowtimeModel, ShowtimeEntity>().ReverseMap();

            CreateMap<MovieModel, MovieEntity>().ReverseMap();
            CreateMap<AddMovieModel, MovieEntity>().ReverseMap();
        }
    }
}
