using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public IFeedbackService Feedback =>
            GetService<IFeedbackService>(() => new FeedbackService(UOW));

        public IQuestionAnswerService QuestionAnswers =>
            GetService<IQuestionAnswerService>(() => new QuestionAnswerService(UOW));

        public IQuizService Quizzes =>
            GetService<IQuizService>(() => new QuizService(UOW));

        public IQuizInvitationService QuizInvitations =>
            GetService<IQuizInvitationService>(() => new QuizInvitationService(UOW));

        public IQuizTopicService QuizTopics =>
            GetService<IQuizTopicService>(() => new QuizTopicService(UOW));

        public ISavedQuestionService SavedQuestions =>
            GetService<ISavedQuestionService>(() => new SavedQuestionService(UOW));
        
        public ITeamAnswerService TeamAnswers =>
            GetService<ITeamAnswerService>(() => new TeamAnswerService(UOW));

        public ITeamService Teams =>
            GetService<ITeamService>(() => new TeamService(UOW));

        public ITeamUserService TeamUsers =>
            GetService<ITeamUserService>(() => new TeamUserService(UOW));

        public ITopicQuestionService TopicQuestions =>
            GetService<ITopicQuestionService>(() => new TopicQuestionService(UOW));

        public IUserFriendService UserFriends =>
            GetService<IUserFriendService>(() => new UserFriendService(UOW));
        
        public IAppUserService AppUsers =>
            GetService<IAppUserService>(() => new AppUserService(UOW));

    }
}