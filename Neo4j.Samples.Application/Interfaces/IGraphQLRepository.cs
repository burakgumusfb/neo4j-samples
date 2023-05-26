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
    {
        Task ExecuteWriteAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null);
        Task<IResultCursor> ExecuteReadAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
    }
}

