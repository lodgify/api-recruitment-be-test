using System;
using System.Threading.Tasks;


namespace ApiApplication
{
    public sealed class StatusImdb
    {
        private static readonly StatusImdb statusImdb = new StatusImdb();
        private static bool  Up { get; set; }
        private static DateTime LastCall { get; set; }

        public static StatusImdb Instance
        {
            get
            {
                return statusImdb;
            }
        }

        public static bool GetUpImdb()
        {
            return Up;
        }

        public static DateTime GetLastCall()
        {
            return LastCall;
        }

        public static void SetStatusImdb(bool up , DateTime lastCall)
        {
            Up = up;
            LastCall = lastCall;
        }
    }
}
