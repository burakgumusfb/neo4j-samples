using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Application.Mappings;
using Neo4j.Samples.Persistence.Context;

namespace Neo4j.Samples.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitofWork, UnitofWork>();
        services.AddScoped<IUserAppService,UserAppService>();
        services.AddSingleton(GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "12345678")));
        return services;
    }
}

