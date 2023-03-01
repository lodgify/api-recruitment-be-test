using System;
using System.Text.Json.Serialization;

namespace ApiApplication.Dtos
{
    public class IMDBStatus
    {
        private static IMDBStatus _instance;
        private static object synLock = new object();
        private static object setSyncLock = new object();

        private bool _up;
        private DateTime _lastCall;

        [JsonPropertyName("up")]
        public bool Up { get { return _up; } }

        [JsonPropertyName("last_call")]
        public DateTime LastCall { get { return _lastCall; } }

        public static IMDBStatus Instance()
        {
            if (_instance == null)
            {
                lock (synLock)
                {
                    if (_instance == null)
                    {
                        _instance = new IMDBStatus();
                    }
                }                
            }

            return _instance;
        }

        public static void SetValues(bool up, DateTime lastCall)
        {
            if (_instance == null)
            {
                lock (setSyncLock)
                {
                    if (_instance == null)
                    {
                        var o = Instance();

                        o._up = up;
                        o._lastCall = lastCall;
                    }
                }


            }
            else
            {
                _instance._up = true;
                _instance._lastCall = lastCall;
            }
        }
    }
}