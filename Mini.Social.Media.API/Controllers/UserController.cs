using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Application.Features.FollowOperations.Commands.CreateRelation;
using Mini.Social.Media.Application.Features.UserOperations.Commands;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;

namespace Mini.Social.Media.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly IUnitOfWork _uow;
    public UserController(IMediator mediator,IUnitOfWork uow)
    {
        this._mediator = mediator;
        this._uow = uow;
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
       var createStockCommand = new CreateUserCommandValidator(_uow);

       var result = await createStockCommand.ValidateAsync(request);

       if (result.IsValid)
       {
           var response = await _mediator.Send(request);
           return Ok(response);
       }

       var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
       return BadRequest(errorMessages);

    }

    [HttpPost("create-relation")]
    public async Task<IActionResult> CreateRelation(CreateRelationCommandRequest request)
    {
       var createStockCommand = new CreateRelationCommandValidator();

       var result = await createStockCommand.ValidateAsync(request);

       if (result.IsValid)
       {
           var response = await _mediator.Send(request);
           return Ok(response);
       }

       var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
       return BadRequest(errorMessages);

    }
}

