using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Exceptions
{
    public class UnkownException : CustomException
    {
        static string key = "Unknown error";

        public UnkownException() : base(key, key)
        {
        }

        public UnkownException(string description) : base(key, description)
        {
        }
    }
}
