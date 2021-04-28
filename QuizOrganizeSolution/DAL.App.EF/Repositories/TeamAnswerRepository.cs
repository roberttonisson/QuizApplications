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
    public class TeamAnswerRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.TeamAnswer, DAL.App.DTO.TeamAnswer>,
        ITeamAnswerRepository
    {
        public TeamAnswerRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.TeamAnswer, DAL.App.DTO.TeamAnswer>())
        {
        }
        
        public async Task<TeamAnswer?> FirstExists(Guid teamId, Guid questionId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(a => teamId == a.TeamId && questionId == a.TopicQuestionId);


            var domainEntity = await query.FirstOrDefaultAsync();
            if (domainEntity != null)
            {
                var result = Mapper.Map(domainEntity);  
            }
            
            return null;
        }

    }
}