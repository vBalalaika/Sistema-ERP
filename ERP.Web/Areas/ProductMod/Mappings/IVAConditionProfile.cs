using AutoMapper;
using ERP.Domain.Entities.Commons;
using ERP.Web.Areas.Commons.Models;

namespace ERP.Web.Areas.ProductMod.Mappings
{
    internal class IVAConditionProfile : Profile
    {
        public IVAConditionProfile()
        {
            CreateMap<IVACondition, IVAConditionViewModel>().ReverseMap();
        }
    }
}

