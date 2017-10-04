using MongoDB.Driver;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Infra.Data
{
    public class WalletRepository : IWalletRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public WalletRepository()
        {
            _client = new MongoClient("mongodb://admin:admin@ds036577.mlab.com:36577/multi-credit-card");
            _database = _client.GetDatabase("multi-credit-card");
        }

        public void AddNewCreditCart(Wallet wallet)
        {

        }


        public async Task CreateWalletAsync(Wallet wallet)
        {
            try
            {
                var collection = _database.GetCollection<Wallet>(nameof(Wallet));
                await collection.InsertOneAsync(wallet);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar a carteira para o usuário {wallet.User.UserName}. Erro: {ex.Message}");
            }
        }

        public void RemoveCreditCart(Wallet wallet)
        {

        }

        public void UpdateUserCreditLimit(Wallet wallet)
        {

        }
    }
}