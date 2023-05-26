using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Domain.Entities;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Domain.Common;
using Neo4j.Samples.Domain.Entities;

namespace Neo4j.Samples.Application.Mappings
{
    public class UserAppService : IUserAppService
    {
        private readonly IUnitofWork _uow;
        public UserAppService(IUnitofWork uow)
        {
            _uow = uow;
        }

        public async Task<int> CreateAsync(User entity)
        {
            return await this._uow.UserRepository.CreateAsync(entity);
        }
    }
}

