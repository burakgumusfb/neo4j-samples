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

namespace Mini.Social.Media.Application.Features.FollowOperations.Commands.CreateRelation
{
    public class CreateRelationCommandHandler : IRequestHandler<CreateRelationCommandRequest, ServiceResult<CreateRelationCommandResponse>>
    {

        private readonly IGraphQLUnitOfWork _gql;
        private readonly INeo4jUserRepository _neo4jUserRepository;
        private readonly IMapper _mapper;
        public CreateRelationCommandHandler(
            INeo4jUserRepository neo4jUserRepository,
            IGraphQLUnitOfWork gql,
             IMapper mapper)
        {
            _gql = gql;
            _neo4jUserRepository = neo4jUserRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<CreateRelationCommandResponse>> Handle(CreateRelationCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = new ServiceResult<CreateRelationCommandResponse>();
            await this._gql.BeginTransactionAsync();

            try
            {
                await _neo4jUserRepository.CreateFollow(request.CurrentUserEmail,request.TargetUserEmail);

                await this._gql.CommitAsync();
            }
            catch (System.Exception ex)
            {  
               await this._gql.RollbackAsync();

               serviceResult.Message = ex.Message;
               serviceResult.MessageType = MessageType.Danger;
            }
            return serviceResult;
        }
    }
}