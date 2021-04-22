using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        public IFeedbackRepository Feedback =>
            GetRepository<IFeedbackRepository>(() => new FeedbackRepository(UOWDbContext));

        public IQuestionAnswerRepository QuestionAnswers =>
            GetRepository<IQuestionAnswerRepository>(() => new QuestionAnswerRepository(UOWDbContext));

        public IQuizInvitationRepository QuizInvitations =>
            GetRepository<IQuizInvitationRepository>(() => new QuizInvitationRepository(UOWDbContext));

        public IQuizRepository Quizzes =>
            GetRepository<IQuizRepository>(() => new QuizRepository(UOWDbContext));

        public IQuizTopicRepository QuizTopics =>
            GetRepository<IQuizTopicRepository>(() => new QuizTopicRepository(UOWDbContext));

        public ISavedQuestionRepository SavedQuestions =>
            GetRepository<ISavedQuestionRepository>(() => new SavedQuestionRepository(UOWDbContext));
        
        public ITeamAnswerRepository TeamAnswers =>
            GetRepository<ITeamAnswerRepository>(() => new TeamAnswerRepository(UOWDbContext));

        public ITeamRepository Teams =>
            GetRepository<ITeamRepository>(() => new TeamRepository(UOWDbContext));

        public ITeamUserRepository TeamUsers =>
            GetRepository<ITeamUserRepository>(() => new TeamUserRepository(UOWDbContext));

        public ITopicQuestionRepository TopicQuestions =>
            GetRepository<ITopicQuestionRepository>(() => new TopicQuestionRepository(UOWDbContext));

        public IUserFriendRepository UserFriends =>
            GetRepository<IUserFriendRepository>(() => new UserFriendRepository(UOWDbContext));
        
        public IAppUserRepository AppUsers =>
            GetRepository<IAppUserRepository>(() => new AppUserRepository(UOWDbContext));
        
    }
}