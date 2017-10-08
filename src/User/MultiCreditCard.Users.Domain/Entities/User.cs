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

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException(nameof(email));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException(nameof(password));

            if (documentNumber <= 0)
                throw new ArgumentException(nameof(documentNumber));

            UserName = userName;
            DocumentNumber = documentNumber;
            Email = email;
            Password = password;
        }

        public string UserId { get; private set; } = Guid.NewGuid().ToString();
        public string UserName { get; private set; }
        public decimal DocumentNumber { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; private set; } = DateTime.Now;

        public static User DefaultEntity() => new User();
    }
}
