using System;

namespace ApiApplication.Services
{
    public class ImdbStatusService
    {
        public bool Up { get; set; }
        public DateTime Last_call { get; set; }

        public ImdbStatusService()
        {
            Up = false;
            Last_call = DateTime.UtcNow;
        }
        public ImdbStatusService(bool up, DateTime last_call)
        {
            Up = up;
            Last_call = last_call;
        }
    }
}
