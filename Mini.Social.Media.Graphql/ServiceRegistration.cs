using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Application.Mappings;
using Mini.Social.Media.Graphql.GraphqlDB;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;
using Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Neo4j;

namespace Mini.Social.Media.Graphql;

public static class ServiceRegistration
{
    public static IServiceCollection AddGraphqlServices(this IServiceCollection services)
    {   
        services.AddScoped<IGraphQLUnitOfWork,Neo4jUnitOfWork>();
        services.AddScoped<INeo4jUserRepository,UserRepository>();
        services.AddSingleton(GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "12345678")));
        return services;
    }
}

