using AutoMapper;
using MediatR;
using Neo4j.Samples.Application.Common.BaseModels;
using Neo4j.Samples.Application.Interfaces;
using Neo4j.Samples.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Architecture.Application.Features.UserOperations.Commands.CreateUser
{
    public class CreateProductCommandHandler : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
    {
        private readonly IUnitofWork _uow;
        private readonly IGraphQLRepository _igql;
        private readonly IUserAppService _userAppService;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(
            IUnitofWork uow,
            IUserAppService userAppService,
            IGraphQLRepository igql,
             IMapper mapper)
        {
            _uow = uow;
            _igql = igql;
            _userAppService = userAppService;
            _mapper = mapper;
        }
        public async Task<ServiceResult<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var serviceResult = new ServiceResult<CreateUserCommandResponse>();
            await this._uow.BeginTransactionAsync();
            await this._igql.BeginTransactionAsync();
                await _userAppService.CreateAsync(new User(request.Email,request.Password));
            await this._igql.CommitAsync();
            await this._uow.CommitAsync();
            return serviceResult;
        }
    }
}