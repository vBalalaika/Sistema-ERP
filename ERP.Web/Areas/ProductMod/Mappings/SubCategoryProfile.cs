using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    public class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategoryDTO, SubCategoryViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
