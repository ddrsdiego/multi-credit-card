﻿using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Entities;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Domain.Contracts.Services
{
    public interface IWalletService
    {
        void Buy(Wallet wallet, decimal valueBuy);

        Task CreateWalletAsync(User user);

        void AddNewCreditCart(Wallet wallet);

        void RemoveCreditCart(Wallet wallet);

        void UpdateUserCreditLimit(Wallet wallet);
    }
}
