using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace BLL.App.DTO.CustomDTO
{
    public class QuizAddDTO : DomainEntityId
    {
        
        public string Title { get; set; } = default!;
        
        public DateTime Start { get; set; } = default!;

        public bool Finished { get; set; } = false;

        public Guid AppUserId { get; set; }

    }
}