

using FluentValidation;
using Onion.Architecture.Application.Features.UserOperations.Commands.CreateUser;

namespace Onion.Architecture.Application.Features.UserOperations.Commands
{
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .WithMessage("Lütfen 'Email'i boş geçmeyiniz.")
                .MaximumLength(20)
                .MinimumLength(3)
                .WithMessage("'ProductCode' değeri 3 ile 20 karakter arasında olmalıdır.");
        }
    }
}