using MultiCreditCard.CreditCards.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCreditCard.CreditCards.Domain.Contracts.Service
{
    public interface ICreditCardService
    {
        void UpdateCreditCardLimit(CreditCard creditCard);
    }
}
