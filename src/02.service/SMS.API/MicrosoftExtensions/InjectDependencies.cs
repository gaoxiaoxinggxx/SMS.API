using Common.VNextFramework.EntityFramework;
using Common.VNextFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMS.Base;
using SMS.Data;

namespace SMS.API.MicrosoftExtensions
{
    public static class InjectDependencies
    {
        public static IServiceCollection AddInjectDependencies(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddHttpContextAccessor();
            

            services.AddScopedScan("SMS.Service");
            return services;
        }
    }
}
