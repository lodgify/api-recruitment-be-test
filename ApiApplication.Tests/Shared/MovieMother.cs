using System;
using ApiApplication.Resources;

namespace ApiApplication.Tests.Shared
{
    public sealed class MovieMother
    {
        public static Movie Create(string imdbId, string Title)
        {
            return new Movie()
            {
                ImdbId = imdbId,
                Stars = "Enrique",
                ReleaseDate = DateTime.Now.AddYears(-4).ToString(),
                Title = Title
            };
        }
    }
}

