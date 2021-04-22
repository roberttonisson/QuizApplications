using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Domain.App;
using Domain.Base;
using Models.Identity;

namespace Models
{
    public class Quiz : DomainEntityIdMetadataUser<AppUser>
    {

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Title { get; set; } = default!;
        
        public DateTime Start { get; set; } = default!;

        public bool Finished { get; set; } = false;
        
        public ICollection<QuizTopic>? QuizTopics { get; set; }
        public ICollection<Team>? Teams { get; set; }
        public ICollection<Feedback>? Feedback { get; set; }

    }
}