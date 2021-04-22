using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IFeedbackService Feedback { get; }
        IQuestionAnswerService QuestionAnswers { get; }
        IQuizInvitationService QuizInvitations { get; }
        IQuizService Quizzes { get; }
        IQuizTopicService QuizTopics { get; }
        ISavedQuestionService SavedQuestions { get; }
        ITeamAnswerService TeamAnswers { get; }
        ITeamService Teams { get; }
        ITeamUserService TeamUsers { get; }
        ITopicQuestionService TopicQuestions { get; }
        IUserFriendService UserFriends { get; }
        IAppUserService AppUsers { get; }
    }
}