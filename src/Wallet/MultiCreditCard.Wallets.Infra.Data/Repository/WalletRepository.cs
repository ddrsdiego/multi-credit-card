using Dapper;
using Microsoft.Extensions.Configuration;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Entities;
using MultiCreditCard.Wallets.Infra.Data.Statement;
using System;
using System.Collections.Generic;
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

                conn.Execute("UPDATE WALLETS SET UpdateDate = GETDATE() where WalletId = @WalletId", new { walletId = wallet.WalletId });
            }
        }

        private static void CreateNewCreditCard(CreditCard creditCard, SqlConnection conn)
        {
            var parameters = new
            {
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
                        AvailableCredit = wallet.AvailableCredit,
                        MaximumCreditLimit = wallet.MaximumCreditLimit,
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
                var walletDictionary = new Dictionary<string, Wallet>();

                walletResult = conn.Query<Wallet, CreditCard, Wallet>(WalletStatement.GetWalletByUserId,
                    (wallet, creditCard) =>
                    {
                        Wallet walletEntry;

                        if (!walletDictionary.TryGetValue(wallet.WalletId, out walletEntry))
                        {
                            walletEntry = wallet;
                            walletEntry.CreditCards = new List<CreditCard>();
                            walletDictionary.Add(walletEntry.WalletId, walletEntry);
                        }
                        walletEntry.CreditCards.Add(creditCard);

                        return walletEntry;
                    },
                    new { UserId = userId },
                    splitOn: "CreditCardNumber").FirstOrDefault();
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