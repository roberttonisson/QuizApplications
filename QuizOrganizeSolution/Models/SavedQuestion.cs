using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Models.Identity;

namespace Models
{
    public class SavedQuestion : DomainEntityIdMetadataUser<AppUser>
    {
        [Required] 
        public Guid TopicQuestionId { get; set; } = default!;
        public TopicQuestion? TopicQuestion { get; set; }
        
    }
}