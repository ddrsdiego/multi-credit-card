using System;

namespace MultiCreditCard.Domain.Entities
{
    public class CreditCard
    {
        public CreditCard()
        {

        }

        public CreditCard(string cardNumber, DateTime maturityDate, DateTime expirationDate, string printedName, string cvv, decimal creditLimit)
        {
            CardNumber = cardNumber;
            MaturityDate = maturityDate;
            ExpirationDate = expirationDate;
            PrintedName = printedName;
            CVV = cvv;
            CreditLimit = creditLimit;
        }

        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public string PrintedName { get; set; }
        public string CVV { get; set; }
        public decimal CreditLimit { get; set; }
    }
}