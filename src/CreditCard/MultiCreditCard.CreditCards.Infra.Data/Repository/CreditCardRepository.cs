using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Infra.Data.Statement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace MultiCreditCard.CreditCards.Infra.Data.Repository
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public CreditCardRepository(IConfiguration config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger<CreditCardRepository>();
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
                var msgError = $"Erro ao salvar o cartão de crédito {creditCard.CreditCardType} {creditCard.CreditCardNumber} {ex.Message}";

                _logger.LogError(ex, msgError);
                throw new InvalidOperationException(msgError);
            }
        }

        public IList<CreditCard> GetCreditCardByUserId(string userId)
        {
            var result = new List<CreditCard>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
                {
                    result = (List<CreditCard>)conn.Query<CreditCard>(CreditCardStatements.GetCreditCardsUser, new { userId = userId });
                }
                return result;
            }
            catch (Exception ex)
            {
                var msgError = $"Erro ao consultar cartões de crédito {ex.Message}";

                _logger.LogError(ex, msgError);
                throw new InvalidOperationException(msgError);
            }
        }

        public IEnumerable<dynamic> GetCreditCardsUser(string userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
                {
                    var result = conn.Query(CreditCardStatements.GetCreditCardsUser, new { userId = userId });

                    return result;
                }
            }
            catch (Exception ex)
            {
                var msgError = $"Erro ao consultar cartões de crédito {ex.Message}";

                _logger.LogError(ex, msgError);
                throw new InvalidOperationException(msgError);
            }
        }

        public async void UpdateCreditCardLimit(List<CreditCard> creditCards)
        {
            try
            {

                var filtroXml = new XElement(nameof(creditCards),
                        from creditCard in creditCards
                        select new XElement(nameof(CreditCard),
                            new XAttribute(nameof(creditCard.User.UserId), creditCard.User.UserId),
                            new XAttribute(nameof(creditCard.CreditCardNumber), creditCard.CreditCardNumber),
                            new XAttribute(nameof(creditCard.CreditCardType), (int)creditCard.CreditCardType),
                            new XAttribute(nameof(creditCard.CreditLimit), creditCard.CreditLimit)));

                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
                {
                    await conn.ExecuteAsync(CreditCardStatements.CreateNewCreditCard, new { xml = filtroXml.ToString() });
                }
            }
            catch (Exception ex)
            {
                var msgError = $"Erro ao atualizar o limite do cartão de crédito {ex.Message}";

                _logger.LogError(ex, msgError);
                throw new InvalidOperationException(msgError);
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
                var msgError = $"Erro ao atualizar o limite do cartão de crédito {creditCard.CreditCardType} {creditCard.CreditCardNumber} {ex.Message}";

                _logger.LogError(ex, msgError);
                throw new InvalidOperationException(msgError);
            }
        }
    }
}