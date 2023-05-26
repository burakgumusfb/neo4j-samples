using System;
using System.Linq.Expressions;
using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Domain.Common;
using Neo4j.Samples.Domain.Entities;
using Neo4j.Samples.Persistence.Context;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Domain.Extensions;

namespace Neo4j.Samples.Application.Mappings
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Neo4jSamplesDbContext _db;
        private DatabaseFacade transaction { get; set; }
        public Repository(Neo4jSamplesDbContext db)
        {
            _db = db;
        }
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] properties)
        {
            var query = _db.Set<T>().AsNoTracking();

            if (properties.Any())
            {
                query = properties.Aggregate(query, (current, property) => current.Include(property));
            }
            return query;
        }
        public async Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] properties)
        {
            var query = _db.Set<T>().AsNoTracking();

            if (properties.Any())
            {
                query = properties.Aggregate(query, (current, property) => current.Include(property));
            }

            return await Task.FromResult(query);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return _db.Set<T>().Where(where);
        }
        public async Task<int> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.CreateDate = DateTime.Now;
            entity.CreateUserId = UserID;
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();

            return entity.GetType().GetProperty("Id").GetValue(entity,null).ToInt32();
        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.ModifyDate = DateTime.Now;
            entity.ModifyUserId = UserID;
            _db.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.Entry(entity).Property(x => x.ModifyDate).IsModified = false;
            _db.Entry(entity).Property(x => x.ModifyUserId).IsModified = false;
            _db.Entry(entity).Property(x => x.CreateUserId).IsModified = false;
            _db.Entry(entity).Property(x => x.CreateDate).IsModified = false;
            await _db.SaveChangesAsync();
        }
        public async Task HardDeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }
        public async Task BulkInsertAsync(List<T> entities)
        {
            var createDate = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.CreateDate = createDate;
                entity.CreateUserId = UserID;
            }
           await _db.BulkInsertAsync(entities);
        }
        public async Task BulkUpdateAsync(List<T> entities)
        {
            var modifyDate = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.ModifyDate = modifyDate;
                entity.ModifyUserId = UserID;
            }
            var bulkConfig = new BulkConfig()
            {
                PropertiesToExclude = new List<string>
                {
                    "CreateDate",
                    "CreateUserID",
                    "GUID",
                }
            };
            await _db.BulkUpdateAsync(entities, bulkConfig);
        }
        public async Task BulkDeleteAsync(List<T> entities)
        {
            var deleteDate = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.DeleteDate = deleteDate;
                entity.DeleteUserId = UserID;
                entity.IsDeleted = true;
            }
            var bulkConfig = new BulkConfig()
            {
                PropertiesToExclude = new List<string>
                {
                    "ModifyDate",
                    "ModifyUserID",
                    "CreateDate",
                    "CreateUserID",
                    "GUID",
                }
            };
           await _db.BulkUpdateAsync(entities, bulkConfig);
        }
        public async Task BulkHardDeleteAsync(List<T> entities)
        {
            await _db.BulkDeleteAsync(entities);
        }
        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            entity.DeleteUserId = this.UserID;
            _db.Entry(entity).Property(x => x.ModifyDate).IsModified = false;
            _db.Entry(entity).Property(x => x.ModifyUserId).IsModified = false;
            _db.Entry(entity).Property(x => x.CreateUserId).IsModified = false;
            _db.Entry(entity).Property(x => x.CreateDate).IsModified = false;
            _db.Entry(entity).State = EntityState.Modified;
            await  _db.SaveChangesAsync();
        }
        protected void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        ~Repository()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int UserID
        {
            get
            {
                return 1;
            }
        }
    }
}

