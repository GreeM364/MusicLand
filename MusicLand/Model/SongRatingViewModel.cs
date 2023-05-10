using System.ComponentModel.DataAnnotations;

namespace MusicLand.API.Model
{
    public class SongRatingViewModel
    {
        [Required]
        public Guid UserID { get; set; }

        [Required]
        public Guid SongID { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
