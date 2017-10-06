using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiCreditCard.Wallets.Domain.Entities
{
    public class Wallet
    {
        private IList<CreditCard> _creditCards;

        protected Wallet()
        {
        }

        public Wallet(User user)
        {
            User = user;
            WalletId = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }

        public string WalletId { get; private set; }

        public decimal AvailableCredit { get; set; }

        public decimal MaximumCreditLimit
        {
            get
            {
                return _creditCards.Sum(x => x.CreditLimit);
            }
        }

        public decimal UserCreditLimit { get; private set; }

        public User User { get; private set; }

        public ICollection<CreditCard> CreditCards
        {
            get { return _creditCards; }
            set { _creditCards = new List<CreditCard>(value); }
        }

        public DateTime CreationDate { get; private set; }

        public DateTime UpdateDate { get; set; }

        public void AddNewCreditCart(CreditCard newCreditCard)
        {
            if (_creditCards == null)
                _creditCards = new List<CreditCard>();

            if (_creditCards.Any(x => x.CreditCardNumber == newCreditCard.CreditCardNumber && x.CreditCardType == newCreditCard.CreditCardType))
                throw new InvalidOperationException($"Cartão de Crédito {newCreditCard.CreditCardNumber} já adicionado a carteira.");

            UpdateDate = DateTime.Now;
            _creditCards.Add(newCreditCard);

        }

        public void RemoveCreditCard(CreditCard creditCard)
        {
            if (!_creditCards.Any(x => x.CreditCardNumber == creditCard.CreditCardNumber))
                throw new InvalidOperationException($"Cartão de Crédito {creditCard.CreditCardNumber} já removido da carteira.");

            UpdateDate = DateTime.Now;
            _creditCards.Remove(creditCard);
        }

        public void UpdateUserCreditLimit(decimal newUserCreditLimit)
        {
            if (newUserCreditLimit > MaximumCreditLimit)
                throw new InvalidOperationException($"Cartão de Crédito {newUserCreditLimit} já removido da carteira.");

            UpdateDate = DateTime.Now;
            UserCreditLimit = newUserCreditLimit;
        }

        public static Wallet DefaultEntity() => new Wallet();
    }
}