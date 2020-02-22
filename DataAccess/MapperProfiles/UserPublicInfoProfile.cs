using AutoMapper;
using DataAccess.Models;
using DataAccess.Projections;

namespace DataAccess.MapperProfiles
{
    class UserPublicInfoProfile : Profile
    {
        public UserPublicInfoProfile()
        {
            CreateMap<User,UserPublicInfo>();
        }
    }
}
