using System;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using QuizInvitation = BLL.App.DTO.QuizInvitation;


namespace BLL.App.Services
{
    public class QuizInvitationService :
        BaseEntityService<IAppUnitOfWork, IQuizInvitationRepository, BLLMapper<DAL.App.DTO.QuizInvitation, BLL.App.DTO.QuizInvitation>,
            DAL.App.DTO.QuizInvitation, BLL.App.DTO.QuizInvitation>, IQuizInvitationService
    {
        public QuizInvitationService(IAppUnitOfWork uow) : base(uow, uow.QuizInvitations, new BLLMapper<DAL.App.DTO.QuizInvitation, BLL.App.DTO.QuizInvitation>())
        {
        }

        public async Task<QuizInvitation> AddWithTeamUser(QuizInvitation quizInvitation, Guid? userId = null, bool noTracking = true)
        {
            var qi = await UpdateAsync(quizInvitation);
            await UOW.SaveChangesAsync();
            if (qi.Accepted)
            {
                UOW.TeamUsers.Add(new TeamUser
                {
                    TeamId = quizInvitation.TeamId,
                    AppUserId = quizInvitation.AppUserId,
                });
            }
            await UOW.SaveChangesAsync();
            return qi;
        }
    }
}