using MediatR;
using Mini.Social.Media.Application.Abstraction;
using Mini.Social.Media.Application.Features.UserOperations.Commands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Social.Media.Application.Features.FollowOperations.Commands.CreateFollow
{
    public class CreateFollowCommandRequest : IRequest<ServiceResult<CreateFollowCommandResponse>>
    {
        public string CurrentUserEmail { get; set; }
        public string TargetUserEmail { get; set; }
    }
}