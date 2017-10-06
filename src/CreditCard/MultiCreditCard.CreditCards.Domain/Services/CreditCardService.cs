using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Domain.Contracts.Service;
using MultiCreditCard.CreditCards.Domain.Entities;
using System;

namespace MultiCreditCard.CreditCards.Domain.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardService(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public void UpdateCreditCardLimit(CreditCard creditCard)
        {
            try
            {
                _creditCardRepository.UpdateCreditCardLimit(creditCard);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
