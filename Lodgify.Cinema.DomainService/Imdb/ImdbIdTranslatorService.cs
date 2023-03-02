using Lodgify.Cinema.Domain.Contract;
using System;

namespace Lodgify.Cinema.DomainService.Imdb
{
    public sealed class ImdbIdTranslatorService : IImdbIdTranslatorService
    {
        public string ImdbPrefixId { get; private set; } = "tt";

        public string Get(int id) => $"{ImdbPrefixId}{id}";

        public int Get(string id) => Convert.ToInt32(id.Replace(ImdbPrefixId, string.Empty));
    }
}