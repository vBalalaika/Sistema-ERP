using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Domain.Entities.Clients;

namespace ERP.Application.Mappings.Clients
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
        }
    }
}
