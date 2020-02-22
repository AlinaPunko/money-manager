using AutoMapper;
using DataAccess.Models;
using DataAccess.Projections;

namespace DataAccess.MapperProfiles
{
    class FullTransactionInfoProfile : Profile
    {
        public FullTransactionInfoProfile()
        {
            CreateMap<Transaction, FullTransactionInfo>()
                .ForMember(fullInfo => fullInfo.AssetName,
                    fullInfo => fullInfo.MapFrom(t => t.Asset.Name))
                .ForMember(fullInfo => fullInfo.CategoryName,
                    fullInfo => fullInfo.MapFrom(t => t.Category.Name))
                .ForMember(fullInfo => fullInfo.ParentName,
                    fullInfo => fullInfo.MapFrom(t => t.Category.Parent.Name));
        }
    }
}
