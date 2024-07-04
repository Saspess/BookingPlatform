using MongoDB.Bson.Serialization.Attributes;

namespace BP.IdentityMS.Data.Entities
{
    public class UserEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
