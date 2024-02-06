using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace userAuth.Models{
    public class User{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public string token { get; set; } = null!;
    }
}