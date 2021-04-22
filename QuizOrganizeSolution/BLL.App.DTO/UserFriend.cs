using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace BLL.App.DTO
{
    public class UserFriend : DomainEntityId
    {

        [Required]
        [ForeignKey("Recipient")]
        public Guid RecipientId { get; set; } = default!;
        public AppUser? Recipient { get; set; } 
        
        public bool Pending { get; set; } = true;
        
        public bool Accepted { get; set; } = false;


        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}