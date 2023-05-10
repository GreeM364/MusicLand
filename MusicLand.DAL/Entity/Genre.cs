using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MusicLand.DAL.Entity
{
    public class Genre
    {
        [BsonId]
        public Guid GenreID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }


        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
