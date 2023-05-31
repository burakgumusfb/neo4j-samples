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
using Neo4j.Driver;

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
            var query = string.Format("CREATE (u:User {{email: '{0}', password: '{1}'}}) RETURN ID(u) AS nodeId", entity.Email, entity.Password);
            var record = await this._gpl.ExecuteWriteAsync(query);
            var createdNodeId = record["nodeId"].As<int>();
            return createdNodeId;

        }

        public async Task CreateFollow(string currentEmail, string targetEmail)
        {
            var query = string.Format(@"MATCH (u1:User {{email: '{0}'}}), (u2:User {{email: '{1}'}})
             CREATE (u1)-[:RELATIONSHIP]->(u2)
             RETURN u1, u2", currentEmail, targetEmail);
            var record = await this._gpl.ExecuteWriteAsync(query);
            var u1Node = record["u1"].As<INode>();
            var u2Node = record["u2"].As<INode>();
        }
    }
}

