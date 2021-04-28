using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.CustomDTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface ITeamAnswerService : IBaseEntityService<TeamAnswer>, ITeamAnswerRepositoryCustom<TeamAnswer>
    {
        Task<IEnumerable<TeamAnswer>> AddTeamAnswers(TeamAnswer[] teamAnswers, Guid? userId = null,
            bool noTracking = true);
        
    }
}