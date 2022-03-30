using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SMS.Base;
using System.Text;

namespace SMS.API.MicrosoftExtensions
{
    public static class AddCustomAuthService
    {
        public static IServiceCollection AddCustomAuth(this IServiceCollection services, AppSettings appSettings)
        {
            var commonEvents = new JwtBearerEvents
            {
                //OnChallenge = context => throw new Common.VNextFramework.BCChinaUnauthorizedException("The Token is invalidate or provided has already expired.") { Code = "auth_failed" }
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AppClientAuthConfig.Secret))
                };
                options.Events = commonEvents;
            });
            // 自定义Scheme 来进行校验
            //.AddJwtBearer(AppConst.InternalApiAuthScheme, options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidIssuer = appSettings.InternalApiClientAuthConfig.Key,
            //        ValidateIssuer = true,
            //        ValidateAudience = false,
            //        IssuerSigningKey =
            //            new SymmetricSecurityKey(
            //                Encoding.UTF8.GetBytes(appSettings.InternalApiClientAuthConfig.Secret))

            //    };
            //    options.Events = commonEvents;
            //});
            return services;
        }

        public static IApplicationBuilder UseCustomAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
