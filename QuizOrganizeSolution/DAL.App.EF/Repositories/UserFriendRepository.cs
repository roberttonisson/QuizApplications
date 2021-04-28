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
    public class UserFriendRepository :
        EFBaseRepository<AppDbContext, Models.Identity.AppUser, Models.UserFriend, DAL.App.DTO.UserFriend>,
        IUserFriendRepository
    {
        public UserFriendRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Models.UserFriend, DAL.App.DTO.UserFriend>())
        {
        }

        public async Task<UserFriend> GetExistingRequest(BLL.App.DTO.UserFriend userFriend, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Where(a => a.RecipientId == userFriend.RecipientId)
                .Where(a => a.AppUserId == userFriend.AppUserId);

            var domainEntity = await query.FirstOrDefaultAsync();
            var result = Mapper.Map(domainEntity);
            return result;
        }
    }
}