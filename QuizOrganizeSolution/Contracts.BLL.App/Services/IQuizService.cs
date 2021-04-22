using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories.Custom;

namespace Contracts.BLL.App.Services
{
    public interface IQuizService : IBaseEntityService<Quiz>, IQuizRepositoryCustom<Quiz>
    {

    }
}