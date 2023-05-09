using MongoDB.Driver;

namespace ChatBot.Infra.DataProvider
{
    public class Context
    {
        private readonly IMongoDatabase _mongoDatabase;

        public Context(IMongoClient client, string databaseName)
        {
            _mongoDatabase = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Entities.User> UserCollection
        {
            get
            {
                return _mongoDatabase.GetCollection<Entities.User>(nameof(UserCollection));
            }
        }

        public IMongoCollection<Entities.Message> MessageCollection
        {
            get
            {
                return _mongoDatabase.GetCollection<Entities.Message>(nameof(MessageCollection));
            }
        }
    }
}
