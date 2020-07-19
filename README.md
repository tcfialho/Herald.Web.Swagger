# Herald.Web.Swagger

![Status](https://github.com/tcfialho/Herald.Web.Swagger/workflows/Herald.Web.Swagger/badge.svg) ![Coverage](https://codecov.io/gh/tcfialho/Herald.Web.Swagger/branch/master/graph/badge.svg) ![NuGet](https://buildstats.info/nuget/Herald.Web.Swagger)

## Overview
 - Seamlessly adds a [Swagger](https://github.com/domaindrivendev/Swashbuckle) to WebApi projects.

## Installation
 - Package Manager
    ```
    Install-Package Herald.Web.Swagger
    ```
 - .NET CLI
    ```
    dotnet add package Herald.Web.Swagger
    ```

See more information in [Nuget](https://www.nuget.org/packages/Herald.Web.Swagger/).

## Usage
 - Configure service and middleware in Startup.cs
    ```c#
    using Herald.Web.Swagger

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //...
            services.AddSwagger();
            //...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //...
            app.UseSwagger();
            //...
        }
    }
    ```
   
 - Access API Swagger
    + By default, API Swagger URL is "/swagger".

## Credits

Author [**Thiago Fialho**](https://br.linkedin.com/in/thiago-fialho-139ab116)

## License

Herald.Web.Swagger is licensed under the [MIT License](LICENSE).