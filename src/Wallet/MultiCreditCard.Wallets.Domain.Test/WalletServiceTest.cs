using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace MultiCreditCard.Wallets.Domain.Test
{
    public class WalletServiceTest
    {
        [Fact]
        public void Should_Fail_Buy_Creditcards_Null()
        {
            var walletRepository = Substitute.For<IWalletRepository>();
            var creditCardRepository = Substitute.For<ICreditCardRepository>();

            var wallet = new Wallet(GetUser());

            var walletService = new WalletService(walletRepository, creditCardRepository);
            Assert.Throws<ArgumentNullException>(() => walletService.Buy(wallet));
        }

        [Fact]
        public void Should_Fail_Buy_Creditcards_Empty()
        {
            var walletRepository = Substitute.For<IWalletRepository>();
            var creditCardRepository = Substitute.For<ICreditCardRepository>();

            var wallet = new Wallet(GetUser())
            {
                CreditCards = new List<CreditCard>()
            };

            var walletService = new WalletService(walletRepository, creditCardRepository);
            Assert.Throws<ArgumentNullException>(() => walletService.Buy(wallet));
        }

        [Fact]
        public void Should_Buy_Success_With_Creditcards()
        {
            var walletRepository = Substitute.For<IWalletRepository>();
            var creditCardRepository = Substitute.For<ICreditCardRepository>();

            var user = GetUser();

            var wallet = new Wallet(user)
            {
                CreditCards = new List<CreditCard>()
            };

            wallet.AddNewCreditCart(new CreditCard(user, CreditCardType.Visa, 4539012657749922, "MARIA FERNANDES ALVES", "01/18", 3, "669", 400));
            wallet.AddNewCreditCart(new CreditCard(user, CreditCardType.AmericanExpress, 344241982621208, "MARIA FERNANDES ALVES", "05/08", 15, "949", 100));

            walletRepository.UpdateUserCreditLimit(wallet);

            var walletService = new WalletService(walletRepository, creditCardRepository);
            walletService.Buy(wallet);

            walletRepository.Received().UpdateUserCreditLimit(Arg.Any<Wallet>());
        }

        private static User GetUser()
        {
            var email = new Email("mariafernandesalves@armyspy.com");
            var password = new Password("aeph9rukuno");
            return new User("Maria Fernandes Alves", 30313527504, email.EletronicAddress, password.Encoded);
        }
    }
}
