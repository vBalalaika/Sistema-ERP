using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Web.Areas.Commons.Models;

namespace ERP.Web.Areas.Commons.Mappings
{
    public class IVAConditionProfile : Profile
    {
        public IVAConditionProfile()
        {
            CreateMap<IVAConditionDTO, IVAConditionViewModel>().ReverseMap();
        }
    }
}
