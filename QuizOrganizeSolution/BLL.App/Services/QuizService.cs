using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class QuizService :
        BaseEntityService<IAppUnitOfWork, IQuizRepository, BLLMapper<DAL.App.DTO.Quiz, BLL.App.DTO.Quiz>,
            DAL.App.DTO.Quiz, BLL.App.DTO.Quiz>, IQuizService
    {
        public QuizService(IAppUnitOfWork uow) : base(uow, uow.Quizzes,
            new BLLMapper<DAL.App.DTO.Quiz, BLL.App.DTO.Quiz>())
        {
        }


        public async Task<Quiz> GetSingleWithCollections(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var quiz = await Repository.GetSingleWithCollections(id, userId, noTracking);
            return Mapper.Map(quiz);
        }


    }
}