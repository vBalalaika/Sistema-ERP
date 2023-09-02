using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Mappings.Purchases.Providers
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
        }
    }
}
