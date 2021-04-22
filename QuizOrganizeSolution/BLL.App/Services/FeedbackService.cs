using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class FeedbackService :
        BaseEntityService<IAppUnitOfWork, IFeedbackRepository, BLLMapper<DAL.App.DTO.Feedback, BLL.App.DTO.Feedback>,
            DAL.App.DTO.Feedback, BLL.App.DTO.Feedback>, IFeedbackService
    {
        public FeedbackService(IAppUnitOfWork uow) : base(uow, uow.Feedback, new BLLMapper<DAL.App.DTO.Feedback, BLL.App.DTO.Feedback>())
        {
        }

    }
}