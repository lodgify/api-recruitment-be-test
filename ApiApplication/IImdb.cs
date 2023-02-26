using System.Net;

namespace ApiApplication
{
    public interface IImdb
    {
        public HttpStatusCode Status { get; set; }
    }
}
