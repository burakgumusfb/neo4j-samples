using System;
using AutoMapper;
using Neo4j.Samples.Domain.Common;
using Neo4j.Samples.Domain.Entities;

namespace Neo4j.Samples.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<int> CreateAsync(User entity);
    }
}

