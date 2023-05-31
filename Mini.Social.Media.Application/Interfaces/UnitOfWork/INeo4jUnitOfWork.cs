using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Neo4j;

namespace Mini.Social.Media.Application.Interfaces.UnitOfWork
{
    public interface INeo4jUnitOfWork:ITransactionManager
    {  
        public INeo4jUserRepository Neo4jUserRepository{get;set;}
        Task<IRecord> ExecuteWriteAsync(string query);
        Task<IResultCursor> ExecuteReadAsync(string query);
    }
}

