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
        private readonly IConfiguration _config;

        public WalletRepository(IConfiguration configuracoes)
        {
            _config = configuracoes;
        }

        public void AddNewCreditCart(Wallet wallet, CreditCard creditCard)
        {
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
            {
                conn.Execute(WalletStatement.AddNewCreditCart, new { walletId = wallet.WalletId, CreditCardId = creditCard.CreditCardId });

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
                createDate = creditCard.CreateDate,
                Enable = creditCard.Enable
            };

            conn.Execute(WalletStatement.CreateNewCreditCard, parameters);
        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
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

            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
            {
                using (var multi = await conn.QueryMultipleAsync(WalletStatement.GetWalletByUserId, new { userId = userId }))
                {
                    var user = multi.Read<User>().First();
                    var wallet = walletResult = multi.Read<Wallet>().First();
                    var creditCards = multi.Read<CreditCard>().ToList();

                    if (creditCards != null && creditCards.Any())
                    {
                        creditCards.ForEach(c =>
                        {
                            c.User = user;
                            wallet.AddNewCreditCart(c);
                        });
                    }

                    wallet.User = user;
                    walletResult = wallet;
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
                using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("MultCreditCard")))
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