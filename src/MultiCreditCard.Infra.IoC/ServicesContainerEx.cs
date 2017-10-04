using Microsoft.Extensions.DependencyInjection;
using MultiCreditCard.Users.Domain.Contracts.Services;
using MultiCreditCard.Users.Domain.Services;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCreditCard.Infra.IoC
{
    public static class ServicesContainerEx
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWalletService, WalletService>();
        }
    }
}