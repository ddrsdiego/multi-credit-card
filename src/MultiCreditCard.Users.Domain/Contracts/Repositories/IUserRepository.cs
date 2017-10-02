using MultiCreditCard.Users.Domain.Entities;

namespace MultiCreditCard.Users.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        User GetUserByEmail(string email);
    }
}
