using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface IQuizTopicRepositoryCustom: IQuizTopicRepositoryCustom<QuizTopic>
    {
    }

    public interface IQuizTopicRepositoryCustom<TQuizTopic>
    {
    }
    
}