using userAuth.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace userAuth.Services{
    public class UserService{
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<DatabaseSettings> databaseSettings){
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDataBase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _userCollection = mongoDataBase.GetCollection<User>("user");
        }

        public async Task<List<User>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();
        public async Task<User?> GetAsync(string id) => await _userCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(User newUser) => await _userCollection.InsertOneAsync(newUser);
        public async Task UpdateAsync(string id, User updatedUser) => await _userCollection.ReplaceOneAsync(x => x.id == id, updatedUser);
        public async Task RemoveAsync(string id) => await _userCollection.DeleteOneAsync(x => x.id == id);
    }
}
