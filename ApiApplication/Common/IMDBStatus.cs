using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Common
{
    public class IMDBStatus
    {
        private static readonly IMDBStatus instance = new IMDBStatus();

        public bool Up { get; set; }

        public DateTime LastCall { get; set; }

        static IMDBStatus()
        {
        }
        private IMDBStatus()
        {
        }
        public static IMDBStatus Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
