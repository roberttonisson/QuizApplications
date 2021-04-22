using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.CustomDTO;
using BLL.App.DTO.Identity;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface IAppUserService : IBaseEntityService<AppUser>, IAppUserRepositoryCustom<AppUser>
    {
        Task<AppUserCustomDTO> GetUserWithQuizCollectionsCustomUser(Guid userId, bool noTracking = true);
        Task<AppUserCustomDTO> GetUserWithFriendsCollectionsCustomUser(Guid userId, bool noTracking = true);
    }
}