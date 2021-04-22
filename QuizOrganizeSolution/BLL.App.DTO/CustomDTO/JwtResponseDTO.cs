using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.CustomDTO
{
    public class JwtResponseDTO
    {
        [Required]
        public Guid AppUserId { get; set; } = default!;
        [Required]
        public string Token { get; set; } = default!;
        [Required]
        public string Status { get; set; } = default!;

        [Required]
        public string FirstName { get; set; }  = default!;
        [Required]
        public string LastName { get; set; }  = default!;
    }

}