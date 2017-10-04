using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IWalletService, WalletService>();
        }
    }
}