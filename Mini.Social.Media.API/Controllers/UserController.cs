using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Application.Features.UserOperations.Commands;

namespace Mini.Social.Media.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{

    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
       var createStockCommand = new CreateUserCommandValidator();

       var result = createStockCommand.Validate(request);

       if (result.IsValid)
       {
           var response = await _mediator.Send(request);
           return Ok(response);
       }

       var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
       return BadRequest(errorMessages);

    }
}

