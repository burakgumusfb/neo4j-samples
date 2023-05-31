using AutoMapper;
using MediatR;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Application.Abstraction;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;
using Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Sql;
using Mini.Social.Media.Application.Interfaces.UnitOfWork.Repositories.Neo4j;

namespace Mini.Social.Media.Application.Features.FollowOperations.Commands.CreateFollow
{
    public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommandRequest, ServiceResult<CreateFollowCommandResponse>>
    {

        private readonly INeo4jUnitOfWork _nuow;
        private readonly INeo4jUserRepository _neo4jUserRepository;
        private readonly IMapper _mapper;
        public CreateFollowCommandHandler(
            INeo4jUserRepository neo4jUserRepository,
            INeo4jUnitOfWork nuow,
             IMapper mapper)
        {
            _nuow = nuow;
            _neo4jUserRepository = neo4jUserRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<CreateFollowCommandResponse>> Handle(CreateFollowCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = new ServiceResult<CreateFollowCommandResponse>();
            await this._nuow.BeginTransactionAsync();

            try
            {
                await _neo4jUserRepository.CreateFollow(request.CurrentUserEmail,request.TargetUserEmail);

                await this._nuow.CommitAsync();
            }
            catch (System.Exception ex)
            {  
               await this._nuow.RollbackAsync();

               serviceResult.Message = ex.Message;
               serviceResult.MessageType = MessageType.Danger;
            }
            return serviceResult;
        }
    }
}