using AutoMapper;
using DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        { 
            // add more mapping configurations
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Feedback, BLL.App.DTO.Feedback>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.QuestionAnswer, BLL.App.DTO.QuestionAnswer>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Quiz, BLL.App.DTO.Quiz>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.QuizInvitation, BLL.App.DTO.QuizInvitation>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.QuizTopic, BLL.App.DTO.QuizTopic>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.SavedQuestion, BLL.App.DTO.SavedQuestion>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Team, BLL.App.DTO.Team>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.TeamAnswer, BLL.App.DTO.TeamAnswer>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.TeamUser, BLL.App.DTO.TeamUser>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.TopicQuestion, BLL.App.DTO.TopicQuestion>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.UserFriend, BLL.App.DTO.UserFriend>();

            // create Mapper based on selected configurations
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}