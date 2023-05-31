

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;

namespace Mini.Social.Media.Application.Features.FollowOperations.Commands.CreateRelation
{
    public class CreateRelationCommandValidator : AbstractValidator<CreateRelationCommandRequest>
    {
        private readonly IUnitOfWork _uow;
        public CreateRelationCommandValidator()
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
        public CreateRelationCommandValidator(IUnitOfWork uow)
        {
            this._uow = uow;
        }
    }
}