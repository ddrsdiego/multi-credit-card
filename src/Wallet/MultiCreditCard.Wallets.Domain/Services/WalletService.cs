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

        public void AddNewCreditCart(Wallet wallet)
        {
            try
            {
                if (wallet == null)
                    throw new ArgumentNullException(nameof(wallet));

                if (wallet.CreditCards == null || !wallet.CreditCards.Any())
                    throw new ArgumentNullException(nameof(wallet.CreditCards));

                wallet.CreditCards.ToList().ForEach(creditCard =>
                {
                    _creditCardRepository.Create(creditCard);
                    _walletRepository.AddNewCreditCart(wallet, creditCard);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Buy(Wallet wallet)
        {
            try
            {
                if (wallet == null)
                    throw new ArgumentNullException(nameof(wallet));

                if (wallet.CreditCards == null || !wallet.CreditCards.Any())
                    throw new ArgumentNullException(nameof(wallet.CreditCards));

                _creditCardRepository.UpdateCreditCardLimit(wallet.CreditCards.ToList());
            }
            catch (Exception)
            {

                throw;
            }
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
