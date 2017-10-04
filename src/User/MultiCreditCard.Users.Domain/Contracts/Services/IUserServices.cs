using MultiCreditCard.Users.Domain.Entities;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Domain.Contracts.Services
{
    public interface IUserServices
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByUserId(string userId);
        Task<User> GetUserByEmail(string email);
    }
}
