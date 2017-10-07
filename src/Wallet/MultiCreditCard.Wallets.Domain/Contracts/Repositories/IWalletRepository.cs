using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Entities;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Domain.Contracts.Repositories
{
    public interface IWalletRepository
    {
        Task CreateWalletAsync(Wallet wallet);

        Task<Wallet> GetWalletByUserId(string userId);

        void AddNewCreditCart(Wallet wallet, CreditCard creditCard);

        void RemoveCreditCart(Wallet wallet);

        void UpdateUserCreditLimit(Wallet wallet);
    }
}
