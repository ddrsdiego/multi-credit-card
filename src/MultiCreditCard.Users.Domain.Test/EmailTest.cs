using MultiCreditCard.Users.Domain.ValueObjects;
using NUnit.Framework;
using System;

namespace MultiCreditCard.Domain.Test
{
    [TestFixture]
    public class EmailTest
    {
        [Test]
        public void Should_Create_When_EmailAddress_Is_Valid()
        {
            var eletronicAddress = "ddrsdiego@hotmail.com";
            var email = new Email(eletronicAddress);

            Assert.NotNull(email);
            Assert.AreEqual(eletronicAddress, email.EletronicAddress);
        }

        [Test]
        public void Should_FailCreate_When_EmailAddress_Is_Empty()
        {
            var email = new Email(string.Empty);
        }
    }
}
