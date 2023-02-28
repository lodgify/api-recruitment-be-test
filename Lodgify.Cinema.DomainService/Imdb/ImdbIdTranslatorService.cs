using Lodgify.Cinema.Domain.Contract;
using System;

namespace Lodgify.Cinema.DomainService.Imdb
{
    public sealed class ImdbIdTranslatorService : IImdbIdTranslatorService
    {
        private const string IMDB_ID_PREFIX = "tt";

        public string Get(int id) => $"{IMDB_ID_PREFIX}{id}";

        public int Get(string id) => Convert.ToInt32(id.Replace(IMDB_ID_PREFIX,string.Empty));
    }
}