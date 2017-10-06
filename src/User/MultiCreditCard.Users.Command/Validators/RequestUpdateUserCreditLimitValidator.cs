using FluentValidation;
using MultiCreditCard.Users.Command.Commands;

namespace MultiCreditCard.Users.Command.Validators
{
    public class RequestUpdateUserCreditLimitValidator : AbstractValidator<RequestUpdateUserCreditLimitCommand>
    {
        public RequestUpdateUserCreditLimitValidator()
        {
            RuleFor(user => user.UserId)
                .NotEmpty()
                .WithMessage("Id do usuário inválido para essa operação.");

            RuleFor(user => user.NewCreditLimit)
                .GreaterThan(0)
                .WithMessage("Valor do limite deve ser maior que zero.");
        }
    }
}
