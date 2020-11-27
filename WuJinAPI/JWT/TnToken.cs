using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WuJinAPI.JWT
{
    public class TnToken
    {
        public string  TokenStr { get; set; }
        public DateTime Expires { get; set; }
    }
}
