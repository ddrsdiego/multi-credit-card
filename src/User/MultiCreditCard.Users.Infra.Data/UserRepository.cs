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

        public UserRepository()
        {
            _client = new MongoClient("mongodb://admin:admin@ds036577.mlab.com:36577/multi-credit-card");
            _database = _client.GetDatabase("multi-credit-card");
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                var collection = _database.GetCollection<User>("users");
                await collection.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var collection = _database.GetCollection<User>("users");
            var user = await collection.FindAsync(x => x.Email.EletronicAddress.Equals(email));
            if (user == null)
                return null;

            return user.FirstOrDefault();
        }
    }
}
