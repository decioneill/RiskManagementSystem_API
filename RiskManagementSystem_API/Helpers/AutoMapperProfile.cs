using AutoMapper;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Models.Users;
using RiskManagementSystem_API.Models.Projects;
using RiskManagementSystem_API.Models.Risks;
using RiskManagementSystem_API.Models.Mitigation;

namespace RiskManagementSystem_API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();

            //Project
            CreateMap<Project, ProjectModel>();
            CreateMap<TeamMember, TeamMemberModel>();

            //Risk
            CreateMap<Risk, RiskModel>();
            CreateMap<RiskProperty, RiskPropertiesModel>();

            //Mitigation
            CreateMap<Mitigation, MitigationModel>();
        }
    }
}