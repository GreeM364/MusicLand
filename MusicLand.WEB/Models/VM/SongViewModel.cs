namespace MusicLand.WEB.Models.VM
{
    public class SongViewModel
    {
		public string SongID { get; set; } = null!;
		public string Title { get; set; } = null!;
		public int Duration { get; set; }
		public string Genre { get; set; } = null!;
		public string Artist { get; set; } = null!;
		public double Rating { get; set; }
	}
}
