using System;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class SavedQuestion : DomainEntityId, IDomainEntityUser<AppUser>
    {
        [Required] 
        public Guid TopicQuestionId { get; set; } = default!;
        public TopicQuestion? TopicQuestion { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}