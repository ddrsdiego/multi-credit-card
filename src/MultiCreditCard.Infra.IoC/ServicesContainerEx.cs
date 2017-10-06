using Microsoft.Extensions.DependencyInjection;
using MultiCreditCard.CreditCards.Domain.Contracts.Service;
using MultiCreditCard.CreditCards.Domain.Services;
using MultiCreditCard.Users.Domain.Contracts.Services;
using MultiCreditCard.Users.Domain.Services;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Services;

namespace MultiCreditCard.Infra.IoC
{
    public static class ServicesContainerEx
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICreditCardService, CreditCardService>();
        }
    }
}