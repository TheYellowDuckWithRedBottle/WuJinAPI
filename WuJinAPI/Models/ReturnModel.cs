using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WuJinAPI.JWT;

namespace WuJinAPI.Models
{
    public class ReturnModel
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
        public TnToken  TnToken { get; set; }
    }
}
