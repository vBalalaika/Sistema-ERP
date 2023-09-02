using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Web.Areas.Purchases.Models.Providers;

namespace ERP.Web.Areas.Purchases.Mappings.Providers
{
    internal class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactDTO, ContactViewModel>().ReverseMap();
        }
    }
}
