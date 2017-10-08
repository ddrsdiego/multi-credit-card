using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using System;
using Xunit;

namespace MultiCreditCard.CreditCards.Domain.Test
{
    public class CreditCardTest
    {
        [Fact]
        public void Debit_Fail_CreditLimit_Invalid()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(GetUser(), CreditCardType.Visa, 5212410848753186, "LARA CAVALCANTI", "06/19", 5, "946", 0m));
        }

        [Fact]
        public void Debit_DebitCreditLimit_Success()
        {
            var creditCard = new CreditCard(GetUser(), CreditCardType.Visa, 5212410848753186, "LARA CAVALCANTI", "06/19", 5, "946", 3520.00m);
            creditCard.Debit(1020);

            Assert.Equal(2500, creditCard.CreditLimit);
        }

        [Fact]
        public void Debit_DebitAllCreditLimit_Success()
        {
            var creditCard = new CreditCard(GetUser(), CreditCardType.Visa, 5212410848753186, "LARA CAVALCANTI", "06/19", 5, "946", 3520.00m);
            creditCard.Debit(3520.00m);

            Assert.Equal(0, creditCard.CreditLimit);
        }

        private static User GetUser()
        {
            var email = new Email("mariafernandesalves@armyspy.com");
            var password = new Password("aeph9rukuno");
            return new User("Maria Fernandes Alves", 30313527504, email.EletronicAddress, password.Encoded);
        }
    }
}
