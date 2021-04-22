using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class TopicQuestionService :
        BaseEntityService<IAppUnitOfWork, ITopicQuestionRepository, BLLMapper<DAL.App.DTO.TopicQuestion, BLL.App.DTO.TopicQuestion>,
            DAL.App.DTO.TopicQuestion, BLL.App.DTO.TopicQuestion>, ITopicQuestionService
    {
        public TopicQuestionService(IAppUnitOfWork uow) : base(uow, uow.TopicQuestions, new BLLMapper<DAL.App.DTO.TopicQuestion, BLL.App.DTO.TopicQuestion>())
        {
        }

    }
}