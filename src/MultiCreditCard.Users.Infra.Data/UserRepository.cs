using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using System;

namespace MultiCreditCard.Users.Infra.Data
{
    public class UserRepository : IUserRepository
    {
        public void Create(User user)
        {

        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
