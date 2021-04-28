using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.CustomDTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface ITeamService : IBaseEntityService<Team>, ITeamRepositoryCustom<Team>
    {
        Task<Team> AddTeamWithMembers(AddTeamDTO addTeamDto, Guid? userId = null,
            bool noTracking = true);
        
    }
}