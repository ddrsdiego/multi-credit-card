using MongoDB.Driver;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Infra.Data
{
    public class UserRepository : IUserRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        private IMongoCollection<User> _usersCollection;
        private IMongoCollection<User> UsersCollection
        {
            get
            {
                if (_usersCollection == null)
                    _usersCollection = _database.GetCollection<User>(nameof(User));
                return _usersCollection;
            }
        }

        public UserRepository()
        {
            _client = new MongoClient("mongodb://admin:admin@ds036577.mlab.com:36577/multi-credit-card");
            _database = _client.GetDatabase("multi-credit-card");
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                await UsersCollection.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar o usuário {user.UserName}");
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await UsersCollection.FindAsync(x => x.Email.EletronicAddress.Equals(email));

            if (user == null)
                return null;

            return user.FirstOrDefault();
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            try
            {
                var user = await UsersCollection.FindAsync(x => x.Id.Equals(userId));

                if (user == null)
                    return null;

                return user.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao criar o usuário {userId}. Erro: {ex.Message}");
            }
        }
    }
}
