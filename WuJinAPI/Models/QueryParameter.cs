﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WuJinAPI.Extensions;

namespace WuJinAPI.Models
{
    public class QueryParameter : PaginationBase
    {
        public string EstateUnitNo;
        public string HouseHoldeID;
        public string NatbuildNo;
        public string FloLayer;
        public string RoomId;
    }
}
