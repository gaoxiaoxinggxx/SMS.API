using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using SMS.API.MicrosoftExtensions;
using SMS.Base;
using SMS.Service.Hubs;

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
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            //注入：Swagger文档
            services.AddCustomSwaggerDocumentService(AppSettings);
            //注入：AddHttpContextAccessor
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            //注入：数据库上下文
            services.AddCustomDbContextService(Configuration);
            //注入：跨域服务
            services.AddCustomCorsService(AppSettings);
            //注入：AutoMapper
            services.AddCustomAutoMapperService();
            //注入：业务服务注入
            services.AddInjectDependencies(AppSettings);
            //注入：鉴权认证策略
            services.AddCustomAuth(AppSettings);
            //注入：Hangfire定时任务
            services.AddCustomHangfireService(Configuration);
            //注入：ApiService,Refit.HttpClientFactory
            //注入：SignalR
            services.AddSignalR(x => { x.EnableDetailedErrors = true; });
            //services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
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
            //跨域管道
            app.UseCustomCors(AppSettings);
            //hangfire管道
            app.UseCustomHangfire();
            //鉴权授权管道
            app.UseCustomAuth();

            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
