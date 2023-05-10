using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicLand.DAL.Entity
{
    public class SongRating
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("UserID")]
        public Guid UserID { get; set; }

        [BsonElement("SongID")]
        public Guid SongID { get; set; }

        [BsonElement("LikedDate")]
        public DateTime LikedDate { get; set; }

        [BsonElement("Rating")]
        public int Rating { get; set; }
    }
}
