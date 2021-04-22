using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Models.Identity;

namespace Models
{
    public class QuizInvitation : DomainEntityIdMetadataUser<AppUser>
    {
        [Required] 
        public Guid TeamId { get; set; } = default!;
        public Team? Team { get; set; }
        
        public bool Pending { get; set; } = true;
        
        public bool Accepted { get; set; } = false;
    }
}