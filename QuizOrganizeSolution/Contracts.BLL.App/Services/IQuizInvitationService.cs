using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.CustomDTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface IQuizInvitationService : IBaseEntityService<QuizInvitation>, IQuizInvitationRepositoryCustom<QuizInvitation>
    {
        Task<QuizInvitation> AddWithTeamUser(QuizInvitation quizInvitation, Guid? userId = null,
            bool noTracking = true);
    }
}