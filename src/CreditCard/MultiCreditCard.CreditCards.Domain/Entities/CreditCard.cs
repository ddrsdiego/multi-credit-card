using MultiCreditCard.CreditCards.Domain.Enums;
using System;

namespace MultiCreditCard.CreditCards.Domain.Entities
{
    public class CreditCard
    {
        public CreditCard()
        {

        }

        public CreditCard(CreditCardType creditCardType, decimal creditCardNumber, string printedName, string expirationDate, int payDay, string cvv, decimal creditLimit)
        {
            CreditCardType = creditCardType;
            CreditCardNumber = creditCardNumber;
            PrintedName = printedName;
            ExpirationDate = expirationDate;
            CVV = cvv;
            CreditLimit = creditLimit;
            PayDay = payDay;
        }

        public decimal CreditCardNumber { get; private set; }
        public string PrintedName { get; private set; }
        public int PayDay { get; set; }
        public DateTime MaturityDate { get; set; }
        public string ExpirationDate { get; private set; }
        public decimal CreditLimit { get; set; }
        public string CVV { get; private set; }
        public CreditCardType CreditCardType { get; private set; }
    }
}
