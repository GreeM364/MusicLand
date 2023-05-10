using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
	public class RegisterationDTO
	{
		[Required]
		public string FirstName { get; set; } = null!;

		[Required]
		public string LastName { get; set; } = null!;

		[Required]
		public string Email { get; set; } = null!;

		[Required]
		public string Password { get; set; } = null!;
	}
}
