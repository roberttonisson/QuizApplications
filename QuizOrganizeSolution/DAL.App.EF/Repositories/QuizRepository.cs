using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuizRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.Quiz, DAL.App.DTO.Quiz>,
        IQuizRepository
    {
        public QuizRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.Quiz, DAL.App.DTO.Quiz>())
        {
            
        }

        /*public async Task<IEnumerable<Quiz>> GetUserQuizzes(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(us)
        }*/

        public async Task<Quiz> GetSingleWithCollections(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(a => a.Id == id)
                .Include(s => s.QuizTopics!)
                .ThenInclude(t => t.TopicQuestions)
                .ThenInclude(a => a.QuestionAnswers)
                .Include(a => a.Teams)
                .ThenInclude(b => b.TeamUsers)
                .ThenInclude(c=> c.AppUser)
                .Include(a => a.Teams)
                .ThenInclude(b => b.AppUser)
                .Include(a => a.Teams)
                .ThenInclude(b => b.TeamAnswers)
                .ThenInclude(c => c.TopicQuestion);
                
            
            var domainEntity = await query.FirstOrDefaultAsync();
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}