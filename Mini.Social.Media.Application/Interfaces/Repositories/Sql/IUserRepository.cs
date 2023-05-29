using System;
using AutoMapper;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;

namespace Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Sql
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User entity);
    }
}

