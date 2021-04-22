using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class TeamService :
        BaseEntityService<IAppUnitOfWork, ITeamRepository, BLLMapper<DAL.App.DTO.Team, BLL.App.DTO.Team>,
            DAL.App.DTO.Team, BLL.App.DTO.Team>, ITeamService
    {
        public TeamService(IAppUnitOfWork uow) : base(uow, uow.Teams, new BLLMapper<DAL.App.DTO.Team, BLL.App.DTO.Team>())
        {
        }

    }
}