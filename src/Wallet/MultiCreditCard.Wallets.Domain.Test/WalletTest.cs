using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using MultiCreditCard.Wallets.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Services;
using System;
using System.Linq;
using Xunit;

namespace MultiCreditCard.Wallets.Domain.Test
{
    public class WalletTest
    {
        [Fact]
        public void Buy_Fail_Without_CreditCards()
        {
            var user = GetUser();

            var wallet = new Wallet(user);

            wallet.AddNewCreditCart(new CreditCard(user, CreditCardType.Visa, 4539012657749922, "MELISSA DAVIDSON", "01/18", 15, "669", 1000));
            wallet.AddNewCreditCart(new CreditCard(user, CreditCardType.AmericanExpress, 344241982621208, "MELISSA DAVIDSON", "05/08", 4, "949", 100));
            wallet.AddNewCreditCart(new CreditCard(user, CreditCardType.Visa, 4532692653021082, "MELISSA DAVIDSON", "10/19", 4, "647", 100));

            Assert.Throws<InvalidOperationException>(() => wallet.Buy(1300));
        }

        [Fact]
        public void Buy_Fail_Without_Credit()
        {
            var user = GetUser();

            var vidaCreditCard = new CreditCard(user, CreditCardType.Visa, 4539012657749922, "MELISSA DAVIDSON", "01/18", 15, "669", 1000);
            var americanCreditCard = new CreditCard(user, CreditCardType.AmericanExpress, 344241982621208, "MELISSA DAVIDSON", "05/08", 4, "949", 100);
            var masterCreditCard = new CreditCard(user, CreditCardType.Visa, 4532692653021082, "MELISSA DAVIDSON", "10/19", 4, "647", 100);

            var wallet = new Wallet(user);

            wallet.AddNewCreditCart(vidaCreditCard);
            wallet.AddNewCreditCart(americanCreditCard);
            wallet.AddNewCreditCart(masterCreditCard);

            Assert.Throws<InvalidOperationException>(() => wallet.Buy(1300));
        }

        [Fact]
        public void Buy_Success_With_CreditCard_LongestPayDay()
        {
            var user = GetUser();

            var vidaCreditCard = new CreditCard(user, CreditCardType.Visa, 4539012657749922, "MELISSA DAVIDSON", "01/18", 15, "669", 1000);
            var americanCreditCard = new CreditCard(user, CreditCardType.AmericanExpress, 344241982621208, "MELISSA DAVIDSON", "05/08", 4, "949", 100);
            var masterCreditCard = new CreditCard(user, CreditCardType.Visa, 4532692653021082, "MELISSA DAVIDSON", "10/19", 4, "647", 100);

            var wallet = new Wallet(user);

            wallet.AddNewCreditCart(vidaCreditCard);
            wallet.AddNewCreditCart(americanCreditCard);
            wallet.AddNewCreditCart(masterCreditCard);

            wallet.Buy(1000);
            var creditCard = wallet.CreditCards
                                .First(x => x.CreditCardNumber == 4539012657749922
                                        && x.CreditCardType == CreditCardType.Visa);

            Assert.Equal(0, creditCard.CreditLimit);
        }

        [Fact]
        public void Buy_Success_With_CreditCard_MoreThanOneCard()
        {
            var user = GetUser();

            var vidaCreditCard = new CreditCard(user, CreditCardType.Visa, 4539012657749922, "MELISSA DAVIDSON", "01/18", 15, "669", 1000);
            var americanCreditCard = new CreditCard(user, CreditCardType.AmericanExpress, 344241982621208, "MELISSA DAVIDSON", "05/08", 4, "949", 100);
            var masterCreditCard = new CreditCard(user, CreditCardType.Visa, 4532692653021082, "MELISSA DAVIDSON", "10/19", 4, "647", 250);

            var wallet = new Wallet(user);

            wallet.AddNewCreditCart(vidaCreditCard);
            wallet.AddNewCreditCart(americanCreditCard);
            wallet.AddNewCreditCart(masterCreditCard);

            wallet.Buy(1150);

            Assert.Equal(0, wallet.CreditCards.First(x => x.CreditCardNumber == 4539012657749922 && x.CreditCardType == CreditCardType.Visa).CreditLimit);
            Assert.Equal(0, wallet.CreditCards.First(x => x.CreditCardNumber == 344241982621208 && x.CreditCardType == CreditCardType.AmericanExpress).CreditLimit);
            Assert.Equal(200, wallet.CreditCards.First(x => x.CreditCardNumber == 4532692653021082 && x.CreditCardType == CreditCardType.Visa).CreditLimit);
            Assert.Equal(200, wallet.MaximumCreditLimit);
        }

        private static User GetUser()
        {
            var email = new Email("mariafernandesalves@armyspy.com");
            var password = new Password("aeph9rukuno");
            return new User("Maria Fernandes Alves", 30313527504, email.EletronicAddress, password.Encoded);
        }
    }
}
