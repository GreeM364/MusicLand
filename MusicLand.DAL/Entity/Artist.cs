using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MusicLand.DAL.Entity
{
    public class Artist
    {
        [BsonId]
        public Guid ArtistID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }
    }
}
