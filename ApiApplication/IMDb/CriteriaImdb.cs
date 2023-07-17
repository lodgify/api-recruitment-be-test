namespace ApiApplication.IMDb
{
    public class CriteriaImdb
    {
        public string Id { get; set; }
        public string Language { get; set; }

        public CriteriaImdb(string id, string language = "en")
        {
            Id = id;
            Language = language; 

        }

    }
}
