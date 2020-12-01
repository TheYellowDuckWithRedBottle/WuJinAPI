using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WuJinAPI.Resources
{
    public class BuildingResources
    {
        [JsonProperty("不动产单元号")]
        public string EstateUnitNo { get; set; } = null;

        [JsonProperty("房地号")]
        public string LandNo { get; set; } = null;

        [JsonProperty("房屋坐落")]
        public string Location { get; set; } = null;
        [JsonProperty("公安幢号")]
        public string PublicSecBuildingNo { get; set; } = null;
        [JsonProperty("房号")]
        public string ApartmentNo { get; set; } = null;
        [JsonProperty("总层数")]
        public string TotalLayer { get; set; } = null;
        [JsonProperty("所在层")]
        public string Layer { get; set; } = null;
        [JsonProperty("规划用途")]
        public string PlanUsage { get; set; } = null;
        [JsonProperty("结构")]
        public string structure { get; set; } = null;
        [JsonProperty("建筑面积")]
        public double Area { get; set; } = 0;
        [JsonProperty("套内建筑面积")]
        public double internalBuildingArea { get; set; } = 0;
        [JsonProperty("丘号")]
        public string addressNo { get; set; } = null;
        [JsonProperty("幢号")]
        public string BuildingNo { get; set; } = null;
        [JsonProperty("ROOMCODE")]
        public string RoomCode { get; set; }
    }
}
