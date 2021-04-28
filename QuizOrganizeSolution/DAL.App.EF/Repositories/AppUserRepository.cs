using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;
using Models.Identity;

namespace DAL.App.EF.Repositories
{
    public class AppUserRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, AppUser, DAL.App.DTO.Identity.AppUser>,
        IAppUserRepository
    {
        public AppUserRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<AppUser, DAL.App.DTO.Identity.AppUser>())
        {
        }

        public async Task<DTO.Identity.AppUser> GetUserWithQuizCollections(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery(null, noTracking);
            query = query
                .Where(a => a.Id == userId)
                .Include(a => a.Quizzes)
                .Include(a => a.QuizInvitations)
                .ThenInclude(b => b.Team)
                .ThenInclude(c => c.Quiz)
                .Include(a => a.TeamUsers)
                .ThenInclude(b => b.Team)
                .ThenInclude(c => c.Quiz);
            
            var domainEntity = await query.FirstOrDefaultAsync();
            var result = Mapper.Map(domainEntity);
            return result;
        }

        public async Task<DTO.Identity.AppUser> GetUserWithFriendsCollections(Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery(null, noTracking);
            query = query
                .Where(a => a.Id == userId)
                .Include(a => a.ReceivedRequests)
                .ThenInclude(b => b.AppUser)
                .Include(a => a.SentRequests)
                .ThenInclude(b => b.Recipient);

            var domainEntity = await query.FirstOrDefaultAsync();
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}