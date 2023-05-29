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
        private IAsyncTransaction _transaction;
        private readonly IDriver _driver;
        public Neo4JDbRepository(IDriver driver)
        {
            _driver = driver;
            _session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        }

        public async Task ExecuteWriteAsync(string query)
        {
            await this._transaction.RunAsync(query);
        }

        public async Task<IResultCursor> ExecuteReadAsync(string query)
        {
            return await this._transaction.RunAsync(query);

        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _session.BeginTransactionAsync();
            }
        }
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }
    }
}

