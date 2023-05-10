namespace MusicLand.API.Model
{
    public class SongResponseViewModel
    {
        public Guid SongID { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public Guid GenreID { get; set; }
        public Guid ArtistID { get; set; }
    }
}
