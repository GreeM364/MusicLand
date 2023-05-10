namespace MusicLand.WEB.Models.DTO
{
	public class SongDTO
	{
		public string SongID { get; set; } = null!;
		public string Title { get; set; } = null!;
		public int Duration { get; set; }
		public string GenreID { get; set; } = null!;
		public string ArtistID { get; set; } = null!;
	}
}
