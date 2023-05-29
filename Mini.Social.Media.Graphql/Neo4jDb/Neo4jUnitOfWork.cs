using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Domain.Extensions;
using Neo4j.Driver;

namespace Mini.Social.Media.Graphql.GraphqlDB
{
    public class Neo4jUnitOfWork : IGraphQLUnitOfWork
    {

        private IAsyncSession _session;
        private IAsyncTransaction _transaction;
        private readonly IDriver _driver;
        public Neo4jUnitOfWork(IDriver driver)
        {
            _driver = driver;
            _session = driver.AsyncSession(o => o.WithDatabase("neo4j"));
        }

        public async Task<int> ExecuteWriteAsync(string query)
        {
            var createCommand =  await this._transaction.RunAsync(query);
            var record = await createCommand.SingleAsync();

            var createdNode = record["u"].As<INode>();
            var createdNodeId = createdNode.Id.ToInt32();

            return createdNodeId;
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

