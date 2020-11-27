using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace WuJinAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Building
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("房地号")]
        public string LandNo { get; set; }
       
        [JsonProperty("房屋坐落")]
        public string Location { get; set; }
        [JsonProperty("公安幢号")]
        public string PublicSecBuildingNo { get; set; }
        [JsonProperty("房号")]
        public string ApartmentNo { get; set; }
        [JsonProperty("总层数")]
        public string TotalLayer{ get; set; }
        [JsonProperty("所在层")]
        public string Layer { get; set; }
        [JsonProperty("规划用途")]
        public string PlanUsage { get; set; }
        [JsonProperty("结构")]
        public string structure { get; set; }
        [JsonProperty("建筑面积")]
        public double Area { get; set; }
        [JsonProperty("套内建筑面积")]
        public double internalBuildingArea  { get; set; }
        [JsonProperty("丘号")]
        public string addressNo  { get; set; }
        [JsonProperty("幢号")]
        public string BuildingNo { get; set; }
        [JsonProperty("ROOMCODE")]
        public string RoomCode   { get; set; }
        [JsonProperty("不动产单元号")]
        public string EstateUnitNo   { get; set; }
    }
}
