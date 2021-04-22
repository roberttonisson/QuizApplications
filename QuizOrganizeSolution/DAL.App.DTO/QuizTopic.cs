using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;

namespace DAL.App.DTO
{
    public class QuizTopic : DomainEntityId
    {
        [Required] 
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Topic { get; set; } = default!;

        public int? TimeLimit { get; set; } = null;
        
        public ICollection<TopicQuestion>? TopicQuestions { get; set; }
        
    }
}