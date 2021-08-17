using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using App.Docker.Infra.CrossCutting.Identity.Configuration;
using App.Docker.Infra.CrossCutting.Ioc.Api;
using App.Docker.Infra.CrossCutting.Ioc.ServicesConfiguration;
using App.Docker.Infra.CrossCutting.Ioc.Swagger;

namespace App.Docker.Api
{
    public class StartupTests
    {
        public StartupTests(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsProduction())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddIdentityConfiguration(Configuration);
            services.WebApiConfig(Configuration);
            services.AddSwaggerConfig();
            services.ResolveDependencies();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.BuilderConfig(env);
            app.UseSwaggerConfig(provider);
        }
    }
}
