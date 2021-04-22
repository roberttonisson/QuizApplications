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
    public class QuestionAnswerRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.QuestionAnswer, DAL.App.DTO.QuestionAnswer>,
        IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.QuestionAnswer, DAL.App.DTO.QuestionAnswer>())
        {
        }

    }
}