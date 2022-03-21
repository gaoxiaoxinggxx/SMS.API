using Microsoft.Extensions.DependencyInjection;
using SMS.Model;

namespace SMS.API.MicrosoftExtensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddCustomAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
