using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

using System;
using System.IO;
using System.Linq;
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
            return services.AddSwagger(new SwaggerOptions() { Servers = Enumerable.Empty<string>() });
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerOptions options)
        {
            return services.AddSwagger(configure =>
            {
                options.CopyTo(configure);
            });
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerOptions> configure)
        {
            services.Configure(configure);
            var options = new SwaggerOptions();
            configure?.Invoke(options);

            services.AddSwaggerGen(setup =>
            {
                var xmlDocsFileName = $"{PlatformServices.Default.Application.ApplicationName}.xml";
                var xmlDocsFullPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, xmlDocsFileName);

                if (File.Exists(xmlDocsFullPath))
                {
                    setup.IncludeXmlComments(xmlDocsFileName, true);
                }

                foreach (var server in options.Servers)
                {
                    setup.AddServer(new OpenApiServer() { Url = server });
                }

                setup.OperationFilterDescriptors = options.OperationFilterDescriptors;

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
            app.UseSwagger(c => { });

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint($"/swagger/{_assemblyVersion}/swagger.json", _assemblyVersion);
            });

            return app;
        }
    }
}
