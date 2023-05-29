

using FluentValidation;
using Mini.Social.Media.Application.Application.Features.UserOperations.Commands.CreateUser;

namespace Mini.Social.Media.Application.Features.UserOperations.Commands
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