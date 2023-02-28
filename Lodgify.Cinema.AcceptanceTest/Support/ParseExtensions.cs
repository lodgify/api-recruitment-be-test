using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodgify.Cinema.AcceptanceTest.Core
{
    public static class ParseExtensions
    {
        public static DateTime ParseDateTimePtBr(this string str) 
        {
            return DateTime.ParseExact(str, "dd/MM/yyyy HH:mm:ss",null);
        }
    }
}
