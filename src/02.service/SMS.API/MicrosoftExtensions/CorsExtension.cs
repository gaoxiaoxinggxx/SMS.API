using Microsoft.Extensions.DependencyInjection;
using SMS.Base;
using SMS.Base.Consts;
using System.Linq;

namespace SMS.API.MicrosoftExtensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCorsService(this IServiceCollection services, AppSettings appSettings)
        {
            var origins = appSettings.WebApiProjectConfig.Origins.Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            services.AddCors(options =>
            {
                options.AddPolicy(AppConst.CorsDefaultName, builder =>
                {
                    if (!appSettings.WebApiProjectConfig.IsDevelopment)
                        builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    else
                        builder.SetIsOriginAllowed(x => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            return services;

        }

    }
}
