using MongoDB.Driver;
using MongoDB.Bson;
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

        private IMongoCollection<Wallet> _walletCollection;
        private IMongoCollection<Wallet> WalletCollection
        {
            get
            {
                if (_walletCollection == null)
                    _walletCollection = _database.GetCollection<Wallet>(nameof(Wallet));
                return _walletCollection;
            }
        }

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
                await WalletCollection.InsertOneAsync(wallet);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar a carteira para o usuário {wallet.User.UserName}. Erro: {ex.Message}");
            }
        }

        public async Task<Wallet> GetWalletByUserId(string userId)
        {
            var wallet = await WalletCollection.FindAsync(w => w.User.Id.Equals(userId));

            if (wallet == null)
                return null;

            return wallet.FirstOrDefault();
        }

        public void RemoveCreditCart(Wallet wallet)
        {

        }

        public void UpdateUserCreditLimit(Wallet wallet)
        {

        }
    }
}