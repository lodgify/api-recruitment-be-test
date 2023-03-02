namespace Lodgify.Cinema.Domain.Dto
{
    public sealed partial class ImdbRepository
    {
        public sealed class MovieResponse
        {
            public string id { get; set; }
            public string title { get; set; }
            public decimal? rating { get; set; }
            public short? release_year { get; set; }
            public short? popularity { get; set; }
            public string imdb_type { get; set; }
        }


    }
}
