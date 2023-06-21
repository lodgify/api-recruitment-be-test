using System.Net;

namespace ApiApplication.ImdbApi
{
    public interface IStatusInfo
    {
        HttpStatusCode GetStatus();

        void SetStatus(HttpStatusCode statusId);
    }
}
