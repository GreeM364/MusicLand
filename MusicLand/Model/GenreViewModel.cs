using System.ComponentModel.DataAnnotations;

namespace MusicLand.API.Model
{
    public class GenreViewModel
    {
        [Required]
        public string Name { get; set; } = null!;


        [Required]
        public string Description { get; set; } = null!;
    }
}
