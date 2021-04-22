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
    public class UserFriend : DomainEntityIdMetadataUser<AppUser>
    {

        [Required]
        [ForeignKey("Recipient")]
        public Guid RecipientId { get; set; } = default!;
        public AppUser? Recipient { get; set; } 
        
        public bool Pending { get; set; } = true;
        
        public bool Accepted { get; set; } = false;
        
        

    }
}