using Common.VNextFramework.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SMS.Base;

namespace SMS.API.MicrosoftExtensions
{
    public static class InjectDependencies
    {
        public static IServiceCollection AddInjectDependencies(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddHttpContextAccessor();
            //services.AddScopedScan("SMS.Service");
            //services.AddScopedScan("SMS.Repository");
            //services.AddScopedScan("SMS.Manager");
            return services;
        }
    }
}
