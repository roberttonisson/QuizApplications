using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class TeamAnswerService :
        BaseEntityService<IAppUnitOfWork, ITeamAnswerRepository, BLLMapper<DAL.App.DTO.TeamAnswer, BLL.App.DTO.TeamAnswer>,
            DAL.App.DTO.TeamAnswer, BLL.App.DTO.TeamAnswer>, ITeamAnswerService
    {
        public TeamAnswerService(IAppUnitOfWork uow) : base(uow, uow.TeamAnswers, new BLLMapper<DAL.App.DTO.TeamAnswer, BLL.App.DTO.TeamAnswer>())
        {
        }

        public async Task<IEnumerable<TeamAnswer>> AddTeamAnswers(TeamAnswer[] teamAnswers, Guid? userId = null, bool noTracking = true)
        {
            var ret = new List<TeamAnswer>();
            foreach (var answer in teamAnswers)
            {
                var x = await  FirstExists(answer.TeamId, answer.TopicQuestionId, userId, noTracking);
                if ( x == null)
                {
                   ret.Add(Add(new TeamAnswer
                   {
                       TeamId = answer.TeamId,
                       TopicQuestionId = answer.TopicQuestionId,
                       Answer = answer.Answer,
                       Correct = answer.Correct,
                       Points = answer.Points
                   })); 
                }
                else
                {
                    x.Answer = answer.Answer;
                    x.Correct = answer.Correct;
                    x.Points = answer.Points;
                    await UpdateAsync(x);
                    ret.Add(x);
                }
            }
            await UOW.SaveChangesAsync();
            return ret;
        }


        public async Task<TeamAnswer?> FirstExists(Guid teamId, Guid questionId, Guid? userId = null, bool noTracking = true)
        {
            return  Mapper.Map(await Repository.FirstExists(teamId,questionId,userId,noTracking));
        }
    }
}