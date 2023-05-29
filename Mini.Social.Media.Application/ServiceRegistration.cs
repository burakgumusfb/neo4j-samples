using System.Reflection;
using FluentValidation;
using MediatR;
using AutoMapper;
using Mini.Social.Media.Application.Mappings;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

            return services;
        }
    }
}
