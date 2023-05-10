using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
    public class SongRatingCreateDTO
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public string SongID { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
