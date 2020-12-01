using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using WuJinAPI.JWT;
using WuJinAPI.Services;

namespace WuJinAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1.1", new OpenApiInfo
            //    {
            //        Version = "v1.1",
            //        Title = "测试接口文档",
            //        Description = "测试接口"
            //    });
            //    // 为 Swagger 设置xml文档注释路径
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //    c.DocInclusionPredicate((docName, description) => true);
            //    //添加对控制器的标签(描述)
            //    c.DocumentFilter<ApplyTagDescriptions>();//显示类名
            //    c.CustomSchemaIds(type => type.FullName);// 可以解决相同类名会报错的问题
            //    //c.OperationFilter<AuthTokenHeaderParameter>();
            //});
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddTransient<ITokenHelper, TokenHelper>();
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));
            // services.AddDbContext<BuildingsContext>(options => options(Configuration.GetConnectionString("BuildingConnection")));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");
                });
            });
            services.AddSingleton<TileService>();
            services.AddSingleton<BuildingService>();
   
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
           
            app.UseRouting();
            //app.UseSwagger(c =>
            //{
            //    c.RouteTemplate = "swagger/{documentName}/swagger.json";
            //});
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "Web App v1.1");
            //    c.RoutePrefix = string.Empty;//设置根节点访问
            //    c.DocExpansion(DocExpansion.None);//折叠
            //    c.DefaultModelsExpandDepth(-1);//不显示Schemas
            //});
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();
        }
    }
}
