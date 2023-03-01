namespace ApiApplication.Models.ViewModels
{
    public class CriteriaImdb
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public string BaseAPIPath { get; set; }
        public string Query { get; set; }
        public CriteriaImdb(string id, string language = "en", string baseAPIPath = null, string query = null)
        {
            Id = id;
            Language = language;
            BaseAPIPath = baseAPIPath;
            Query = query;
        }
    }
}
