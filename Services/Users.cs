using userAuth.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace userAuth.Services{
    public class UserService{
        private readonly IMongoCollection<Users> _usersCollection;

        public UserService(IOptions<DatabaseSettings> databaseSettings){
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDataBase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this._usersCollection = mongoDataBase.GetCollection<Users>(databaseSettings.Value.UserCollectionName);
        }

        public async Task<List<Users>> GetAsync() => await this._usersCollection.Find(_ => true).ToListAsync();
        public async Task<Users?> GetAsync(string id) => await this._usersCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Users user) => await this._usersCollection.InsertOneAsync(user);
        public async Task UpdateAsync(string id, Users user) => await this._usersCollection.ReplaceOneAsync(x => x.id == id, user);
        public async Task RemoveAsync(string id) => await this._usersCollection.DeleteOneAsync(x => x.id == id);
    }
}
