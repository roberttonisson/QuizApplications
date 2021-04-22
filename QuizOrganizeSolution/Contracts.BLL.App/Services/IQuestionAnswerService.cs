using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface IQuestionAnswerService : IBaseEntityService<QuestionAnswer>, IQuestionAnswerRepositoryCustom<QuestionAnswer>
    {
        Task<QuestionAnswer[]> AddList(QuestionAnswer[] questions, Guid? userId = null, bool noTracking = true);
    }
}