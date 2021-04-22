using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class QuizInvitationService :
        BaseEntityService<IAppUnitOfWork, IQuizInvitationRepository, BLLMapper<DAL.App.DTO.QuizInvitation, BLL.App.DTO.QuizInvitation>,
            DAL.App.DTO.QuizInvitation, BLL.App.DTO.QuizInvitation>, IQuizInvitationService
    {
        public QuizInvitationService(IAppUnitOfWork uow) : base(uow, uow.QuizInvitations, new BLLMapper<DAL.App.DTO.QuizInvitation, BLL.App.DTO.QuizInvitation>())
        {
        }

    }
}