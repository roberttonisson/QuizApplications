using AutoMapper;
using DAL.Base.Mappers;
using Models;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mapping configurations
            MapperConfigurationExpression.CreateMap<Models.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Models.Feedback, DAL.App.DTO.Feedback>();
            MapperConfigurationExpression.CreateMap<Models.QuestionAnswer, DAL.App.DTO.QuestionAnswer>();
            MapperConfigurationExpression.CreateMap<Models.Quiz, DAL.App.DTO.Quiz>();
            MapperConfigurationExpression.CreateMap<Models.QuizInvitation, DAL.App.DTO.QuizInvitation>();
            MapperConfigurationExpression.CreateMap<Models.QuizTopic, DAL.App.DTO.QuizTopic>();
            MapperConfigurationExpression.CreateMap<Models.SavedQuestion, DAL.App.DTO.SavedQuestion>();
            MapperConfigurationExpression.CreateMap<Models.Team, DAL.App.DTO.Team>();
            MapperConfigurationExpression.CreateMap<Models.TeamAnswer, DAL.App.DTO.TeamAnswer>();
            MapperConfigurationExpression.CreateMap<Models.TeamUser, DAL.App.DTO.TeamUser>();
            MapperConfigurationExpression.CreateMap<Models.TopicQuestion, DAL.App.DTO.TopicQuestion>();
            MapperConfigurationExpression.CreateMap<Models.UserFriend, DAL.App.DTO.UserFriend>();

            // create Mapper based on selected configurations
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}