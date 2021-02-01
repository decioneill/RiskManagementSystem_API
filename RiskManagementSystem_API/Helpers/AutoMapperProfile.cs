using AutoMapper;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Models.Users;

namespace RiskManagementSystem_API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}