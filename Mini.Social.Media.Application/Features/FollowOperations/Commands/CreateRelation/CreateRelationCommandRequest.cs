using MediatR;
using Mini.Social.Media.Application.Abstraction;
using Mini.Social.Media.Application.Features.UserOperations.Commands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Social.Media.Application.Features.FollowOperations.Commands.CreateRelation
{
    public class CreateRelationCommandRequest : IRequest<ServiceResult<CreateRelationCommandResponse>>
    {
        public string CurrentUserEmail { get; set; }
        public string TargetUserEmail { get; set; }
    }
}