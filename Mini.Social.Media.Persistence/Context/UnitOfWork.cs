using Dapper;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Application.Mappings;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Mini.Social.Media.Persistence.Context
{
    public class UnitofWork : IUnitofWork
    {
        private bool Disposed = false;
        private readonly MiniSocialMediaDbContext DB;

        private IRepository<User> _userRepository;

        private IDbContextTransaction _transaction;

        public IRepository<User> UserRepository => _userRepository ??= new Repository<User>(DB);

        public UnitofWork(MiniSocialMediaDbContext db)
        {
            DB = db;
        }

        public async Task BeginTransactionAsync()
        {
            if (DB.Database.CurrentTransaction == null)
            {
                _transaction = await DB.Database.BeginTransactionAsync();
            }
        }
        public async Task CommitAsync()
        {
            if (DB.Database.CurrentTransaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }
        public async Task RollbackAsync()
        {
            if (DB.Database.CurrentTransaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public T GetQuery<T>(string query, object parms, CommandType commandType = CommandType.Text)
        {
            var transaction = DB.Database.CurrentTransaction?.GetUnderlyingTransaction(new BulkConfig() { });
            if (transaction != null)
                return DB.Database.GetDbConnection().Query<T>(query, parms, commandType: commandType, transaction: transaction, commandTimeout: 3600).FirstOrDefault();
            else
                return DB.Database.GetDbConnection().Query<T>(query, parms, commandType: commandType, commandTimeout: 3600).FirstOrDefault();
        }
        public List<T> GetAllQuery<T>(string query, object parms = null, CommandType commandType = CommandType.Text)
        {
            var transaction = DB.Database.CurrentTransaction?.GetUnderlyingTransaction(new BulkConfig() { });
            if (transaction != null)
                return DB.Database.GetDbConnection().Query<T>(query, parms, commandType: commandType, transaction: transaction,commandTimeout:3600).ToList();
            else
                return DB.Database.GetDbConnection().Query<T>(query, parms, commandType: commandType, commandTimeout: 3600).ToList();

        }
        public int Execute(string query, object parms = null, CommandType commandType = CommandType.Text)
        {
            var transaction = DB.Database.CurrentTransaction?.GetUnderlyingTransaction(new BulkConfig() { });
            if (transaction == null)
                throw new Exception("transaction_problem");

            var result = DB.Database.GetDbConnection().Execute(query, parms, commandType: commandType, transaction: transaction,commandTimeout: 3600);
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    _transaction.Dispose();
                    DB.Dispose();
                }
            }
            Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}