using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiCreditCard.Infra.Data.Mongo
{
    public class MongoDBContext
    {
        private IMongoDatabase _database { get; }

        public MongoDBContext()
        {

        }
    }
}
