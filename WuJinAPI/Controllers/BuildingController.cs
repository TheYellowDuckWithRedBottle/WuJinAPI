using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WuJinAPI.Models;
using WuJinAPI.Context;
using WuJinAPI.Services;
namespace WuJinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingController : ControllerBase
    {
        private BuildingService BuildingService;
        public BuildingController(BuildingService buildingService)
        {
            BuildingService = buildingService;
        }
        [HttpGet]
        public List<Building> Get()
        {
            return BuildingService.GetAll();

        }
        [HttpGet]
        public ActionResult GetBuilding([FromQuery] string BuildNo, [FromQuery] string RoomId)
        {
            QueryParameter queryParameter = new QueryParameter() {RoomId=RoomId,NatbuildNo=BuildNo };
            var building = BuildingService.GetOneRealEstate(queryParameter);
            if (building == null) return NotFound();
            else return Ok(building);
        }
     }
}
