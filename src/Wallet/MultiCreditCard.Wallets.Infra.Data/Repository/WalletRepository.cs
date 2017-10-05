using Dapper;
using Microsoft.Extensions.Configuration;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Entities;
using MultiCreditCard.Wallets.Infra.Data.Statement;
using System;
using System.Data.SqlClient;
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

        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
                {
                    var parameter = new
                    {
                        WalletId = wallet.Id,
                        UserId = wallet.User.Id,
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
            var wallet = Wallet.DefaultEntity();

            using (SqlConnection conn = new SqlConnection(_configuracoes.GetConnectionString("MultCreditCard")))
            {
                wallet = await conn.QueryFirstOrDefaultAsync<Wallet>(WalletStatement.GetWalletByUserId, new { UserId = userId });
            }

            return wallet;
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
                        WalletId = wallet.Id
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