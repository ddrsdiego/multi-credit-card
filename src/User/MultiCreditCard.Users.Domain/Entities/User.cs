using MultiCreditCard.Users.Domain.ValueObjects;
using System;

namespace MultiCreditCard.Users.Domain.Entities
{
    public class User
    {
        public User(string userName, decimal documentNumber, Email email, Password password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException(nameof(userName));

            UserName = userName;
            DocumentNumber = documentNumber;
            Email = email;
            Password = password;
            CreationDate = DateTime.Now;
        }

        //public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public decimal DocumentNumber { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
