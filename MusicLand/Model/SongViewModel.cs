using System.ComponentModel.DataAnnotations;

namespace MusicLand.API.Model
{
    public class SongViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public Guid GenreID { get; set; }

        [Required]
        public Guid ArtistID { get; set; }
    }
}
