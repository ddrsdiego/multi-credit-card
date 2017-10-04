using Microsoft.Extensions.DependencyInjection;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Infra.Data;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Infra.Data;

namespace MultiCreditCard.Infra.IoC
{
    public static class RepositoriesContainerEx
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
        }
    }
}
