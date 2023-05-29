using System;
using AutoMapper;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;

namespace Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Services
{
    public interface IUserAppService
    {
        Task<int> CreateAsync(CreateUserCommandRequest entity);
    }
}
