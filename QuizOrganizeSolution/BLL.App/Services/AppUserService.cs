using System;
using System.Threading.Tasks;
using BLL.App.DTO.CustomDTO;
using BLL.App.DTO.Identity;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class AppUserService :
        BaseEntityService<IAppUnitOfWork, IAppUserRepository, BLLMapper<DAL.App.DTO.Identity.AppUser, AppUser>,
            DAL.App.DTO.Identity.AppUser, AppUser>, IAppUserService
    {
        public AppUserService(IAppUnitOfWork uow) : base(uow, uow.AppUsers, new BLLMapper<DAL.App.DTO.Identity.AppUser, AppUser>())
        {
        }

        public async Task<AppUser> GetUserWithQuizCollections(Guid userId, bool noTracking = true)
        {
            var user = await Repository.GetUserWithQuizCollections(userId, noTracking);

            return Mapper.Map(user);
        }

        public async Task<AppUser> GetUserWithFriendsCollections(Guid userId, bool noTracking = true)
        {
            var user = await Repository.GetUserWithQuizCollections(userId, noTracking);

            return Mapper.Map(user);
        }

        public async Task<AppUserCustomDTO> GetUserWithQuizCollectionsCustomUser(Guid userId, bool noTracking = true)
        {
            var quiz = await GetUserWithQuizCollections(userId, noTracking);

            return new AppUserCustomDTO
            {
                FirstName = quiz.FirstName,
                LastName = quiz.LastName,
                SentRequests = quiz.SentRequests,
                ReceivedRequests = quiz.ReceivedRequests,
                QuizInvitations = quiz.QuizInvitations,
                TeamUsers = quiz.TeamUsers,
                Quizzes = quiz.Quizzes,
                SavedQuestions = quiz.SavedQuestions
            };

        }

        public async Task<AppUserCustomDTO> GetUserWithFriendsCollectionsCustomUser(Guid userId, bool noTracking = true)
        {
            var quiz = await GetUserWithFriendsCollections(userId, noTracking);

            return new AppUserCustomDTO
            {
                FirstName = quiz.FirstName,
                LastName = quiz.LastName,
                SentRequests = quiz.SentRequests,
                ReceivedRequests = quiz.ReceivedRequests,
                QuizInvitations = quiz.QuizInvitations,
                TeamUsers = quiz.TeamUsers,
                Quizzes = quiz.Quizzes,
                SavedQuestions = quiz.SavedQuestions
            };
        }
    }
}