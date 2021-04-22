using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface ITopicQuestionRepositoryCustom: ITopicQuestionRepositoryCustom<TopicQuestion>
    {
    }

    public interface ITopicQuestionRepositoryCustom<TTopicQuestion>
    {
    }
    
}