using System.ComponentModel.DataAnnotations;

namespace MusicLand.API.Model
{
    public class ArtistViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
