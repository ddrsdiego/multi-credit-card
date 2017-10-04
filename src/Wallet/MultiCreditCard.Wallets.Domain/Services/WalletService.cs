using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Linq;
using MultiCreditCard.Users.Domain.Entities;

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

        public void Buy(Wallet wallet, decimal valueBuy)
        {
            var purchaseDate = DateTime.Now.Day;

            if (!wallet.CreditCards.Any())
                throw new InvalidOperationException("Nenhum cartão de crédito na carteira para realizar a compra.");

            if (valueBuy > wallet.MaximumCreditLimit)
                throw new InvalidOperationException($"Não há saldo suficiente na carteira para realizar a compra.");

            var cardForBuy = GetCardForBuy(wallet);

            if (cardForBuy != null && cardForBuy.CreditLimit >= valueBuy)
                cardForBuy.Debit(valueBuy);
            else
            {
                BuyWithMoreThaOneCard(wallet, cardForBuy, valueBuy);
            }
        }

        public void AddNewCreditCart(Wallet wallet)
        {
            _walletRepository.AddNewCreditCart(wallet);
        }

        public void RemoveCreditCart(Wallet wallet)
        {
            _walletRepository.RemoveCreditCart(wallet);
        }

        public void UpdateUserCreditLimit(Wallet wallet)
        {
            _walletRepository.UpdateUserCreditLimit(wallet);
        }

        public void CreateWallet(User user)
        {
            var newWallet = new Wallet(user);
            _walletRepository.CreateWallet(newWallet);
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
                            .OrderByDescending(creditCard => creditCard.PayDay)
                            .FirstOrDefault();
        }

        private static CreditCard GetNearCardByPayDay(Wallet wallet)
        {
            var card = (from creditCard in wallet.CreditCards
                        group creditCard by creditCard.PayDay into grp
                        select grp.FirstOrDefault(x => x.CreditCardNumber > 0))
                        .FirstOrDefault();

            return card;
        }

        private void BuyWithMoreThaOneCard(Wallet wallet, CreditCard creditCard, decimal valueBuy)
        {
            var creditLimit = creditCard.CreditLimit;
            var diffValue = valueBuy - creditLimit;

            creditCard.Debit(creditLimit);
            Buy(wallet, diffValue);
        }

    }
}
