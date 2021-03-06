using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface ISavedQuestionService : IBaseEntityService<SavedQuestion>, ISavedQuestionRepositoryCustom<SavedQuestion>
    {
        
    }
}