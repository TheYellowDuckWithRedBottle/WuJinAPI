using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WuJinAPI.Models;
namespace WuJinAPI.Repository
{
   public interface IBuildingRepository
    {
        bool CreateBuilding(Building building);

        IEnumerable<Building> GetBuildings();

        Building GetBuildingByEstateNo(string EstateNo);

        bool UpdateBuilding(Building building);
        bool UpdateNameByEstateNo(string EstateNo, string usage);

        bool DeleteBuildingByEstateNo(string EstateNo);
    }
}
