using Microsoft.Extensions.DependencyInjection;
using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Infra.Data;
using MultiCreditCard.CreditCards.Infra.Data.Repository;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Infra.Data.Repository;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Infra.Data.Repository;

namespace MultiCreditCard.Infra.IoC
{
    public static class RepositoriesContainerEx
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
        }
    }
}
