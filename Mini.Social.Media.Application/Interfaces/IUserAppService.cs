using System;
using AutoMapper;
using Mini.Social.Media.Domain.Entities;
using Mini.Social.Media.Domain.Common;
using Mini.Social.Media.Domain.Entities;

namespace Mini.Social.Media.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<int> CreateAsync(User entity);
    }
}

