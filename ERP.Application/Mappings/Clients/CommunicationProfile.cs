using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;

namespace ERP.Application.Mappings.Clients
{
    public class CommunicationProfile : Profile
    {
        public CommunicationProfile()
        {
            CreateMap<Communication, CommunicationDTO>().ReverseMap();
        }
    }
}
