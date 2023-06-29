using System;

namespace ApiApplication.Models
{
    public class ImdbStatus
    {
        public bool Up { get; set; } = false;
        public DateTime? LastCall { get; set; } = null;
    }
}
