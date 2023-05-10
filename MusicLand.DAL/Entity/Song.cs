using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MusicLand.DAL.Entity
{
    public class Song
    {
        [BsonId]
        public Guid SongID { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Duration")]
        public int Duration { get; set; }

        [BsonElement("GenreID")]
        public Guid GenreID { get; set; }

        [BsonElement("ArtistID")]
        public Guid ArtistID { get; set; }
    }
}
