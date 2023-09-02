using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.PurchaseOrders
{
    public class ServicePOHeaderService : GenericService<ServicePOHeader, ServicePOHeaderDTO>, IServicePOHeaderService
    {
        public ServicePOHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(ServicePOHeaderDTO entityToCreateDTO)
        {
            await _mapDetails(entityToCreateDTO);
            return await base.CreateAsync(entityToCreateDTO);
        }

        public override async Task<int> UpdateAsync(ServicePOHeaderDTO entityDtoToUpdate)
        {
            await _mapDetails(entityDtoToUpdate);
            return await base.UpdateAsync(entityDtoToUpdate);
        }

        public async Task _mapDetails(ServicePOHeaderDTO servicePOHeaderDTO)
        {
            var oldDetails = _unitOfWork.ServicePODetailRepository.GetAll().Result.Where(sPOD => sPOD.ServicePOHeaderId == servicePOHeaderDTO.Id).ToList();

            // For create
            foreach (var detail in servicePOHeaderDTO.ServicePODetails.Where(sPOD => sPOD.Id == 0))
            {
                await _unitOfWork.ServicePODetailRepository.CreateAsync(detail);
            }

            // For update
            foreach (var detail in servicePOHeaderDTO.ServicePODetails.Where(sPOD => sPOD.Id != 0 && oldDetails.Any(od => od.Id == sPOD.Id)))
            {
                _unitOfWork.ServicePODetailRepository.Update(detail);
            }

            // For delete
            foreach (var detail in oldDetails.Where(od => !servicePOHeaderDTO.ServicePODetails.Any(sPOD => sPOD.Id != 0 && sPOD.Id == od.Id)))
            {
                await _unitOfWork.ServicePODetailRepository.DeleteAsync(detail.Id);
            }
        }
    }
}
