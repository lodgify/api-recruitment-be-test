using System;

namespace Cinema.Entities.DTOs
{
    public sealed class ImdbStatus
    {
        private static readonly Lazy<ImdbStatus> lazy =
      new Lazy<ImdbStatus>(() => new ImdbStatus());

        public bool Up { get; set; }

        public DateTime LastCall { get; set; }

        public static ImdbStatus Instance { get { return lazy.Value; } }

        private ImdbStatus()
        {
        }
    }
}
