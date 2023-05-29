using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;

namespace Mini.Social.Media.Application.Interfaces
{
    public interface ITransactionManager
    {
        public Task BeginTransactionAsync();
        public Task CommitAsync();
        public Task RollbackAsync();
    }
}

