using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories.Custom
{
    public interface IQuizRepositoryCustom: IQuizRepositoryCustom<Quiz>
    {
    }

    public interface IQuizRepositoryCustom<TQuiz>
    {
        Task<TQuiz> GetSingleWithCollections(Guid id, Guid? userId = null, bool noTracking = true);
    }
    
}