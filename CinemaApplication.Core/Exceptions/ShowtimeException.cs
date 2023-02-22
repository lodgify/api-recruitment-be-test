using System;

namespace CinemaApplication.Core.Exceptions
{
    public class ShowtimeException : Exception
    {
        public ShowtimeException(string message)
            : base(message)
        {
        }
    }
}
