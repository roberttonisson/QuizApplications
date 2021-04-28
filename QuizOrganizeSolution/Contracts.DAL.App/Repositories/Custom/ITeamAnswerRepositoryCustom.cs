using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface ITeamAnswerRepositoryCustom: ITeamAnswerRepositoryCustom<TeamAnswer>
    {
    }

    public interface ITeamAnswerRepositoryCustom<TTeamAnswer>
    {
        Task<TTeamAnswer?> FirstExists(Guid teamId, Guid questionId, Guid? userId = null, bool noTracking = true);
    }
    
}