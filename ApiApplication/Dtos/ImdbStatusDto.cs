using System;

namespace ApiApplication.Dtos
{
    public class ImdbStatusDto
    {
        public bool Up { get; set; }
        public DateTime Last_call { get; set; }

        public ImdbStatusDto(bool up, DateTime last_call)
        {
            Up = up;
            Last_call = last_call;
        }

     }
}
