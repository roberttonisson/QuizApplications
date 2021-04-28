using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface IUserFriendRepositoryCustom: IUserFriendRepositoryCustom<UserFriend>
    {
    }

    public interface IUserFriendRepositoryCustom<TUserFriend>
    {
        Task<TUserFriend> GetExistingRequest(BLL.App.DTO.UserFriend userFriend, Guid? userId = null, bool noTracking = true);
    }
    
}