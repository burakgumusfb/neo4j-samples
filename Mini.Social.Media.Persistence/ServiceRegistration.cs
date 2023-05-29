using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;
using Mini.Social.Media.Persistence.Context;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Application.Mappings;

namespace Mini.Social.Media.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {   
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitofWork, UnitofWork>();
        services.AddScoped<IUserAppService,UserAppService>();
        return services;
    }
}

