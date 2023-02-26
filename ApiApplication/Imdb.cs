using System.Net;

namespace ApiApplication
{
    public class Imdb : IImdb
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
    }
}
