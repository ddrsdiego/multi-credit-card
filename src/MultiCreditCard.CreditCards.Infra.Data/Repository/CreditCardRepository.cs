using Dapper;
using Microsoft.Extensions.Configuration;
using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Infra.Data.Statement;
using System;
using System.Data.SqlClient;

namespace MultiCreditCard.CreditCards.Infra.Data.Repository
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly IConfiguration _config;

        public CreditCardRepository(IConfiguration config)
        {
            _config = config;
        }

        public void Create(CreditCard creditCard)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
                {
                    conn.Execute(CreditCardStatements.CreateNewCreditCard, new
                    {
                        userId = creditCard.User.UserId,
                        creditCardNumber = creditCard.CreditCardNumber,
                        creditCardType = (int)creditCard.CreditCardType,
                        printedName = creditCard.PrintedName,
                        payDay = creditCard.PayDay,
                        expirationDate = creditCard.ExpirationDate,
                        creditLimit = creditCard.CreditLimit,
                        cVV = creditCard.CVV,
                        createDate = creditCard.CreateDate,
                        Enable = creditCard.Enable
                    });
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao salvar o cartão de crédito {creditCard.CreditCardType} {creditCard.CreditCardNumber} {ex.Message}");
            }
        }

        public async void UpdateCreditCardLimit(CreditCard creditCard)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
                {
                    await conn.ExecuteAsync(CreditCardStatements.UpdateCreditCarLimit, new
                    {
                        userId = creditCard.User.UserId,
                        creditcardnumber = creditCard.CreditCardNumber,
                        creditcardtype = (int)creditCard.CreditCardType,
                        creditLimit = creditCard.CreditLimit
                    });
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar o limite do cartão de crédito {creditCard.CreditCardType} {creditCard.CreditCardNumber} {ex.Message}");
            }
        }
    }
}