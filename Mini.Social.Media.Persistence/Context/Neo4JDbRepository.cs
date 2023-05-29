using System;
using System.Linq.Expressions;
using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Persistence.Context;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Extensions;
using Neo4j.Driver;
using Mini.Social.Media.Application.Interfaces;

namespace Mini.Social.Media.Application.Mappings
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

