using System;
using System.Threading.Tasks;
using BLL.App.DTO.CustomDTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Team = BLL.App.DTO.Team;


namespace BLL.App.Services
{
    public class TeamService :
        BaseEntityService<IAppUnitOfWork, ITeamRepository, BLLMapper<DAL.App.DTO.Team, BLL.App.DTO.Team>,
            DAL.App.DTO.Team, BLL.App.DTO.Team>, ITeamService
    {
        public TeamService(IAppUnitOfWork uow) : base(uow, uow.Teams, new BLLMapper<DAL.App.DTO.Team, BLL.App.DTO.Team>())
        {
        }

        public async Task<Team> AddTeamWithMembers(AddTeamDTO addTeamDto, Guid? userId = null, bool noTracking = true)
        {
            var team = Repository.Add(Mapper.Map(addTeamDto.Team));
            await UOW.SaveChangesAsync();

            UOW.TeamUsers.Add(new TeamUser
            {
                TeamId = team.Id,
                AppUserId = team.AppUserId,
            });
            foreach (var memberId in addTeamDto.Members)
            {
                UOW.QuizInvitations.Add(new QuizInvitation
                {
                    TeamId = team.Id,
                    Pending = true,
                    Accepted = false,
                    AppUserId = memberId,
                });
            }
            await UOW.SaveChangesAsync();
            return Mapper.Map(team);
        }
    }
}