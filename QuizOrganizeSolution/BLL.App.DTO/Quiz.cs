using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Quiz : DomainEntityId, IDomainEntityUser<AppUser>
    {

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Title { get; set; } = default!;
        
        public DateTime Start { get; set; } = default!;

        public bool Finished { get; set; } = false;

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public ICollection<QuizTopic>? QuizTopics { get; set; }
        public ICollection<Feedback>? Feedback { get; set; }
        public ICollection<Team>? Teams { get; set; }
    }
}