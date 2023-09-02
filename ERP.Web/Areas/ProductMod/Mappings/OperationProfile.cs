using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Web.Areas.ProductMod.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    public class OperationProfile : Profile
    {
        public OperationProfile()
        {
            CreateMap<OperationDTO, OperationViewModel>().ReverseMap();
        }
    }
}
