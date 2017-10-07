using MediatR;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Application.Reponse;
using System;

namespace MultiCreditCard.Users.Application.Commands
{
    public class RequestAddNewCreditCardCommand : IRequest<RequestAddNewCreditCardResponse>
    {
        public string UserId { get; set; }
        public decimal CreditCardNumber { get; set; }
        public string PrintedName { get; set; }
        public int PayDay { get; set; }
        public DateTime MaturityDate { get; set; }
        public string ExpirationDate { get; set; }
        public decimal CreditLimit { get; set; }
        public string CVV { get; set; }
        public CreditCardType CreditCardType { get; set; }
        public RequestAddNewCreditCardResponse Response { get; set; } = new RequestAddNewCreditCardResponse();
    }
}