﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WuJinAPI.Models;

namespace WuJinAPI.Services
{
    public class MappingService
    {
        private readonly IMongoCollection<Mapping> _Mapping;
        public MappingService()
        {

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("WuJinDB");
            _Mapping = database.GetCollection<Mapping>("mapping");
        }

        public List<Mapping> GetAll() =>
            _Mapping.Find(Mapping => true).ToList();
        public List<Mapping> GetHouseHold(string EstateNo)
        {
            var filterBuilder = Builders<Mapping>.Filter;
            FilterDefinition<Mapping> filter = filterBuilder.Eq("RealEstateNo", EstateNo);
            var mapping = _Mapping.Find(filter).ToList();
            return mapping;
        }
        public Mapping GetOne( string BuildingNo, string RoomId)
        {
            
            var filterBuilder = Builders<Mapping>.Filter;
           
           FilterDefinition<Mapping> filter = filterBuilder.Eq("RoomId", RoomId) & filterBuilder.Eq("BuildingNo", BuildingNo);
            var mapping = _Mapping.Find(filter).FirstOrDefault();

            // FilterDefinition<Mapping> filter = filterBuilder.Eq("RoomId", RoomId);
            //var mappingList = _Mapping.Find(filter).ToList();
            //var mapping = mappingList.Find(item => item.BuildingNo == BuildingNo);
            return mapping;
        }
    }
}
