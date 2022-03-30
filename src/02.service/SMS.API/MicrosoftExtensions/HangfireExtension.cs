using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Tags.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.API.TokenValidator;
using SMS.Service.Hangfire;
using System;
using System.Data;

namespace SMS.API.MicrosoftExtensions
{
    public static class HangfireExtension
    {
        // https://www.cnblogs.com/netlock/p/14097300.html
        public static IServiceCollection AddCustomHangfireService(this IServiceCollection services, IConfiguration Configuration)
        {
            var storage = new SqlServerStorage(Configuration.GetConnectionString("SmsSqlServer")
                  , new SqlServerStorageOptions { PrepareSchemaIfNecessary = true, SchemaName= "Sms" });
            //GlobalConfiguration.Configuration.UseStorage(new SqlServerStorage(storage, new SqlServerStorageOptions
            //{
            //    //TransactionIsolationLevel = IsolationLevel.ReadCommitted, // 事务隔离级别。默认值为读提交。
            //    QueuePollInterval = TimeSpan.FromSeconds(15),             // 作业队列轮询间隔。默认值为15秒
            //    JobExpirationCheckInterval = TimeSpan.FromHours(1),       // 作业过期检查间隔（管理过期记录）。默认为1小时
            //    CountersAggregateInterval = TimeSpan.FromMinutes(5),      // 间隔到聚合计数器。默认为5分钟
            //    PrepareSchemaIfNecessary = true,                          // 如果设置为true，则创建数据库表。默认值为true
            //    DashboardJobListLimit = 50000,                            // 仪表板作业列表上限。默认值为50000 
            //    TransactionTimeout = TimeSpan.FromMinutes(1),             // 事务超时。默认为1分钟
            //}));
            services.AddHangfire(p => p.UseStorage(storage));
            return services;
        }

        public static void UseCustomHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
                IgnoreAntiforgeryToken = true,
                AppPath = "/swagger/index.html",
                DashboardTitle = "Schedule Manage System"
            });
            app.UseHangfireDashboard();
        }
    }
}
