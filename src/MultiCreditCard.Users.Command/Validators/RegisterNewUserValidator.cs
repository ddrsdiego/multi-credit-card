using FluentValidation;
using MultiCreditCard.Users.Command.Commands;

namespace MultiCreditCard.Users.Command.Validators
{
    public class RegisterNewUserValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .WithMessage("Nome de Usuário não deve ser nulo ou branco.");

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email não deve ser nulo ou branco.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Senha não deve ser nulo ou branco.");
        }
    }
}
