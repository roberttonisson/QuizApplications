using System;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IFeedbackRepository Feedback { get; }
        IQuestionAnswerRepository QuestionAnswers { get; }
        IQuizInvitationRepository QuizInvitations { get; }
        IQuizRepository Quizzes { get; }
        IQuizTopicRepository QuizTopics { get; }
        ISavedQuestionRepository SavedQuestions { get; }
        ITeamAnswerRepository TeamAnswers { get; }
        ITeamRepository Teams { get; }
        ITeamUserRepository TeamUsers { get; }
        ITopicQuestionRepository TopicQuestions { get; }
        IUserFriendRepository UserFriends { get; }
        IAppUserRepository AppUsers { get; }
    }
}