using System.Collections.Generic;

namespace ApiApplication.IMDb
{
    public class TitleDataDto
    {
        public string Id { get; set; }
        public string Title { set; get; }
        public string OriginalTitle { get; set; }
        public string FullTitle { set; get; }
        public string Year { set; get; }
        public string ReleaseDate { set; get; }
        public string RuntimeMins { set; get; }
        public string RuntimeStr { set; get; }
        public string Plot { set; get; } // IMDb Plot allways en, TMDb Plot translate
        public string PlotLocal { set; get; }
        public bool PlotLocalIsRtl { set; get; }
        public string Awards { set; get; }
        public string Image { get; set; }
        public string Type { set; get; }
        public string Directors { set; get; }   
        public string Writers { set; get; }      
        public string Stars { set; get; }   
        public string Genres { set; get; }      
        public string Companies { get; set; }   
        public string Countries { set; get; } 
        public string Languages { set; get; }   
        public string ContentRating { get; set; }
        public string IMDbRating { get; set; }
        public string IMDbRatingVotes { get; set; }
        public string MetacriticRating { set; get; }
        public string Tagline { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordList { get; set; }    
        public string ErrorMessage { get; set; }
    }

    
}
