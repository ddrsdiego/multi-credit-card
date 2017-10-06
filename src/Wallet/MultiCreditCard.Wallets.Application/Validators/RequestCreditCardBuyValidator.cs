using FluentValidation;
using MultiCreditCard.Wallets.Application.Commands;

namespace MultiCreditCard.Wallets.Application.Validators
{
    public class RequestCreditCardBuyValidator : AbstractValidator<RequestCreditCardBuyCommand>
    {
        public RequestCreditCardBuyValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Usuário deve ser preenchido.");

            RuleFor(x => x.AmountValue)
                .GreaterThan(0)
                .WithMessage("Valor de compra não deve ser zero.");
        }
    }
}
