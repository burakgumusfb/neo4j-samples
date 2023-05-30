

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;
using Mini.Social.Media.Application.Interfaces.UnitOfWork;

namespace Mini.Social.Media.Application.Features.UserOperations.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        private readonly IUnitOfWork _uow;
        public CreateUserCommandValidator(IUnitOfWork uow)
        {
            this._uow = uow;
            RuleFor(p => p.Email)
                .NotNull()
                .WithMessage("Email cannot be null")
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .MaximumLength(20)
                .WithMessage("Email cannot be empty.")
                .MinimumLength(3)
                .WithMessage("Email must be between 3 and 20 chars.")
                .MustAsync(BeUniqueEmail).WithMessage("Email already exist.");
        }

        public async Task<bool> BeUniqueEmail(string email,CancellationToken cancellationToken)
        {
            return !await _uow.UserRepository.GetAll().AnyAsync(l => l.Email == email);
        }
    }
}