﻿using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Tags.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Service.Hangfire;
using System;

namespace SMS.API.MicrosoftExtensions
{
    public static class HangfireExtension
    {
        public static IServiceCollection AddCustomHangfireService(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHangfire((serviceProvider, hangfireConfiguration) =>
            {
                hangfireConfiguration.UseSqlServerStorage(Configuration.GetConnectionString("SmsSqlServer"));
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

        public static void UseCustomHangfire(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseHangfireDashboard();
        }
    }
}