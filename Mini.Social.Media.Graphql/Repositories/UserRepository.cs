using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Neo4j;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;

namespace Mini.Social.Media.Application.Mappings
{
    public class UserRepository : INeo4jUserRepository
    {
        private readonly IGraphQLUnitOfWork _gpl;
        public UserRepository(IGraphQLUnitOfWork gpl)
        {
            _gpl = gpl;
        }

        public async Task<int> CreateAsync(User entity)
        {
            return await this._gpl.ExecuteWriteAsync(string.Format("CREATE (u:User {{email: '{0}', password: '{1}'}})", entity.Email, entity.Password));

        }
    }
}

