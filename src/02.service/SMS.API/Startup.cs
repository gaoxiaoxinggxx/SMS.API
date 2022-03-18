using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SMS.API.MicrosoftExtensions;
using SMS.Base;
using SMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AppSettings AppSettings { get; } = new();
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注入：配置文件信息
            Configuration.Bind("AppSettings", AppSettings);
            //注入：控制器
            services.AddControllers();
            //注入：Swagger文档
            services.AddCustomSwaggerDocumentService(AppSettings);
            //注入：AddHttpContextAccessor
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            //注入：数据库上下文
            services.AddDbContextFactory<SmsDbContext>(builder => builder.UseSqlServer(Configuration.GetConnectionString("SmsSqlServer")));
            //注入：跨域服务
            //注入：AutoMapper
            //注入：业务服务注入
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
                app.UseCustomSwagger(AppSettings);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
