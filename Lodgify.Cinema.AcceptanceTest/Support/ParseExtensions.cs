using System;

namespace Lodgify.Cinema.AcceptanceTest.Core
{
    public static class ParseExtensions
    {
        public static DateTime ParseDateTimePtBr(this string str)
        {
            return DateTime.ParseExact(str, "dd/MM/yyyy HH:mm:ss", null);
        }
    }
}
