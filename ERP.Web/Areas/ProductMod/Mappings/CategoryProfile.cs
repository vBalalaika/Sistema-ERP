using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;


namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
        }
    }

}

