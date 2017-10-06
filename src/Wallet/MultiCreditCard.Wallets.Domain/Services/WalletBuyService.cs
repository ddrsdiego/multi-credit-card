using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Linq;

namespace MultiCreditCard.Wallets.Domain.Services
{
    public static class WalletBuyService
    {
        public static void Buy(this Wallet wallet, decimal amount)
        {
            var purchaseDate = DateTime.Now.Day;

            if (!wallet.CreditCards.Any())
                throw new InvalidOperationException("Nenhum cartão de crédito na carteira para realizar a compra.");

            if (amount > wallet.MaximumCreditLimit)
                throw new InvalidOperationException($"Não há saldo suficiente na carteira para realizar a compra.");

            var cardForBuy = GetCardForBuy(wallet);

            if (cardForBuy != null && cardForBuy.CreditLimit >= amount)
                cardForBuy.Debit(amount);
            else
            {
                BuyWithMoreThaOneCard(wallet, cardForBuy, amount);
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
                                where creditCard.CreditLimit > 0
                                orderby creditCard.PayDay ascending
                                group creditCard by creditCard.PayDay into grp
                                select grp.FirstOrDefault()).FirstOrDefault();

            return creditCardResult;
        }

        private static void BuyWithMoreThaOneCard(Wallet wallet, CreditCard creditCard, decimal valueBuy)
        {
            var creditLimit = creditCard.CreditLimit;
            var diffValue = valueBuy - creditLimit;

            creditCard.Debit(creditLimit);
            Buy(wallet, diffValue);
        }
    }
}
