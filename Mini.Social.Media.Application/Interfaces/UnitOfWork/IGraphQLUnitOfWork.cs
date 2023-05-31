using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;

namespace Mini.Social.Media.Application.Interfaces.UnitOfWork
{
    public interface IGraphQLUnitOfWork:ITransactionManager
    {  
        Task<IRecord> ExecuteWriteAsync(string query);
        Task<IResultCursor> ExecuteReadAsync(string query);
        Task CreateRelation(string query);
    }
}

