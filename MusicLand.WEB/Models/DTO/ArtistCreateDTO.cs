using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
    public class ArtistCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
