using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class TeamUser : DomainEntityId, IDomainEntityUser<AppUser>
    {
        [Required] 
        public Guid TeamId { get; set; } = default!;
        public Team? Team { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public ICollection<Feedback>? Feedback { get; set; }

    }
}