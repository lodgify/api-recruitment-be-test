using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Exceptions
{
    public class NotExistsException : CustomException
    {
        static string key = "Record does not exist";

        public NotExistsException() : base(key, key)
        {
        }

        public NotExistsException(string description) : base(key, description)
        {
        }
    }
}
