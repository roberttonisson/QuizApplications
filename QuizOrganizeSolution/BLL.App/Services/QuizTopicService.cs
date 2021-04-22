using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class QuizTopicService :
        BaseEntityService<IAppUnitOfWork, IQuizTopicRepository, BLLMapper<DAL.App.DTO.QuizTopic, BLL.App.DTO.QuizTopic>,
            DAL.App.DTO.QuizTopic, BLL.App.DTO.QuizTopic>, IQuizTopicService
    {
        public QuizTopicService(IAppUnitOfWork uow) : base(uow, uow.QuizTopics, new BLLMapper<DAL.App.DTO.QuizTopic, BLL.App.DTO.QuizTopic>())
        {
        }

    }
}