using System;
using System.Linq.Expressions;
using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Domain.Common;
using Neo4j.Samples.Domain.Entities;
using Neo4j.Samples.Persistence.Context;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Domain.Extensions;
using Neo4j.Driver;

namespace Neo4j.Samples.Application.Mappings
{
    public class Neo4JDbRepository : IGraphQLRepository
    {

        private IAsyncSession _session;
        private readonly IDriver _driver;
        public Neo4JDbRepository(IDriver driver)
        {
            _driver = driver;
            _session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        }

        public async Task ExecuteWriteAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
        {
            await this._session.ExecuteWriteAsync(t => t.RunAsync(query, parameters));
        }

        public async Task<IResultCursor> ExecuteReadAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
        {
            return await this._session.ExecuteReadAsync<IResultCursor>(t => t.RunAsync(query, query));
        }
    }
}

