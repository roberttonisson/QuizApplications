using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface IUserFriendService : IBaseEntityService<UserFriend>, IUserFriendRepositoryCustom<UserFriend>
    {
        Task<IEnumerable<AppUser>> SearchUsers(string search, Guid? userId = null,
            bool noTracking = true);
        Task<UserFriend> SendFriendRequest(UserFriend userFriend, Guid? userId = null,
            bool noTracking = true);
    }
}