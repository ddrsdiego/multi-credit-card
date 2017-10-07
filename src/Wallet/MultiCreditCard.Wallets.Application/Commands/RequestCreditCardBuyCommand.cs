using MediatR;
using MultiCreditCard.Wallets.Application.Reponse;

namespace MultiCreditCard.Wallets.Application.Commands
{
    public class RequestCreditCardBuyCommand : IRequest<RequestCreditCardBuyResponse>
    {
        public string UserId { get; set; }
        public decimal AmountValue { get; set; }
        public RequestCreditCardBuyResponse Response { get; set; } = new RequestCreditCardBuyResponse();
    }
}
