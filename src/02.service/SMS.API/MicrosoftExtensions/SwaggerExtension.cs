using Commen.ExceptionHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SMS.Base;
using SMS.Base.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMS.API.MicrosoftExtensions
{
    public static class SwaggerExtension
    {
        public static void AddCustomSwaggerDocumentService(this IServiceCollection services, AppSettings appSettings)
        {
            var apiGroupNames = Enum.GetValues<ApiGroupNameEnum>();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(config =>
            {

                config.AddSecurityDefinition("JWT", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Type into the textBox: Bearer {your JWT token}."
                });
                foreach (var groupName in apiGroupNames)
                {
                    config.SwaggerDoc(groupName.ToString(), new OpenApiInfo()
                    {
                        Title = $"{appSettings.WebApiProjectConfig.Name}_{groupName}",
                        Version = "v1",
                    });
                }

                var xmlNames = new List<string> { "SMS.Model.xml", "SMS.API.xml" };
                xmlNames.ForEach(name =>
                {
                    config.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, name));
                });
                config.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

                config.DocInclusionPredicate((docName, description) => docName == description.GroupName);
                config.OperationFilter<SwaggerOperationNameFilter>();
                config.OperationFilter<CommonResponseOperationFilter>();
            });
        }
        public static void UseCustomSwagger(this IApplicationBuilder app, AppSettings appSettings)
        {
            if (appSettings.SwaggerConfig.Enable)
            {
                string path = appSettings.SwaggerConfig.Path;

                if (!string.IsNullOrEmpty(path))
                {
                    app.Map(path, c => Swagger(c, path));
                }
                else
                {
                    Swagger(app);
                }
            }
        }

        private static void Swagger(IApplicationBuilder app, string path = "")
        {
            app.UseSwagger(x =>
            {
                x.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer>
                        {new() {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{path}/"}};
                });
            });
            app.UseSwaggerUI(x =>
            {
                var apiGroupNames = Enum.GetValues<ApiGroupNameEnum>().ToList();
                foreach (var groupName in apiGroupNames)
                {
                    x.SwaggerEndpoint($"{path}/swagger/{groupName}/swagger.json", groupName.ToString());
                }
            });
        }

    }

    #region IOperationFilter
    public class SwaggerOperationNameFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionDescriptor = (ControllerActionDescriptor)context.ApiDescription.ActionDescriptor;
            operation.OperationId = $"{actionDescriptor.ControllerName}_{actionDescriptor.ActionName}";
        }
    }

    public class CommonResponseOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var serializerSettings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            // Policy names map to scopes
            var requiredScopes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>()
                .Distinct();

            if (!requiredScopes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse
                {
                    Description = "Unauthorized",
                    Content = new Dictionary<string, OpenApiMediaType>()
                        {
                            {
                                "text/plain",
                                new OpenApiMediaType {
                                    Example = OpenApiAnyFactory.CreateFromJson(JsonConvert.SerializeObject(new RemoteServiceErrorInfo
                                    {
                                        Code = "auth_failed",
                                        Message = "token expired"
                                    },serializerSettings))
                                }
                            }
                        }

                });
            }

            operation.Responses.Add("400", new OpenApiResponse
            {
                Description = "BadRequest",
                Content = new Dictionary<string, OpenApiMediaType>()
                        {
                            {
                                "text/plain",
                                new OpenApiMediaType {
                                    Example = OpenApiAnyFactory.CreateFromJson(JsonConvert.SerializeObject(new RemoteServiceErrorInfo
                                    {
                                        Code = "REFERENCE_TO_DOCUMENTATION",
                                        Message = "REFERENCE_TO_DOCUMENTATION"
                                    },serializerSettings))
                                }
                            }
                        }

            });

            operation.Responses.Add("500", new OpenApiResponse
            {
                Description = "ServerError",
                Content = new Dictionary<string, OpenApiMediaType>()
                        {
                            {
                                "text/plain",
                                new OpenApiMediaType {
                                    Example = OpenApiAnyFactory.CreateFromJson(JsonConvert.SerializeObject(new RemoteServiceErrorInfo
                                    {
                                        Code = "internal_error",
                                        Message = "internal error, please contact the administrator"
                                    },serializerSettings))
                                }
                            }
                        }

            });
        }
    }
    #endregion
}
