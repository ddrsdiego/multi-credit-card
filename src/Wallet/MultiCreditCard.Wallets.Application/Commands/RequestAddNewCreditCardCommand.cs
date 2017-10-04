using MediatR;
using MultiCreditCard.Users.Application.Reponse;

namespace MultiCreditCard.Users.Application.Commands
{
    public class RequestAddNewCreditCardCommand : IRequest<RequestAddNewCreditCardResponse>
    {
    }
}
