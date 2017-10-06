using Dapper;
using Microsoft.Extensions.Configuration;
using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Infra.Data.Statement;
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

        public async void UpdateCreditCardLimit(CreditCard creditCard)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
            {
                var parameters = new
                {
                    creditcardnumber= creditCard.CreditCardNumber,
                    creditcardtype = (int)creditCard.CreditCardType,
                    creditLimit = creditCard.CreditLimit
                };
                await conn.ExecuteAsync(CreditCardStatements.UpdateCreditCarLimit, parameters);
            }
        }
    }
}