using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
	public class GenreCreateDTO
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Description { get; set; } = null!;
	}
}
