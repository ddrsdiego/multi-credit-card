using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Entities;

namespace MultiCreditCard.Wallets.Domain.Contracts.Repositories
{
    public interface IWalletRepository
    {
        void CreateWallet(Wallet wallet);

        void AddNewCreditCart(Wallet wallet);

        void RemoveCreditCart(Wallet wallet);

        void UpdateUserCreditLimit(Wallet wallet);
    }
}
