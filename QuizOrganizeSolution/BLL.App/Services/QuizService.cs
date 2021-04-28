using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.Mappers;


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
        
        public async Task<List<Quiz>> GetFriendQuizzes(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var friends = new List<AppUser>();
            var userMapper = new BLLMapper<DAL.App.DTO.Identity.AppUser, AppUser>();
            var quizzes = new List<Quiz>();
            var user = await UOW.AppUsers.GetUserWithFriendsCollections(id);
            
            if (user.ReceivedRequests != null)
            {
                friends.AddRange(from request in user.ReceivedRequests where request.Accepted select userMapper.Map(request.AppUser));
            }
            if (user.SentRequests != null)
            {
                friends.AddRange(from request in user.SentRequests where request.Accepted select userMapper.Map(request.Recipient));
            }

            foreach (var q in friends.Select(friend => 
                UOW.AppUsers.GetUserWithQuizCollections(friend.Id).Result.Quizzes!.Where(a => !a.Finished)))
            {
                quizzes.AddRange(q.Select(quiz => Mapper.Map(quiz)));
            }

            
            return quizzes;
        }


    }
}