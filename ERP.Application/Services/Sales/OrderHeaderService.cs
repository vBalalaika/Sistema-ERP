using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Application.Specification.Sales;
using ERP.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Sales
{
    public class OrderHeaderService : GenericService<OrderHeader, OrderHeaderDTO>, IOrderHeaderService
    {
        public OrderHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(OrderHeaderDTO entityToCreateDTO)
        {
            await MapDetails(entityToCreateDTO);
            return await base.CreateAsync(entityToCreateDTO);
        }

        public async Task MapDetails(OrderHeaderDTO headerDTO)
        {
            IReadOnlyList<OrderDetail> oldDetails = _unitOfWork.OrderDetailRepository.GetAll().Result.Where(x => x.OrderHeaderId == headerDTO.Id).ToList();

            // Create
            foreach (var detail in headerDTO.OrderDetails.Where(x => x.Id == 0))
            {
                await _unitOfWork.OrderDetailRepository.CreateAsync(detail);
            }

            // Delete
            foreach (var detail in oldDetails.Where(od => !headerDTO.OrderDetails.Any(x => x.Id != 0 && x.Id == od.Id)))
            {
                await _unitOfWork.OrderDetailRepository.DeleteAsync(detail.Id);
            }
        }

        public async Task<int> UpdateOrderState(OrderHeaderDTO dtoToUpdate, int orderStateId)
        {
            OrderHeader entity = MapDetailsUpdateOrderState(dtoToUpdate, orderStateId).Result;

            entity.OrderStateId = orderStateId;

            _unitOfWork.GetRepository<OrderHeader>().Update(entity);

            await _unitOfWork.Commit();
            return entity.Id;
        }

        public async Task<OrderHeader> MapDetailsUpdateOrderState(OrderHeaderDTO dtoToUpdate, int orderStateId)
        {
            IReadOnlyList<OrderDetail> detailsForThisOH = _unitOfWork.OrderDetailRepository.GetAll().Result.Where(x => x.OrderHeaderId == dtoToUpdate.Id).ToList();

            foreach (var detail in detailsForThisOH)
            {
                detail.OrderStateId = orderStateId;

                _unitOfWork.GetRepository<OrderDetail>().Update(detail);
                await _unitOfWork.Commit();
            }

            dtoToUpdate.OrderDetails = detailsForThisOH.ToList();

            return MapToEntity(dtoToUpdate);

        }

        public async Task<int> UpdateBilledState(OrderHeaderDTO dtoToUpdate, int billedState)
        {

            OrderHeader entity = MapToEntity(dtoToUpdate);

            entity.Billed = billedState;

            _unitOfWork.GetRepository<OrderHeader>().Update(entity);

            await _unitOfWork.Commit();
            return entity.Id;
        }

        public async Task<int> UpdateSomeProperties(OrderHeaderDTO dtoToUpdate)
        {
            var orderHeaderWithDetailsUpdated = MapDetailsUpdateDeliveryDate(dtoToUpdate);

            _unitOfWork.GetRepository<OrderHeader>().Update(orderHeaderWithDetailsUpdated.Result);

            await _unitOfWork.Commit();
            return orderHeaderWithDetailsUpdated.Result.Id;
        }

        public async Task<OrderHeader> MapDetailsUpdateDeliveryDate(OrderHeaderDTO dtoToUpdate)
        {
            var orderHeaderOld = await _unitOfWork.OrderHeaderRepository.FindAsync(new OrderHeaderWithAllDetailsSpecification(dtoToUpdate.Id));

            var orderHeaderOldDTO = MapToDto(orderHeaderOld.FirstOrDefault());

            orderHeaderOldDTO.SaleObservations = dtoToUpdate.SaleObservations;
            orderHeaderOldDTO.ProductionObservations = dtoToUpdate.ProductionObservations;
            orderHeaderOldDTO.Transport = dtoToUpdate.Transport;
            orderHeaderOldDTO.SaleOperationId = dtoToUpdate.SaleOperationId;

            IReadOnlyList<OrderDetail> oldDetailsForThisOH = _unitOfWork.OrderDetailRepository.GetAll().Result.Where(x => x.OrderHeaderId == dtoToUpdate.Id).ToList();

            foreach (var detail in dtoToUpdate.OrderDetails.Where(x => x.Id != 0 && oldDetailsForThisOH.Any(y => y.Id == x.Id)))
            {
                foreach (var oldDetail in oldDetailsForThisOH)
                {
                    if (oldDetail.Id == detail.Id)
                    {
                        //detail.rowOrder = oldDetail.rowOrder;
                        if (oldDetail.DeliveryDate != detail.DeliveryDate)
                        {
                            // Creo un registro en DeliveryDateHistories
                            var deliveryDateHistory = new DeliveryDateHistory();
                            deliveryDateHistory.ChangeDate = DateTime.Now;
                            deliveryDateHistory.OldDeliveryDate = oldDetail.DeliveryDate.Value;
                            deliveryDateHistory.NewDeliveryDate = detail.DeliveryDate.Value;
                            deliveryDateHistory.OrderDetailId = detail.Id;

                            await _unitOfWork.DeliveryDateHistoryRepository.CreateAsync(deliveryDateHistory);

                            oldDetail.DeliveryDate = detail.DeliveryDate;

                        }
                    }
                }
                _unitOfWork.GetRepository<OrderDetail>().Update(detail);
            }

            orderHeaderOldDTO.OrderDetails = dtoToUpdate.OrderDetails.ToList();

            return MapToEntity(orderHeaderOldDTO);

        }

    }
}
