using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Swagger;

using System.IO;
using System.Reflection;

namespace Herald.Web.Swagger
{
    public static class Configurations
    {
        public static readonly string _assemblyName;
        public static readonly string _assemblyVersion;

        static Configurations()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var assemblyName = executingAssembly.GetName();

            _assemblyName = assemblyName.Name;
            _assemblyVersion = string.Concat("v", assemblyName.Version);
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                var xmlDocsFileName = $"{PlatformServices.Default.Application.ApplicationName}.xml";
                var xmlDocsFullPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, xmlDocsFileName);

                if (File.Exists(xmlDocsFullPath))
                {
                    setup.IncludeXmlComments(xmlDocsFileName, true);
                }

                setup.SwaggerDoc(_assemblyVersion, new OpenApiInfo
                {
                    Title = _assemblyName,
                    Version = _assemblyVersion
                });

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. Example: 'Bearer eyJhbGciPSA...'",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            app.UseMiddleware<SwaggerMiddleware>(new object[1]
            {
                new SwaggerOptions()
            });

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint($"/swagger/{_assemblyVersion}/swagger.json", _assemblyVersion);
            });

            return app;
        }
    }
}
