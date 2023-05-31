

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;

namespace Mini.Social.Media.Application.Features.FollowOperations.Commands.CreateFollow
{
    public class CreateFollowCommandValidator : AbstractValidator<CreateFollowCommandRequest>
    {
        private readonly IUnitOfWork _uow;
        public CreateFollowCommandValidator()
        {

            RuleFor(p => p.CurrentUserEmail)
                .NotNull()
                .WithMessage("Current email cannot be null")
                .NotEmpty()
                .WithMessage("Current email cannot be null");

            RuleFor(p => p.TargetUserEmail)
                .NotNull()
                .WithMessage("Target email cannot be null")
                .NotEmpty()
                .WithMessage("Target email cannot be null");


        }
        public CreateFollowCommandValidator(IUnitOfWork uow)
        {
            this._uow = uow;
        }
    }
}