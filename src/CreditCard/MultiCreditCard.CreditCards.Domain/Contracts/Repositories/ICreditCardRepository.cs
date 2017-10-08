using MultiCreditCard.CreditCards.Domain.Entities;
using System.Collections.Generic;

namespace MultiCreditCard.CreditCards.Domain.Contracts.Repositories
{
    public interface ICreditCardRepository
    {
        void Create(CreditCard creditCard);

        void UpdateCreditCardLimit(List<CreditCard> creditCards);

        void UpdateCreditCardLimit(CreditCard creditCard);

        IEnumerable<dynamic> GetCreditCardsUser(string userId);

        IList<CreditCard> GetCreditCardByUserId(string userId);
    }
}
