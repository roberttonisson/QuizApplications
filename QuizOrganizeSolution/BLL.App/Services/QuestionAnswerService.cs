using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class QuestionAnswerService :
        BaseEntityService<IAppUnitOfWork, IQuestionAnswerRepository, BLLMapper<DAL.App.DTO.QuestionAnswer, BLL.App.DTO.QuestionAnswer>,
            DAL.App.DTO.QuestionAnswer, BLL.App.DTO.QuestionAnswer>, IQuestionAnswerService
    {
        public QuestionAnswerService(IAppUnitOfWork uow) : base(uow, uow.QuestionAnswers, new BLLMapper<DAL.App.DTO.QuestionAnswer, BLL.App.DTO.QuestionAnswer>())
        {
        }
        
        public async Task<QuestionAnswer[]> AddList(QuestionAnswer[] questions, Guid? userId = null, bool noTracking = true)
        {
            foreach (var question in questions)
            {
                if (await Repository.ExistsAsync(question.Id))
                {
                    continue;
                }
                Repository.Add(Mapper.Map(question));
            }

            await UOW.SaveChangesAsync();
            return questions;
        }

    }
}