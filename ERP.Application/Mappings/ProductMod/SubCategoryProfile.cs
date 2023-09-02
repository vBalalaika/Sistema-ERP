using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Mappings.ProductMod
{
    internal class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {

            CreateMap<SubCategory, SubCategoryDTO>().ReverseMap();
        }
    }
}
