using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Infra.Data
{
    public class WalletRepository : IWalletRepository
    {
        public void AddNewCreditCart(Wallet wallet)
        {

        }


        public async Task CreateWalletAsync(Wallet wallet)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar a carteira para o usuário {wallet.User.UserName}");
            }
        }

        public void RemoveCreditCart(Wallet wallet)
        {

        }

        public void UpdateUserCreditLimit(Wallet wallet)
        {

        }
    }
}