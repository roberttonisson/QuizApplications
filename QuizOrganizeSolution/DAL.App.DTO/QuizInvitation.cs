using System;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class QuizInvitation : DomainEntityId, IDomainEntityUser<AppUser>
    {
        [Required] 
        public Guid TeamId { get; set; } = default!;
        public Team? Team { get; set; }
        
        public bool Pending { get; set; } = true;
        
        public bool Accepted { get; set; } = false;
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}