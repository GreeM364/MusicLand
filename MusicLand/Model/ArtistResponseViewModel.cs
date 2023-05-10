namespace MusicLand.API.Model
{
    public class ArtistResponseViewModel
    {
        public Guid ArtistID { get; set; } 
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
