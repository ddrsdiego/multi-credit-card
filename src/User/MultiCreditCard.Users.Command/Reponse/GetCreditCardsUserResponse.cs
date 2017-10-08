using MultiCreditCard.Application.Common;
using MultiCreditCard.CreditCards.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiCreditCard.Users.Command.Reponse
{
    public class GetCreditCardsUserResponse : Response
    {
        public string UserId { get; set; }

        public decimal CreditLimitWallet
        {
            get { return CreditCards.Sum(x => x.CreditLimit); }
        }

        public DateTime DataTimeQuery { get; set; }
        public List<CreditCardsResponse> CreditCards { get; set; } = new List<CreditCardsResponse>();
    }

    public class CreditCardsResponse
    {
        public decimal CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public decimal CreditLimit { get; set; }
        public int PayDay { get; set; }
        public string ExpirationDate { get; set; }
    }
}
