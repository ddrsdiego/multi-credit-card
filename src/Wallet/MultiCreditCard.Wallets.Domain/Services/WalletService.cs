using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Domain.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICreditCardRepository _creditCardRepository;

        public WalletService(IWalletRepository walletRepository, ICreditCardRepository creditCardRepository)
        {
            _walletRepository = walletRepository;
            _creditCardRepository = creditCardRepository;
        }

        public void Buy(Wallet wallet)
        {
            wallet.CreditCards.ToList().ForEach(creditCard =>
            {
                _creditCardRepository.UpdateCreditCardLimit(creditCard);
            });
        }

        public async Task CreateWalletAsync(User user)
        {
            var newWallet = new Wallet(user);

            try
            {
                await _walletRepository.CreateWalletAsync(newWallet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Wallet> GetWalletByUserId(string userId)
        {
            try
            {
                return await _walletRepository.GetWalletByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
