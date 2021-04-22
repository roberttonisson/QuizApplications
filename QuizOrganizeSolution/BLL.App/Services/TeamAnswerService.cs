using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class TeamAnswerService :
        BaseEntityService<IAppUnitOfWork, ITeamAnswerRepository, BLLMapper<DAL.App.DTO.TeamAnswer, BLL.App.DTO.TeamAnswer>,
            DAL.App.DTO.TeamAnswer, BLL.App.DTO.TeamAnswer>, ITeamAnswerService
    {
        public TeamAnswerService(IAppUnitOfWork uow) : base(uow, uow.TeamAnswers, new BLLMapper<DAL.App.DTO.TeamAnswer, BLL.App.DTO.TeamAnswer>())
        {
        }

    }
}