using System;
using AutoMapper;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Domain.Entities;

namespace Mini.Social.Media.Application.Mappings
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			// CreateMap<CreateUserCommandRequest,User>();
		}
	}
}

