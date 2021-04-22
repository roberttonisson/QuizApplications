using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class TeamUserService :
        BaseEntityService<IAppUnitOfWork, ITeamUserRepository, BLLMapper<DAL.App.DTO.TeamUser, BLL.App.DTO.TeamUser>,
            DAL.App.DTO.TeamUser, BLL.App.DTO.TeamUser>, ITeamUserService
    {
        public TeamUserService(IAppUnitOfWork uow) : base(uow, uow.TeamUsers, new BLLMapper<DAL.App.DTO.TeamUser, BLL.App.DTO.TeamUser>())
        {
        }

    }
}