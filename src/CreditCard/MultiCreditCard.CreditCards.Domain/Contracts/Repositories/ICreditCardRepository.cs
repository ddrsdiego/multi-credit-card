using MultiCreditCard.CreditCards.Domain.Entities;

namespace MultiCreditCard.CreditCards.Domain.Contracts.Repositories
{
    public interface ICreditCardRepository
    {
        void UpdateCreditCardLimit(CreditCard creditCard);
    }
}
