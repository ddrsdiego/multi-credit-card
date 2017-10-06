using MultiCreditCard.CreditCards.Domain.Contracts.Service;
using MultiCreditCard.CreditCards.Domain.Entities;
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
        private readonly ICreditCardService _creditCardService;

        public WalletService(IWalletRepository walletRepository, ICreditCardService creditCardService)
        {
            _walletRepository = walletRepository;
            _creditCardService = creditCardService;
        }

        public void Buy(Wallet wallet)
        {
            wallet.CreditCards.ToList().ForEach(creditCard =>
            {
                _creditCardService.UpdateCreditCardLimit(creditCard);
            });
        }

        public void AddNewCreditCart(Wallet wallet)
        {
            try
            {
                _walletRepository.AddNewCreditCart(wallet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveCreditCart(Wallet wallet)
        {
            try
            {
                _walletRepository.RemoveCreditCart(wallet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateUserCreditLimit(Wallet wallet)
        {
            try
            {
                _walletRepository.UpdateUserCreditLimit(wallet);
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

        private static CreditCard GetCardForBuy(Wallet wallet)
        {
            var moreThaOne = wallet.CreditCards.GroupBy(card => card.PayDay).Any(x => x.Count() > 1);

            var cardForBuy = moreThaOne ? GetNearCardByPayDay(wallet) : GetFirstCreditCard(wallet);

            return cardForBuy;
        }

        private static CreditCard GetFirstCreditCard(Wallet wallet)
        {
            return wallet.CreditCards
                            .Where(creditCard => creditCard.CreditLimit > 0)
                            .OrderByDescending(creditCard => creditCard.PayDay).FirstOrDefault();
        }

        private static CreditCard GetNearCardByPayDay(Wallet wallet)
        {
            var creditCardResult = CreditCard.DefaultEntity();

            creditCardResult = (from creditCard in wallet.CreditCards
                                group creditCard by creditCard.PayDay into grp
                                select grp.FirstOrDefault(x => x.CreditLimit > 0)).FirstOrDefault();

            return creditCardResult;
        }

        private void BuyWithMoreThaOneCard(Wallet wallet, CreditCard creditCard, decimal valueBuy)
        {
            var creditLimit = creditCard.CreditLimit;
            var diffValue = valueBuy - creditLimit;

            creditCard.Debit(creditLimit);
            //Buy(wallet, diffValue);
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
