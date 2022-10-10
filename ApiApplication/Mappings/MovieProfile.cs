using ApiApplication.Database.Entities;
using ApiApplication.Models;
using AutoMapper;

namespace ApiApplication.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile() {
            CreateMap<MovieEntity, Movie>()
                .ReverseMap();
            CreateMap<Movie, ImdbService.Models.Movie>()
                .ReverseMap();
        }
    }
}
