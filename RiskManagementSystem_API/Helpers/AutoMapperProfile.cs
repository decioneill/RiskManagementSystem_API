using AutoMapper;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Models.Users;
using RiskManagementSystem_API.Models.Projects;

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
        }
    }
}