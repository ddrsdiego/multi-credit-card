using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using MultiCreditCard.Wallets.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Services;
using Xunit;

namespace MultiCreditCard.Wallets.Domain.Test
{
    public class WalletServiceTest
    {
        [Fact]
        public void TesteMaisUm()
        {
            var user = GetUser();

            var vidaCreditCard = new CreditCard(CreditCardType.Visa, 4539012657749922, "MELISSA DAVIDSON", "01/18", 3, "669", 400);
            var americanCreditCard = new CreditCard(CreditCardType.AmericanExpressa, 344241982621208, "MELISSA DAVIDSON", "05/08", 15, "949", 100);
            //var masterCreditCard = new CreditCard(CreditCardType.Visa, 4532692653021082, "MELISSA DAVIDSON", "10/19", 15, "647", 2000);

            var wallet = new Wallet(user);

            wallet.AddNewCreditCart(vidaCreditCard);
            wallet.AddNewCreditCart(americanCreditCard);

            var walletService = new WalletService(null, null);
            walletService.Buy(wallet, 450);
        }

        private static User GetUser()
        {
            var email = new Email("mariafernandesalves@armyspy.com");
            var password = new Password("aeph9rukuno");
            return new User("Maria Fernandes Alves", 30313527504, email, password);
        }
    }
}
