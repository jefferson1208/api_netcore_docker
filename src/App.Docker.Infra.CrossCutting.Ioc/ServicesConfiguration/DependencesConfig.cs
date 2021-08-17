using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using App.Docker.Domain.Commands.Products;
using App.Docker.Domain.Communication;
using App.Docker.Domain.Handlers;
using App.Docker.Domain.Handlers.Products;
using App.Docker.Domain.Interfaces.Products;
using App.Docker.Domain.Interfaces.Users;
using App.Docker.Domain.Messages;
using App.Docker.Domain.Queries.Products;
using App.Docker.Domain.Queries.Users;
using App.Docker.Infra.CrossCutting.Ioc.Api;
using App.Docker.Infra.CrossCutting.Ioc.Swagger;
using App.Docker.Infra.Data.Repository.Products;
using App.Docker.Infra.Data.Repository.Users;

namespace App.Docker.Infra.CrossCutting.Ioc.ServicesConfiguration
{
    public static class DependencesConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddMediatR(typeof(ApiConfig));
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserQuery, UserQuery>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductQuery, ProductQuery>();


            //Handlers
            services.AddScoped<IRequestHandler<CreateProductCommand, bool>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, bool>, ProductCommandHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        }
    }
}
