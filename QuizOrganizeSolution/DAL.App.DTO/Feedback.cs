using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;

namespace DAL.App.DTO
{
    public class Feedback : DomainEntityId
    {
        [Required] 
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
        
        [Required] 
        public Guid TeamUserId { get; set; } = default!;
        public TeamUser? TeamUser { get; set; }
        
        [MinLength(2)]
        [MaxLength(1024)]
        public string? Text { get; set; } = null;
        
        [Required]
        [Range(0,5)]
        public byte Rating { get; set; } = default!;
    }
}