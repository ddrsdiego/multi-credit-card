using MultiCreditCard.Users.Domain.ValueObjects;
using System;

namespace MultiCreditCard.Users.Domain.Entities
{
    public class User
    {
        protected User()
        {

        }

        public User(string userName, decimal documentNumber, string email, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException(nameof(userName));

            UserId = Guid.NewGuid().ToString();
            UserName = userName;
            DocumentNumber = documentNumber;
            Email = email;
            Password = password;
            CreationDate = DateTime.Now;
        }

        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public decimal DocumentNumber { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; private set; }

        public static User DefaultEntity() => new User();
    }
}
