using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; } = null!;

        [Required]
        public string NewPassword { get; set; } = null!;
    }
}
