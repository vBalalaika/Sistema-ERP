using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Domain.Entities.Clients;
using System.Threading.Tasks;

namespace ERP.Application.Services.Clients
{
    public class ConsultedProductService : GenericService<ConsultedProduct, ConsultedProductDTO>, IConsultedProductService
    {
        public ConsultedProductService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override Task<int> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}
