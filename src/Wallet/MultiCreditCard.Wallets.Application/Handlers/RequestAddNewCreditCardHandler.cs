using MediatR;
using MultiCreditCard.Users.Application.Commands;
using MultiCreditCard.Users.Application.Reponse;
using System;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Application.Handlers
{
    public class RequestAddNewCreditCardHandler : IAsyncRequestHandler<RequestAddNewCreditCardCommand, RequestAddNewCreditCardResponse>
    {
        public RequestAddNewCreditCardHandler()
        {
        }

        public Task<RequestAddNewCreditCardResponse> Handle(RequestAddNewCreditCardCommand message)
        {
            throw new NotImplementedException();
        }
    }
}
