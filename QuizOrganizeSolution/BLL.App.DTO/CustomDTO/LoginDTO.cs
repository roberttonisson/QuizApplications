﻿using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.CustomDTO
{
    public class LoginDTO
    {
        [MaxLength(256)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = default!;

        [MinLength(6)]
        [MaxLength(100)]
        [Required]
        public string Password { get; set; } = default!;
    }

}