using Hangfire;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SMS.Service.Hangfire;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace SMS.API
{
    public static class StartupServiceBuilders
    {
        #region AddHangfireService
        public static IServiceCollection AddHangfireService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHangfire((serviceProvider, hangfireConfiguration) =>
            {
                hangfireConfiguration.UseSqlServerStorage(configuration.GetConnectionString("SchedulerSqlServer"));
                GlobalJobFilters.Filters.Clear();
                hangfireConfiguration.UseFilter(new CaptureCultureAttribute());
                //hangfireConfiguration.UseFilter(new SchedulerRetryFilterAttribute { Attempts = 5 });
                hangfireConfiguration.UseFilter(new StatisticsHistoryAttribute());
                hangfireConfiguration.UseFilter(new MyContinuationsSupportAttribute(true));
            });

            services.AddHangfireServer(x =>
            {
                x.Queues = new[] { "default", "change_status" };
                x.SchedulePollingInterval = TimeSpan.FromSeconds(2);
            });
            return services;
        }

        #endregion
        #region AddSwaggerService

        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Schedule Manage Syseam API", Version = "v1" });

                c.UseAllOfForInheritance();
                //c.EnableAnnotations(true, true);
                //c.DocumentFilter<LowercaseDocumentFilter>();
                c.OperationFilter<SwaggerOperationNameFilter>();
               
                //var filePath = Path.Combine(AppContext.BaseDirectory, "SMS.Model.xml");
                //c.IncludeXmlComments(filePath);

                c.DescribeAllParametersInCamelCase();
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public class SwaggerOperationNameFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var actionDescriptor = (ControllerActionDescriptor)context.ApiDescription.ActionDescriptor;
                operation.OperationId = $"{actionDescriptor.ControllerName}_{actionDescriptor.ActionName}";
            }
        }

        #endregion
    }
}
