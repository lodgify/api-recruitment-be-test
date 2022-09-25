using ApiApplication.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Extensions
{
    public static class MovieEntityExtension
    {
        public static MovieEntity Convert(this MovieEntity movieEntity, Dictionary<string, object> apiMovieData)
        {
            movieEntity.ImdbId = apiMovieData["id"].ToString();
            movieEntity.ReleaseDate = DateTime.Parse(apiMovieData["releaseDate"].ToString());
            movieEntity.Title = apiMovieData["title"].ToString();
            movieEntity.Stars = apiMovieData["stars"].ToString();

            return movieEntity;
        }
    }
}
