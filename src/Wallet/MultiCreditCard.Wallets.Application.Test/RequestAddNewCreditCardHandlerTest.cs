using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Application.Commands;
using MultiCreditCard.Users.Application.Reponse;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using MultiCreditCard.Wallets.Application.Handlers;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Entities;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MultiCreditCard.Wallets.Application.Test
{
    public class RequestAddNewCreditCardHandlerTest
    {
        private IWalletService _walletService;
        private IUserRepository _userRepository;
        private IWalletRepository _walletRepository;
        private RequestAddNewCreditCardCommandBuild _commaBuild;

        [Fact]
        public void AddNewCreditCard_AddCreditCardToWallet_Success()
        {
            Initialize();

            _commaBuild = new RequestAddNewCreditCardCommandBuild();
            _commaBuild.UserId("7b5c4c7e-32db-4a47-94c8-e65fa1654c3b")
                        .CreditCardNumber(5212410848753186)
                            .CreditCardType(CreditCardType.Visa)
                                .PrintedName("LARA CAVALCANTI")
                                    .PayDay(5)
                                        .ExpirationDate("06/19")
                                            .CreditLimit(3520.00m)
                                                .CVV("946");

            _userRepository.GetUserByUserId(Arg.Any<string>()).Returns(x => { return GetUser(); });
            _walletRepository.GetWalletByUserId(Arg.Any<string>()).Returns(x => { return GetWallet(); });

            var service = new RequestAddNewCreditCardHandler(_userRepository, _walletRepository, _walletService);
            var response = service.Handle(_commaBuild.Build()).Result;

            Assert.IsType<RequestAddNewCreditCardResponse>(response);
            Assert.False(response.Errors.Any());
            Assert.True(response.Errors.Count() == 0);
        }

        [Fact]
        public void AddNewCreditCard_CommandInvalid_Fail()
        {
            Initialize();

            _commaBuild = new RequestAddNewCreditCardCommandBuild();
            _commaBuild.UserId("7b5c4c7e-32db-4a47-94c8-e65fa1654c3b")
                        .CreditCardType(CreditCardType.Visa)
                            .PrintedName("LARA CAVALCANTI")
                                .PayDay(5)
                                    .ExpirationDate("06/19")
                                        .CreditLimit(3520.00m)
                                            .CVV("946");

            _userRepository.GetUserByUserId(Arg.Any<string>()).Returns(x => { return GetUser(); });
            _walletRepository.GetWalletByUserId(Arg.Any<string>()).Returns(x => { return GetWallet(); });

            var command = _commaBuild.Build();
            var service = new RequestAddNewCreditCardHandler(_userRepository, _walletRepository, _walletService);
            var response = service.Handle(command).Result;

            Assert.IsType<RequestAddNewCreditCardResponse>(response);
            Assert.True(response.Errors.Any());
            Assert.True(response.Errors.Count() > 0);
        }

        [Fact]
        public void AddNewCreditCard_UserNotFound_Fail()
        {
            Initialize();

            _commaBuild = new RequestAddNewCreditCardCommandBuild();
            _commaBuild.UserId("7b5c4c7e-32db-4a47-94c8-e65fa1654c3b")
                        .CreditCardNumber(5212410848753186)
                            .CreditCardType(CreditCardType.Visa)
                                .PrintedName("LARA CAVALCANTI")
                                    .PayDay(5)
                                        .ExpirationDate("06/19")
                                            .CreditLimit(3520.00m)
                                                .CVV("946");

            _userRepository.GetUserByUserId(Arg.Any<string>()).Returns(x => { return User.DefaultEntity(); });
            _walletRepository.GetWalletByUserId(Arg.Any<string>()).Returns(x => { return GetWallet(); });

            var command = _commaBuild.Build();
            var service = new RequestAddNewCreditCardHandler(_userRepository, _walletRepository, _walletService);
            var response = service.Handle(command).Result;

            Assert.IsType<RequestAddNewCreditCardResponse>(response);
            Assert.True(response.Errors.Any());
            Assert.True(response.Errors.Count() > 0);
        }

        [Fact]
        public void AddNewCreditCard_WalletNotFound_Fail()
        {
            Initialize();

            _commaBuild = new RequestAddNewCreditCardCommandBuild();
            _commaBuild.UserId("7b5c4c7e-32db-4a47-94c8-e65fa1654c3b")
                        .CreditCardNumber(5212410848753186)
                            .CreditCardType(CreditCardType.Visa)
                                .PrintedName("LARA CAVALCANTI")
                                    .PayDay(5)
                                        .ExpirationDate("06/19")
                                            .CreditLimit(3520.00m)
                                                .CVV("946");

            _userRepository.GetUserByUserId(Arg.Any<string>()).Returns(x => { return GetUser(); });
            _walletRepository.GetWalletByUserId(Arg.Any<string>()).Returns(x => { return Wallet.DefaultEntity(); });

            var command = _commaBuild.Build();
            var service = new RequestAddNewCreditCardHandler(_userRepository, _walletRepository, _walletService);
            var response = service.Handle(command).Result;

            Assert.IsType<RequestAddNewCreditCardResponse>(response);
            Assert.True(response.Errors.Any());
            Assert.True(response.Errors.Count() > 0);
        }

        private static User GetUser()
        {
            return new User("LARA CAVALCANTI", 10883620448, new Email("lararibeirocavalcanti@jourrapide.com").EletronicAddress, new Password("1234mudar").Encoded);
        }

        private static Wallet GetWallet() => new Wallet(GetUser());

        private static List<CreditCard> GetCreditCards()
        {
            return new List<CreditCard>
                {
                    new CreditCard(GetUser(), CreditCardType.Mastercard, 5212410848753186, "LARA CAVALCANTI", "06/19", 5, "946", 3520.00m),
                    new CreditCard(GetUser(), CreditCardType.Mastercard, 5350172366189464, "LARA CAVALCANTI", "06/19", 5, "746", 1750.00m),
                };
        }

        private void Initialize()
        {
            _walletService = Substitute.For<IWalletService>();
            _userRepository = Substitute.For<IUserRepository>();
            _walletRepository = Substitute.For<IWalletRepository>();
        }
    }

    public class RequestAddNewCreditCardCommandBuild
    {
        private readonly RequestAddNewCreditCardCommand command;

        public RequestAddNewCreditCardCommandBuild()
        {
            command = new RequestAddNewCreditCardCommand();
        }

        public RequestAddNewCreditCardCommandBuild UserId(string userId)
        {
            command.UserId = userId;
            return this;
        }

        public RequestAddNewCreditCardCommandBuild CreditCardNumber(decimal creditCardNumber)
        {
            command.CreditCardNumber = creditCardNumber;
            return this;
        }

        public RequestAddNewCreditCardCommandBuild CreditCardType(CreditCardType creditCardType)
        {
            command.CreditCardType = creditCardType;
            return this;
        }

        public RequestAddNewCreditCardCommandBuild PrintedName(string printedName)
        {
            command.PrintedName = printedName;
            return this;
        }

        public RequestAddNewCreditCardCommandBuild PayDay(int payDay)
        {
            command.PayDay = payDay;
            return this;
        }

        public RequestAddNewCreditCardCommandBuild ExpirationDate(string expirationDate)
        {
            command.ExpirationDate = expirationDate;
            return this;
        }

        public RequestAddNewCreditCardCommandBuild CreditLimit(decimal creditLimit)
        {
            command.CreditLimit = creditLimit;
            return this;
        }

        public RequestAddNewCreditCardCommandBuild CVV(string cvv)
        {
            command.CVV = cvv;
            return this;
        }

        public RequestAddNewCreditCardCommand Build() => command;
    }
}