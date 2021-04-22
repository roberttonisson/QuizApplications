using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App;

namespace BLL.App.DTO
{
    public class TeamAnswer : DomainEntityId
    {
        [Required] 
        public Guid TeamId { get; set; } = default!;
        public Team? Team { get; set; }
        
        public Guid? QuestionAnswerId { get; set; } = null;
        public QuestionAnswer? QuestionAnswer { get; set; }
        
        [Required] 
        public Guid TopicQuestionId { get; set; } = default!;
        public TopicQuestion? TopicQuestion { get; set; }
        
        [MinLength(2)]
        [MaxLength(64)]
        public string? Answer { get; set; } = null;
        
        public bool Correct { get; set; } = false;

        [Column(TypeName = "decimal(4,1)")]
        public decimal? Points { get; set; } = 0;
    }
}