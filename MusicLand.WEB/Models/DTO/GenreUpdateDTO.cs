using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
	public class GenreUpdateDTO
	{
		[Required]
		public string GenreID { get; set; }

		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Description { get; set; } = null!;
	}
}
