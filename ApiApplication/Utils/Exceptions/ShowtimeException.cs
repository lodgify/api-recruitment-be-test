using System;

namespace ApiApplication.Utils.Exceptions
{
    public class ShowtimeException : Exception
    {
        public ShowtimeException(string message) 
            : base(message)
        {
        }
    }
}
