using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taleem.Common
{
    public class AppSettings
    {  
        public string emailIsTest { get; set; }
        public string emailHost { get; set; }
        public string emailUsername { get; set; }
        public string emailPassword { get; set; }
        public string emailPort { get; set; }
        public string emailEnableSSL { get; set; }
        public string emailFrom { get; set; }
        public string emailDev { get; set; } 
    }
}
