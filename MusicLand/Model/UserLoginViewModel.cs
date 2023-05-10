﻿using System.ComponentModel.DataAnnotations;

namespace MusicLand.API.Model
{
    public class UserLoginViewModel
    {
        [Required] 
        public string Email { get; set; } = null!;

        [Required] 
        public string Password { get; set; } = null!;

    }
}
