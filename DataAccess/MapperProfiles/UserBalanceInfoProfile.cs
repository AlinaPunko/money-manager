using AutoMapper;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;

namespace DataAccess.MapperProfiles
{
    class UserBalanceInfoProfile : Profile
    {
        public UserBalanceInfoProfile()
        {
            CreateMap<User, UserBalanceInfo>()
                .ForMember(d => d.Balance, opt => opt.MapFrom(src => BalanceHelper.GetUserBalance(src)));
        }
    }
}
