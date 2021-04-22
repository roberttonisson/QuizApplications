using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class SavedQuestionService :
        BaseEntityService<IAppUnitOfWork, ISavedQuestionRepository, BLLMapper<DAL.App.DTO.SavedQuestion, BLL.App.DTO.SavedQuestion>,
            DAL.App.DTO.SavedQuestion, BLL.App.DTO.SavedQuestion>, ISavedQuestionService
    {
        public SavedQuestionService(IAppUnitOfWork uow) : base(uow, uow.SavedQuestions, new BLLMapper<DAL.App.DTO.SavedQuestion, BLL.App.DTO.SavedQuestion>())
        {
        }

    }
}