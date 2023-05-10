using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicLand.DAL.Entity
{
    public class User
    {
        [BsonId]
        public Guid UserID { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
