using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Models.Identity;

namespace Models
{
    public class Team : DomainEntityIdMetadataUser<AppUser>
    {
        [Required] 
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        public ICollection<TeamUser>? TeamUsers { get; set; }
        public ICollection<TeamAnswer>? TeamAnswers { get; set; }
        public ICollection<QuizInvitation>? QuizInvitations { get; set; }

    }
}