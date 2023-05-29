using AutoMapper;
using MediatR;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Application.Common.BaseModels;
using Mini.Social.Media.Application.Interfaces;
using Mini.Social.Media.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Social.Media.Application.Features.UserOperations.Commands.CreateUser
{
    public class CreateProductCommandHandler : IRequestHandler<CreateUserCommandRequest, ServiceResult<CreateUserCommandResponse>>
    {
        private readonly IUnitofWork _uow;
        private readonly IGraphQLUnitOfWork _igql;
        private readonly IUserAppService _userAppService;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(
            IUnitofWork uow,
            IUserAppService userAppService,
            IGraphQLUnitOfWork igql,
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