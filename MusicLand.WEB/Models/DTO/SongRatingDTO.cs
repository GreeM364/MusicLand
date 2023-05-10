namespace MusicLand.WEB.Models.DTO
{
    public class SongRatingDTO
    {
        public string UserID { get; set; }
        public string SongID { get; set; }
        public DateTime LikedDate { get; set; }
        public int Rating { get; set; }
    }
}
