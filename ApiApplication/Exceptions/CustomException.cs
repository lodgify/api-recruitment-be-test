using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Exceptions
{
    public class CustomException : Exception
    {
        public string Key { get; set; }

        public string Description { get; set; }

        public CustomException(string key, string description)
        {
            Key = key;
            Description = description;
        }
    }
}
