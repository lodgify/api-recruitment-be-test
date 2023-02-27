namespace ApiApplication.Application.Querie
{
    public class FilteredRequest
    {
        public long LastSince { get; set; }

        public long Since { get; private set; }
    }
}
