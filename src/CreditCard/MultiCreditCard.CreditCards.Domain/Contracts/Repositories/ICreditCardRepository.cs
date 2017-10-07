using MultiCreditCard.CreditCards.Domain.Entities;

namespace MultiCreditCard.CreditCards.Domain.Contracts.Repositories
{
    public interface ICreditCardRepository
    {
        void Create(CreditCard creditCard);

        void UpdateCreditCardLimit(CreditCard creditCard);
    }
}
