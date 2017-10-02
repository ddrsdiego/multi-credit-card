using MultiCreditCard.Users.Domain.Entities;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetUserByEmail(string email);
    }
}
