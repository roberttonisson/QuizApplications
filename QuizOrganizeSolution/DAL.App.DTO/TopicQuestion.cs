using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App;
using Extensions;

namespace DAL.App.DTO
{
    public class TopicQuestion : DomainEntityId
    {
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Question { get; set; } = default!;
        
        [Required]
        [MinLength(2)]
        [MaxLength(4096)]
        public string Text { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(4,1)")]
        public decimal Points { get; set; } = default!;

        [Required] 
        public QuestionType Type { get; set; } = default!;
        
        [Required] 
        public Guid QuizTopicId { get; set; } = default!;
        public QuizTopic? QuizTopic { get; set; }
        
        public ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
        public ICollection<TeamAnswer>? TeamAnswers { get; set; }
        public ICollection<SavedQuestion>? SavedQuestions { get; set; }
    }
}