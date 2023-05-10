using System.ComponentModel.DataAnnotations;

namespace MusicLand.API.Model
{
    public class ChangePasswordViewModel
    {
        [Required] 
        public string OldPassword { get; set; } = null!;

        [Required] 
        public string NewPassword { get; set; } = null!;
    }
}
