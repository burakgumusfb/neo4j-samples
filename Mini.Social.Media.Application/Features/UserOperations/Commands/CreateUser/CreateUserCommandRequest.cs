using MediatR;
using Mini.Social.Media.Application.Abstraction;
using Mini.Social.Media.Application.Features.UserOperations.Commands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<ServiceResult<CreateUserCommandResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}