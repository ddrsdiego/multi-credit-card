using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Domain.Entities;
using System;

namespace MultiCreditCard.CreditCards.Domain.Entities
{
    public class CreditCard
    {
        protected CreditCard()
        {

        }

        public CreditCard(User user, CreditCardType creditCardType, decimal creditCardNumber, string printedName, string expirationDate, int payDay, string cvv, decimal creditLimit)
        {
            if (creditLimit <= 0)
                throw new ArgumentException(nameof(creditLimit));

            CreditCardId = Guid.NewGuid().ToString();
            User = user;
            CreditCardType = creditCardType;
            CreditCardNumber = creditCardNumber;
            PrintedName = printedName;
            ExpirationDate = expirationDate;
            CVV = cvv;
            CreditLimit = creditLimit;
            PayDay = payDay;
            CreateDate = DateTime.Now;
            Enable = true;
        }

        public static CreditCard DefaultEntity() => new CreditCard();

        public string CreditCardId { get; private set; }
        public User User { get; set; }
        public decimal CreditCardNumber { get; private set; }
        public string PrintedName { get; private set; }
        public int PayDay { get; private set; }
        public DateTime MaturityDate { get; set; }
        public string ExpirationDate { get; private set; }
        public decimal CreditLimit { get; private set; }
        public string CVV { get; private set; }
        public CreditCardType CreditCardType { get; private set; }
        public DateTime CreateDate { get; private set; }
        public bool Enable { get; private set; }

        public void Debit(decimal value)
        {
            if (value > CreditLimit)
                throw new InvalidOperationException($"Não há saldo disponível no cartão {CreditCardNumber} para realizar a operação");

            CreditLimit = CreditLimit - value;
        }
    }
}
