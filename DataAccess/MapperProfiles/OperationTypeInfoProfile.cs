using AutoMapper;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;

namespace DataAccess.MapperProfiles
{
    class OperationTypeInfoProfile : Profile
    {
        public OperationTypeInfoProfile()
        {
            CreateMap<Category, OperationTypeInfo>()
                .ForMember(d => d.Amount, opt => opt.MapFrom(src => ParentCategoriesHelper.GetNumberOfParents(src)));
        }
    }
}
