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
    public class CreateProductCommandHandler : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IGraphQLUnitOfWork _gql;
        private readonly IUserRepository _userRepository;
        private readonly INeo4jUserRepository _neo4jUserRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(
            IUnitOfWork uow,
            IUserRepository userRepository,
            INeo4jUserRepository _neo4jUserRepository,
            IGraphQLUnitOfWork gql,
             IMapper mapper)
        {
            _uow = uow;
            _gql = gql;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = new ServiceResult<CreateUserCommandResponse>();
            await this._gql.BeginTransactionAsync();
            try
            {

                await _neo4jUserRepository.CreateAsync(new User(request.Email, request.Password));
                await this._gql.CommitAsync();
            }
            catch (System.Exception)
            {  
               await this._gql.RollbackAsync();
            }
            return serviceResult;
        }
    }
}