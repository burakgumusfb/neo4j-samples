using MediatR;
using Neo4j.Samples.Application.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Architecture.Application.Features.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<ServiceResult<CreateUserCommandResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}