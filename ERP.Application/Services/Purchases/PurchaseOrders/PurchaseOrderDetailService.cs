using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.PurchaseOrders;
using ERP.Application.Specification.Logistics.Incomes;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.PurchaseOrders
{
    public class PurchaseOrderDetailService : GenericService<PurchaseOrderDetail, PurchaseOrderDetailDTO>, IPurchaseOrderDetailService
    {
        public PurchaseOrderDetailService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
        public override async Task<int> DeleteAsync(int id)
        {
            // Antes de borrar la orden de compra, debo volver los estados de los faltantes al estado "Pendiente", setear el ProviderId = null y la PurchasedQuantity = 0
            // ademas, eliminar los ingresos asociados con los faltantes. 
            UpdateDataOnDelete(id);

            return await base.DeleteAsync(id);
        }

        // Se ejecuta cuando se devuelve un ítem de una orden de compra porque no es lo que se pidió.
        public async Task<int> UpdateDataOnReturnItemAsync(int podId)
        {
            try
            {
                var podEntity = new PurchaseOrderDetail();
                var getPODById = _unitOfWork.PurchaseOrderDetailRepository.FindAsync(new PurchasesOrderDetailWithAllSpecification(podId));
                if (getPODById.Result.Count() > 0)
                {
                    podEntity = getPODById.Result.First();
                    if (podEntity.MissingProductId.HasValue)
                    {
                        var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(podEntity.MissingProductId.Value));
                        if (getMPById.Result.Count() > 0)
                        {
                            var mpEntity = getMPById.Result.First();
                            var mpDto = _mapper.Map<MissingProductDTO>(mpEntity);
                            mpDto.ProviderId = null;
                            mpDto.StateMissingProductId = 3;
                            mpDto.PurchasedQuantity = 0;
                            mpEntity = _mapper.Map<MissingProduct>(mpDto);
                            _unitOfWork.MissingProductRepository.Update(mpEntity);
                        }

                        var getIncomeDetailsByMPId = _unitOfWork.IncomeDetailRepository.FindAsync(new IncomeDetailsByMissingProductIdSpecification(podEntity.MissingProductId.Value));
                        if (getIncomeDetailsByMPId.Result.Count() > 0)
                        {
                            foreach (var id in getIncomeDetailsByMPId.Result)
                            {
                                await _unitOfWork.IncomeDetailRepository.DeleteAsync(id.Id);
                            }
                        }
                    }

                    // Debo agregar a la tabla "PurchaseStates" el estado "Devuelto"
                    podEntity.PurchaseStateId = 8;
                    _unitOfWork.PurchaseOrderDetailRepository.Update(podEntity);

                }

                await _unitOfWork.Commit();
                return podEntity.Id;
            }
            catch
            {
                throw;
            }
        }

        public async void UpdateDataOnDelete(int podId)
        {
            try
            {
                var getPODById = _unitOfWork.PurchaseOrderDetailRepository.FindAsync(new PurchasesOrderDetailWithAllSpecification(podId));
                if (getPODById.Result.Count() > 0)
                {
                    var podEntity = getPODById.Result.First();
                    if (podEntity.MissingProductId.HasValue)
                    {
                        var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(podEntity.MissingProductId.Value));
                        if (getMPById.Result.Count() > 0)
                        {
                            var mpEntity = getMPById.Result.First();
                            var mpDto = _mapper.Map<MissingProductDTO>(mpEntity);
                            mpDto.ProviderId = null;
                            mpDto.StateMissingProductId = 1;
                            mpDto.PurchasedQuantity = 0;
                            mpEntity = _mapper.Map<MissingProduct>(mpDto);
                            _unitOfWork.MissingProductRepository.Update(mpEntity);
                        }

                        var getIncomeDetailsByMPId = _unitOfWork.IncomeDetailRepository.FindAsync(new IncomeDetailsByMissingProductIdSpecification(podEntity.MissingProductId.Value));
                        if (getIncomeDetailsByMPId.Result.Count() > 0)
                        {
                            foreach (var id in getIncomeDetailsByMPId.Result)
                            {
                                await _unitOfWork.IncomeDetailRepository.DeleteAsync(id.Id);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
