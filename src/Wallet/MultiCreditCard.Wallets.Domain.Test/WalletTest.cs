using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using MultiCreditCard.Wallets.Domain.Services;
using MultiCreditCard.Wallets.Domain.Entities;
using Xunit;

namespace MultiCreditCard.Wallets.Domain.Test
{
    public class WalletTest
    {
        [Fact]
        public void Test1()
        {
            var user = GetUser();

            var vidaCreditCard = new CreditCard(user,CreditCardType.Visa, 4539012657749922, "MELISSA DAVIDSON", "01/18", 15, "669", 0);
            var americanCreditCard = new CreditCard(user, CreditCardType.AmericanExpress, 344241982621208, "MELISSA DAVIDSON", "05/08", 4, "949", 1250);
            var masterCreditCard = new CreditCard(user, CreditCardType.Visa, 4532692653021082, "MELISSA DAVIDSON", "10/19", 4, "647", 1250);

            var wallet = new Wallet(user);

            wallet.AddNewCreditCart(vidaCreditCard);
            wallet.AddNewCreditCart(americanCreditCard);
            wallet.AddNewCreditCart(masterCreditCard);

            wallet.Buy(1300);
        }

        private static User GetUser()
        {
            var email = new Email("mariafernandesalves@armyspy.com");
            var password = new Password("aeph9rukuno");
            return new User("Maria Fernandes Alves", 30313527504, email.EletronicAddress, password.Encoded);
        }
    }
}
