using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Team : DomainEntityId
    {
        [Required] 
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; } = default!;

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public ICollection<TeamUser>? TeamUsers { get; set; }
        public ICollection<TeamAnswer>? TeamAnswers { get; set; }
        public ICollection<QuizInvitation>? QuizInvitations { get; set; }
    }
}