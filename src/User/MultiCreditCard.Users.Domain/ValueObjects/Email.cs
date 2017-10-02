using System;
using System.Text.RegularExpressions;

namespace MultiCreditCard.Users.Domain.ValueObjects
{
    public class Email
    {
        private const string EMAIL_REGEX = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public Email(string eletronicAddress)
        {
            if (string.IsNullOrEmpty(eletronicAddress))
                throw new ArgumentException(nameof(eletronicAddress));

            if (!Regex.IsMatch(eletronicAddress, EMAIL_REGEX, RegexOptions.IgnoreCase))
                throw new ArgumentException($"Email em formato inválido {nameof(eletronicAddress)}");

            EletronicAddress = eletronicAddress;
        }

        public string EletronicAddress { get; private set; }
    }
}
