using System.Net;

namespace ApiApplication.ImdbApi
{
    public class StatusInfo : IStatusInfo
    {
        private HttpStatusCode _statusId;

        public HttpStatusCode GetStatus()
        {
            return _statusId;
        }

        public void SetStatus(HttpStatusCode statusId)
        {
            _statusId = statusId;
        }
    }
}
