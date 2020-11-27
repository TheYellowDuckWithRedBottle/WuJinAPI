﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WuJinAPI.Models;
using WuJinAPI.Services;
namespace WuJinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RealEstateController : ControllerBase
    {
        private BuildingService BuildingService;
        public RealEstateController(BuildingService buildingService)
        {
            BuildingService = buildingService;
        }
        /// <summary>
        /// 获取所有不动产信息
        /// </summary>
        /// <returns>所有的不动产信息</returns>
        [HttpGet]
        public List<Building> GetBuildings([FromQuery] QueryParameter parameter)
        {
            return BuildingService.GetBuildings(parameter);

        }
        /// <summary>
        /// 根据栋号和房间号获取不动产信息
        /// </summary>
        /// <param name="NatbuildNo">栋号</param>
        /// <param name="RoomId">房间号</param>
        /// <returns>不动产信息</returns>
        [HttpGet]
        public ActionResult GetRealEstate([FromQuery] string NatbuildNo, [FromQuery] string RoomId)
        {
            MappingService mapping = new MappingService();
            var mappingObj= mapping.GetOne(NatbuildNo, RoomId);
            if (mappingObj == null) return NotFound();
           
            QueryParameter queryParameter = new QueryParameter() {EstateNo= mappingObj.EstateUnitNo };
            var building = BuildingService.GetOneRealEstate(queryParameter);
            if (building == null) return NotFound();
            else return Ok(building);
        }
     }
}
