using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
    public class ArtistUpdateDTO
    {
        [Required]
        public string ArtistId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
