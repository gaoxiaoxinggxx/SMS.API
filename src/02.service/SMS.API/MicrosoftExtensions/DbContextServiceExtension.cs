using Common.VNextFramework.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Base;
using SMS.Data;

namespace SMS.API.MicrosoftExtensions
{
    public static class DbContextServiceExtension
    {
        public static IServiceCollection AddCustomDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SmsDbContext>(builder => builder.UseSqlServer(configuration.GetConnectionString("SmsSqlServer")),ServiceLifetime.Scoped);
            services.AddScoped<DbContext, SmsDbContext>(f => f.GetService<SmsDbContext>());
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IEFAsyncRepository<>), typeof(EFRepository<>));
            return services;
        }
    }
}
