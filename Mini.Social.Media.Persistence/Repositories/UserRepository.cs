using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Sql;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;

namespace Mini.Social.Media.Application.Mappings
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _uow;
        public UserRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateAsync(User entity)
        {
            return await this._uow.UserRepository.CreateAsync(entity);
        }
    }
}

