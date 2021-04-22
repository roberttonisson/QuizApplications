using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;

namespace DAL.App.DTO
{
    public class QuestionAnswer : DomainEntityId
    {
        [Required] 
        public Guid TopicQuestionId { get; set; } = default!;
        public TopicQuestion? TopicQuestion { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Answer { get; set; } = default!;
        
        public bool Correct { get; set; } = false;
                
        public ICollection<TeamAnswer>? TeamAnswers { get; set; }
    }
}