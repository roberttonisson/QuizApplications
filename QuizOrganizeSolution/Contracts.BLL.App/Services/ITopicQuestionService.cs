using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface ITopicQuestionService : IBaseEntityService<TopicQuestion>, ITopicQuestionRepositoryCustom<TopicQuestion>
    {
        
    }
}