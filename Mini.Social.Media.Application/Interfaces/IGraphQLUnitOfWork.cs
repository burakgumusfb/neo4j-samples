using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;

namespace Mini.Social.Media.Application.Interfaces
{
    public interface IGraphQLUnitOfWork:ITransactionManager
    {  
        Task ExecuteWriteAsync(string query);
        Task<IResultCursor> ExecuteReadAsync(string query);
    }
}

