using AutoMapper;
using DataAccess.Models;
using DataAccess.Projections;

namespace DataAccess.MapperProfiles
{
    class AssetBalanceInfoProfile : Profile
    {
        public AssetBalanceInfoProfile()
        {
            CreateMap<Asset, AssetBalanceInfo>();
        }
    }
}
