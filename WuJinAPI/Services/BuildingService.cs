using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WuJinAPI.Repository;
using WuJinAPI.Context;
using WuJinAPI.Models;
using MongoDB.Driver;
using WuJinAPI.Extensions;
namespace WuJinAPI.Services
{
    public class BuildingService
    {
        private readonly IMongoCollection<Building> _Building;
        public BuildingService()
        {

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("WuJinDB");
            _Building = database.GetCollection<Building>("buildings");
        }
        /// <summary>
        /// 查找所有的不动产
        /// </summary>
        /// <returns>不动产列表</returns>
        public List<Building> GetAll() =>
             _Building.Find(building => true).ToList();

        public List<Building> FindList(string[] field = null)
        {
            try
            {
                var fieldList = new List<ProjectionDefinition<Building>>();
                foreach (var item in field)
                {
                    fieldList.Add(Builders<Building>.Projection.Include(item.ToString()));
                }
                var projection = Builders<Building>.Projection.Combine(fieldList);
                fieldList?.Clear();

                return _Building.Find(build => true).Project<Building>(projection).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据查询参数获取某个建筑物
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Building GetOneRealEstate(QueryParameter query)
        {
            FilterDefinition<Building> filter;//查询表达式
            var filterBuilder = Builders<Building>.Filter;//查询构建器
            // filter = filterBuilder.Eq("EstateUnitNo", query.NatbuildNo) & filterBuilder.Eq("RoomId", query.RoomId) & filterBuilder.Eq("FloLayerId", query.FloLayer);
            filter = filterBuilder.Eq("EstateUnitNo", query.EstateNo);
            var building = _Building.Find(filter).FirstOrDefault();
            return building;
        }

        public PaginatedList<Building> GetBuildings(QueryParameter parameter)
        {
            var query = _Building.AsQueryable().OrderBy(x => x.EstateUnitNo);
            var count = query.Count();

            var buildings = query
                .Skip(parameter.PageIndex * parameter.PageIndex)
                .Take(parameter.PageSize)

                .ToList();
            return new PaginatedList<Building>(parameter.PageIndex, parameter.PageSize, count, buildings);
        }
        public Building Get(string EstateUnitNo)
        {
            return _Building.Find<Building>(building => building.EstateUnitNo == EstateUnitNo).FirstOrDefault();
        }

        /// <summary>
        /// 新建一个建筑物
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public Building Create(Building building)
        {
            _Building.InsertOne(building);
            return building;
        }
        /// <summary>
        ///上传多个信息
        /// </summary>
        /// <param name="buildings"></param>
        public void CreateMany(IEnumerable<Building> buildings)
        {
            _Building.InsertMany(buildings);
        }
        public void Update(string EstateUnitNo, Building building) =>
            _Building.ReplaceOne(building => building.EstateUnitNo == EstateUnitNo, building);
        public void Remove(Building building) =>
            _Building.DeleteOne(building => building.EstateUnitNo == building.EstateUnitNo);
        public void Remove(string EstateUnitNo) =>
            _Building.DeleteOne(building => building.EstateUnitNo == EstateUnitNo);

    }
    //    public BuildingsContext BuildingsContext;
    //    public BuildingService(BuildingsContext buildingsContext)
    //    {
    //        this.BuildingsContext = buildingsContext;
    //    }

    //    public bool CreateBuilding(Building building)
    //    {
    //        BuildingsContext.Buildings.Add(building);
    //        return BuildingsContext.SaveChanges() > 0;
    //    }
    //    /// <summary>
    //    /// 根据不动产编号删除建筑物
    //    /// </summary>
    //    /// <param name="EstateNo"></param>
    //    /// <returns></returns>
    //    public bool DeleteBuildingByEstateNo(string EstateNo)
    //    {
    //        var building = BuildingsContext.Buildings.SingleOrDefault(building => building.EstateUnitNo == EstateNo);
    //        BuildingsContext.Buildings.Remove(building);
    //        return BuildingsContext.SaveChanges() > 0;
    //    }

    //    public Building GetBuildingByEstateNo(string EstateNo)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<Building> GetBuildings()
    //    {
    //        return BuildingsContext.Buildings.ToList();
    //    }

    //    public bool UpdateBuilding(Building building)
    //    {
    //        BuildingsContext.Buildings.Update(building);
    //        return BuildingsContext.SaveChanges() > 0;
    //    }

    //    public bool UpdateNameByEstateNo(string EstateNo, string PlanUsage)
    //    {
    //        bool state = false;
    //        var findedbuilding = BuildingsContext.Buildings.SingleOrDefault(b => b.EstateUnitNo == EstateNo);
    //        if (findedbuilding != null)
    //        {
    //            findedbuilding.PlanUsage = PlanUsage;
    //            state = BuildingsContext.SaveChanges() > 0;
    //        }
    //        return state;
    //    }

}
