using System;

namespace MultiCreditCard.CreditCards.Domain.Entities
{
    public class CreditCard
    {
        public CreditCard()
        {

        }

        public decimal Number { get; set; }
        public string PrintedName { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal CreditLimit { get; set; }
        public string Cvv { get; set; }
    }
}
