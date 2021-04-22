using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TopicQuestionRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.TopicQuestion, DAL.App.DTO.TopicQuestion>,
        ITopicQuestionRepository
    {
        public TopicQuestionRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.TopicQuestion, DAL.App.DTO.TopicQuestion>())
        {
        }

    }
}