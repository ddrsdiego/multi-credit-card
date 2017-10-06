using MultiCreditCard.Users.Domain.Entities;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetUserByUserId(string userId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserFromCredentials(string email, string password);
    }
}
