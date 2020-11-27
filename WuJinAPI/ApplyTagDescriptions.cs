using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WuJinAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplyTagDescriptions : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                //new OpenApiTag{Name="Login",Description="获取登录token"},
                new OpenApiTag{Name="Building",Description="获取不动产相关数据"},
                new OpenApiTag{Name="User",Description="获取用户信息"},
                new OpenApiTag{Name="Tile",Description="获取影像信息"},
            };


        }
    }
}
