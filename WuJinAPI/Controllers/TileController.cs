﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.RegularExpressions;
using WuJinAPI.Models;
using WuJinAPI.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WuJinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TileController : ControllerBase
    {
        private readonly TileService _tileService;
        public TileController(TileService tileService)
        {
            _tileService = tileService;
        }
        /// <summary>
        /// 获取影像
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{x}/{y}/{z}")]
        public IActionResult Get(string x, string y, string z)
        {
            var tile = _tileService.Get(x, y, z);
            if (tile == null)
            {
                return NotFound();
            }
            return tile;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm(Name = "files")] IFormFile imgs)
        {
            if (imgs == null)
            {
                return NotFound();
            }

            Tile tile = new Tile() { };
            var fileInfo = imgs.ContentDisposition;
            var pattern = @"\d+(?=.)";

            List<string> index = new List<string>();
            string fullName = "";
            foreach (Match match in Regex.Matches(fileInfo, pattern))
            {
                fullName = match.Value;
                index.Add(fullName);
            }

            if (index.Count < 3)
            {
                return Ok("输入文件夹层级错误");
            }
            else
            {
                tile.z = index[index.Count - 1];
                tile.y = index[index.Count - 2];
                tile.x = index[index.Count - 3];
            }
            BinaryReader binaryReader = new BinaryReader(imgs.OpenReadStream());
            tile.img = binaryReader.ReadBytes(Convert.ToInt32(imgs.Length));
            return Ok(await _tileService.Create(tile));

        }

    }
}
