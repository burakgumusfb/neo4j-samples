using Microsoft.EntityFrameworkCore.Infrastructure;
using Mini.Social.Media.Domain.Entities;
using System.Collections.Generic; 
using System.Collections.Generic;
using System.Data;

namespace Mini.Social.Media.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork:ITransactionManager
    {
        IRepository<User> UserRepository { get; }
        T GetQuery<T>(string query, object parms, CommandType commandType = CommandType.Text);
        List<T> GetAllQuery<T>(string query, object parms = null, CommandType commandType = CommandType.Text);
        int Execute(string query, object parms = null, CommandType commandType = CommandType.Text);
    }
}