﻿using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Entities;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Domain.Contracts.Services
{
    public interface IWalletService
    {
        void AddNewCreditCart(Wallet wallet);

        void Buy(Wallet wallet);

        Task<Wallet> GetWalletByUserId(string userId);

        Task CreateWalletAsync(User user);
    }
}
