using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Generated using: https://json2csharp.com/
namespace ApiApplication.Resources
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ActorList
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("asCharacter")]
        public string AsCharacter { get; set; }
    }

    public class BoxOffice
    {
        [JsonPropertyName("budget")]
        public string Budget { get; set; }

        [JsonPropertyName("openingWeekendUSA")]
        public string OpeningWeekendUSA { get; set; }

        [JsonPropertyName("grossUSA")]
        public string GrossUSA { get; set; }

        [JsonPropertyName("cumulativeWorldwideGross")]
        public string CumulativeWorldwideGross { get; set; }
    }

    public class CompanyList
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class CountryList
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class CreatorList
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class GenreList
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class LanguageList
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class ImdbTitleResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("originalTitle")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("fullTitle")]
        public string FullTitle { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("runtimeMins")]
        public object RuntimeMins { get; set; }

        [JsonPropertyName("runtimeStr")]
        public object RuntimeStr { get; set; }

        [JsonPropertyName("plot")]
        public string Plot { get; set; }

        [JsonPropertyName("plotLocal")]
        public string PlotLocal { get; set; }

        [JsonPropertyName("plotLocalIsRtl")]
        public bool PlotLocalIsRtl { get; set; }

        [JsonPropertyName("awards")]
        public string Awards { get; set; }

        [JsonPropertyName("directors")]
        public string Directors { get; set; }

        [JsonPropertyName("directorList")]
        public List<object> DirectorList { get; set; }

        [JsonPropertyName("writers")]
        public string Writers { get; set; }

        [JsonPropertyName("writerList")]
        public List<object> WriterList { get; set; }

        [JsonPropertyName("stars")]
        public string Stars { get; set; }

        [JsonPropertyName("starList")]
        public List<StarList> StarList { get; set; }

        [JsonPropertyName("actorList")]
        public List<ActorList> ActorList { get; set; }

        [JsonPropertyName("fullCast")]
        public object FullCast { get; set; }

        [JsonPropertyName("genres")]
        public string Genres { get; set; }

        [JsonPropertyName("genreList")]
        public List<GenreList> GenreList { get; set; }

        [JsonPropertyName("companies")]
        public string Companies { get; set; }

        [JsonPropertyName("companyList")]
        public List<CompanyList> CompanyList { get; set; }

        [JsonPropertyName("countries")]
        public string Countries { get; set; }

        [JsonPropertyName("countryList")]
        public List<CountryList> CountryList { get; set; }

        [JsonPropertyName("languages")]
        public string Languages { get; set; }

        [JsonPropertyName("languageList")]
        public List<LanguageList> LanguageList { get; set; }

        [JsonPropertyName("contentRating")]
        public string ContentRating { get; set; }

        [JsonPropertyName("imDbRating")]
        public string ImDbRating { get; set; }

        [JsonPropertyName("imDbRatingVotes")]
        public string ImDbRatingVotes { get; set; }

        [JsonPropertyName("metacriticRating")]
        public string MetacriticRating { get; set; }

        [JsonPropertyName("ratings")]
        public object Ratings { get; set; }

        [JsonPropertyName("wikipedia")]
        public object Wikipedia { get; set; }

        [JsonPropertyName("posters")]
        public object Posters { get; set; }

        [JsonPropertyName("images")]
        public object Images { get; set; }

        [JsonPropertyName("trailer")]
        public object Trailer { get; set; }

        [JsonPropertyName("boxOffice")]
        public BoxOffice BoxOffice { get; set; }

        [JsonPropertyName("tagline")]
        public object Tagline { get; set; }

        [JsonPropertyName("keywords")]
        public string Keywords { get; set; }

        [JsonPropertyName("keywordList")]
        public List<string> KeywordList { get; set; }

        [JsonPropertyName("similars")]
        public List<Similar> Similars { get; set; }

        [JsonPropertyName("tvSeriesInfo")]
        public TvSeriesInfo TvSeriesInfo { get; set; }

        [JsonPropertyName("tvEpisodeInfo")]
        public object TvEpisodeInfo { get; set; }

        [JsonPropertyName("errorMessage")]
        public object ErrorMessage { get; set; }
    }

    public class Similar
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("imDbRating")]
        public string ImDbRating { get; set; }
    }

    public class StarList
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class TvSeriesInfo
    {
        [JsonPropertyName("yearEnd")]
        public string YearEnd { get; set; }

        [JsonPropertyName("creators")]
        public string Creators { get; set; }

        [JsonPropertyName("creatorList")]
        public List<CreatorList> CreatorList { get; set; }

        [JsonPropertyName("seasons")]
        public List<string> Seasons { get; set; }
    }


}

