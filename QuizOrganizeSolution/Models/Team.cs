using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Models.Identity;

namespace Models
{
    public class Team : DomainEntityIdMetadata
    {
        [Required] 
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        public ICollection<TeamUser>? TeamUsers { get; set; }
        public ICollection<TeamAnswer>? TeamAnswers { get; set; }
        public ICollection<QuizInvitation>? QuizInvitations { get; set; }

    }
}