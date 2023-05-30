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

namespace Mini.Social.Media.Application.Features.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IGraphQLUnitOfWork _gql;
        private readonly IUserRepository _userRepository;
        private readonly INeo4jUserRepository _neo4jUserRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(
            IUnitOfWork uow,
            IUserRepository userRepository,
            INeo4jUserRepository neo4jUserRepository,
            IGraphQLUnitOfWork gql,
             IMapper mapper)
        {
            _uow = uow;
            _gql = gql;
            _userRepository = userRepository;
            _neo4jUserRepository = neo4jUserRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = new ServiceResult<CreateUserCommandResponse>();
            
            await this._uow.BeginTransactionAsync();
            await this._gql.BeginTransactionAsync();

            try
            {
                var userEntity = new User(request.Email,request.Password);

                await _uow.UserRepository.CreateAsync(userEntity);
                await _neo4jUserRepository.CreateAsync(userEntity);

                await this._uow.CommitAsync();
                await this._gql.CommitAsync();
            }
            catch (System.Exception ex)
            {  
               await this._uow.RollbackAsync(); 
               await this._gql.RollbackAsync();

               serviceResult.Message = ex.Message;
               serviceResult.MessageType = MessageType.Danger;
            }
            return serviceResult;
        }
    }
}