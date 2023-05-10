namespace MusicLand.API.Model
{
    public class SongRatingResponseViewModel
    {
        public Guid UserID { get; set; }
        public Guid SongID { get; set; }
        public DateTime LikedDate { get; set; }
        public int Rating { get; set; }
    }
}
