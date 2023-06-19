using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Dtos
{
    public class ImdbStatusDto
    {
        public bool Up { get; set; }

        public DateTime LastCall { get; set; }
    }
}
