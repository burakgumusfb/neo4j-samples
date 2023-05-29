using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using Neo4j.Samples.Domain.Common;
using Neo4j.Samples.Domain.Entities;

namespace Neo4j.Samples.Application.Interfaces
{
    public interface IGraphQLRepository
    {   Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync ();
        Task ExecuteWriteAsync(string query);
        Task<IResultCursor> ExecuteReadAsync(string query);
    }
}

