using App.Docker.Infra.CrossCutting.Ioc.Extensions;
using App.Docker.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace App.Docker.Infra.CrossCutting.Ioc.Api
{
    public static class ApiConfig
    {
        public static void WebApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    p => p.EnableRetryOnFailure(
                            maxRetryCount: 2,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null).MigrationsHistoryTable("Application_Migrations", null));
            });

            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
                opt.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;

            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            
            //services.AddHealthChecks()
            //    .AddCheck("Users", new DataBaseHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSql");

            //services.AddHealthChecksUI();
        }

        public static void BuilderConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHealthChecks("/api/hc", new HealthCheckOptions()
                //{
                //    Predicate = _ => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});

                //endpoints.MapHealthChecksUI(options =>
                //{
                //    options.UIPath = "/api/hc-ui";
                //    options.ResourcesPath = "/api/hc-ui-resources";

                //    options.UseRelativeApiPath = false;
                //    options.UseRelativeResourcesPath = false;
                //    options.UseRelativeWebhookPath = false;
                //});

            });



        }

    }
}
