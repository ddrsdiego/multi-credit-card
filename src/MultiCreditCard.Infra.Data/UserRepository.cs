using MultiCreditCard.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using MultiCreditCard.Domain.Entities;

namespace MultiCreditCard.Infra.Data
{
    public class UserRepository : IUserRepository
    {
        public void Create(Domain.Entities.User user)
        {

        }

        public Domain.Entities.User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
