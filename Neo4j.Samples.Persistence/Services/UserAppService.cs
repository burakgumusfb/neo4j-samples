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
        private readonly IGraphQLRepository _igql;
        public UserAppService(IUnitofWork uow, IGraphQLRepository igql)
        {
            _uow = uow;
            _igql = igql;
        }

        public async Task<int> CreateAsync(User entity)
        {
            await this._igql.ExecuteWriteAsync(string.Format("CREATE (u:User {{email: '{0}', password: '{1}'}})", entity.Email, entity.Password));
            return await this._uow.UserRepository.CreateAsync(entity);
        }
    }
}

