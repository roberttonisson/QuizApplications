using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Models.Identity;

namespace Models
{
    public class TeamUser : DomainEntityIdMetadataUser<AppUser>
    {
        [Required] 
        public Guid TeamId { get; set; } = default!;
        public Team? Team { get; set; }
        
        public ICollection<Feedback>? Feedback { get; set; }
    }
}