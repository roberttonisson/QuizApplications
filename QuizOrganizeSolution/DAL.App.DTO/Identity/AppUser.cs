using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {


        // add your own fields to User
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(128)] [MinLength(1)] public string LastName { get; set; } = default!;

        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
        
        public ICollection<UserFriend>? SentRequests { get; set; }
        public ICollection<UserFriend>? ReceivedRequests { get; set; }
        public ICollection<QuizInvitation>? QuizInvitations { get; set; }
        public ICollection<TeamUser>? TeamUsers { get; set; }
        public ICollection<Quiz>? Quizzes { get; set; }
        public ICollection<SavedQuestion>? SavedQuestions { get; set; }
    }
}