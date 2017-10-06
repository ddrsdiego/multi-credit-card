using Dapper;
using Microsoft.Extensions.Configuration;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Entities;
using MultiCreditCard.Wallets.Infra.Data.Statement;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Infra.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IConfiguration _configuracoes;

        public WalletRepository(IConfiguration configuracoes)
        {
            _configuracoes = configuracoes;
        }

        public void AddNewCreditCart(Wallet wallet)
        {
            using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
            {
                wallet.CreditCards.ToList().ForEach(creditCard =>
                {
                    CreateNewCreditCard(creditCard, conn);
                    conn.Execute(WalletStatement.AddNewCreditCart, new { walletId = wallet.WalletId, creditCardNumber = creditCard.CreditCardNumber });
                });

                var userCreditLimit = wallet.CreditCards.Sum(x => x.CreditLimit);
                conn.Execute("UPDATE WALLETS SET UserCreditLimit = @userCreditLimit, UpdateDate = GETDATE() where WalletId = @WalletId", new { userCreditLimit = userCreditLimit, walletId = wallet.WalletId });
            }
        }

        private static void CreateNewCreditCard(CreditCard creditCard, SqlConnection conn)
        {
            var parameters = new
            {
                userId = creditCard.User.UserId,
                creditCardNumber = creditCard.CreditCardNumber,
                creditCardType = (int)creditCard.CreditCardType,
                printedName = creditCard.PrintedName,
                payDay = creditCard.PayDay,
                expirationDate = creditCard.ExpirationDate,
                creditLimit = creditCard.CreditLimit,
                cVV = creditCard.CVV,
                createDate = creditCard.CreateDate
            };

            conn.Execute(WalletStatement.CreateNewCreditCard, parameters);
        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    var parameter = new
                    {
                        WalletId = wallet.WalletId,
                        UserId = wallet.User.UserId,
                        UserCreditLimit = wallet.UserCreditLimit,
                        CreationDate = wallet.CreationDate,
                    };

                    await conn.ExecuteAsync(WalletStatement.CreateWallet, parameter);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar a carteira para o usuário {wallet.User.UserName}. Erro: {ex.Message}");
            }
        }

        public async Task<Wallet> GetWalletByUserId(string userId)
        {
            var walletResult = Wallet.DefaultEntity();

            using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
            {
                using (var multi = conn.QueryMultiple(WalletStatement.GetWalletByUserId + WalletStatement.GetUserByUserId + WalletStatement.GetCreditCardByUserId, new { userId = userId }))
                {
                    walletResult = multi.Read<Wallet>().First();

                    var user = multi.Read<User>().First();
                    var creditCards = multi.Read<CreditCard>().ToList();

                    if (creditCards != null && creditCards.Any())
                    {
                        creditCards.ForEach(c => 
                        {
                            c.User = user;
                            walletResult.AddNewCreditCart(c);
                        });
                    }
                }
            }

            return walletResult;
        }

        public void RemoveCreditCart(Wallet wallet)
        {

        }

        public void UpdateUserCreditLimit(Wallet wallet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    var parameters = new
                    {
                        UserCreditLimit = wallet.UserCreditLimit,
                        WalletId = wallet.WalletId
                    };
                    conn.ExecuteAsync(WalletStatement.UpdateUserCreditLimit, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualiza o limite da carteira. Erro: {ex.Message}");
            }
        }
    }
}