﻿using System.ComponentModel.DataAnnotations;

namespace MusicLand.API.Model
{
    public class UserRegisterViewModel
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
