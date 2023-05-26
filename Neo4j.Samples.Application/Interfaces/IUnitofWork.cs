using Microsoft.EntityFrameworkCore.Infrastructure;
using Neo4j.Samples.Domain.Entities;
using System.Collections.Generic; 
using System.Collections.Generic;
using System.Data;

namespace Neo4j.Samples.Application.Interfaces
{
    public interface IUnitofWork
    {
        IRepository<User> UserRepository { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync ();
        T GetQuery<T>(string query, object parms, CommandType commandType = CommandType.Text);
        List<T> GetAllQuery<T>(string query, object parms = null, CommandType commandType = CommandType.Text);
        int Execute(string query, object parms = null, CommandType commandType = CommandType.Text);
    }
}