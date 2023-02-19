using System;
using ApiApplication.Resources;

namespace ApiApplication.Tests.Shared
{
    public sealed class ImdbTitleMother
    {
        public static ImdbTitleResponse Create(string imdbId)
        {
            return new ImdbTitleResponse()
            {
                Id = imdbId
            };
        }
    }
}

