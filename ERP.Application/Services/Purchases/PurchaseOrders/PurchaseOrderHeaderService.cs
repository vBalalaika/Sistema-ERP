using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.DTOs.Entities.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.PurchaseOrders;
using ERP.Application.Specification.ProductMod;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Purchases.ProviderProduct;
using ERP.Application.Specification.Purchases.PurchaseOrders;
using ERP.Application.Specification.Purchases.QuoteRequests;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.PurchaseOrders
{
    public class PurchaseOrderHeaderService : GenericService<PurchaseOrderHeader, PurchaseOrderHeaderDTO>, IPurchaseOrderHeaderService
    {
        public PurchaseOrderHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(PurchaseOrderHeaderDTO entityToCreateDTO)
        {
            await MapDetails(entityToCreateDTO);

            // Luego de que se crea la orden de compra, hay que cambiar el estado de los detalles de la SC a "Comprado".
            UpdateStatesQuoteRequestDetails(entityToCreateDTO);

            // Luego de que se crea la orden de compra, debo cambiar el estado de los productos en faltantes => "Comprado".
            UpdateMissingProductStatesAndSetProviderId(entityToCreateDTO);

            // Luego de que se crea la orden de compra, tengo que actualizar el DollarPrice o PesosPrice con el valor del UnitPrice del detalle por cada producto.
            UpdatePrices(entityToCreateDTO);

            // Luego de que se crea la orden de compra, se deben generar registros de ingresos con los detalles de compra.
            await createIncomesByPODetails(entityToCreateDTO);

            return await base.CreateAsync(entityToCreateDTO);
        }

        public override async Task<int> UpdateAsync(PurchaseOrderHeaderDTO entityDtoToUpdate)
        {
            await MapDetails(entityDtoToUpdate);
            return await base.UpdateAsync(entityDtoToUpdate);
        }

        public async Task MapDetails(PurchaseOrderHeaderDTO headerDTO)
        {
            IReadOnlyList<PurchaseOrderDetail> oldDetails = _unitOfWork.PurchaseOrderDetailRepository.GetAll().Result.Where(x => x.PurchaseOrderHeaderId == headerDTO.Id).ToList();

            // Create
            foreach (var detail in headerDTO.PurchaseOrderDetails.Where(x => x.Id == 0))
            {
                await _unitOfWork.PurchaseOrderDetailRepository.CreateAsync(detail);
            }

            // Update
            foreach (var detail in headerDTO.PurchaseOrderDetails.Where(x => x.Id != 0 && oldDetails.Any(od => od.Id == x.Id)))
            {
                _unitOfWork.PurchaseOrderDetailRepository.Update(detail);

                // Luego de que se actualice el detalle de compra, tengo que actualizar la informacion del ingreso asociado.
                if (detail.IncomeDetailId.HasValue)
                {
                    var getIncomeDetailById = _unitOfWork.IncomeDetailRepository.GetByIdAsync(detail.IncomeDetailId.Value);
                    if (getIncomeDetailById.Result != null)
                    {
                        getIncomeDetailById.Result.Quantity = detail.ProviderQuantity;
                        _unitOfWork.IncomeDetailRepository.Update(getIncomeDetailById.Result);
                    }
                }
            }

            // Delete
            foreach (var detail in oldDetails.Where(od => !headerDTO.PurchaseOrderDetails.Any(x => x.Id != 0 && x.Id == od.Id)))
            {
                await _unitOfWork.PurchaseOrderDetailRepository.DeleteAsync(detail.Id);
            }
        }

        public async void UpdateStatesQuoteRequestDetails(PurchaseOrderHeaderDTO poHeaderDTO)
        {
            try
            {
                if (poHeaderDTO.QuoteRequestResponseHeaderId.HasValue)
                {
                    var getQRRHById = await _unitOfWork.QuoteRequestResponseHeaderRepository.FindAsync(new QuoteRequestResponseHeaderWithAllSpecifications(poHeaderDTO.QuoteRequestResponseHeaderId.Value));
                    var getQRHById = await _unitOfWork.QuoteRequestHeaderRepository.FindAsync(new QuoteRequestHeaderWithAllSpecifications(getQRRHById.First().QuoteRequestHeaderId));
                    if (getQRHById != null && getQRHById.Count() > 0)
                    {
                        //foreach (var qrd in getQRHById.First().QuoteRequestDetails)
                        foreach (var qrd in getQRHById.First().QuoteRequestDetails.Where(qrd => poHeaderDTO.PurchaseOrderDetails.Any(pod => pod.MissingProductId == qrd.MissingProductId || pod.ProductId == qrd.ProductId)))
                        {
                            qrd.PurchaseStateId = 4;
                            _unitOfWork.QuoteRequestDetailRepository.Update(qrd);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateMissingProductStatesAndSetProviderId(PurchaseOrderHeaderDTO poHeaderDTO)
        {
            try
            {
                foreach (var pod in poHeaderDTO.PurchaseOrderDetails)
                {
                    if (pod.MissingProductId.HasValue)
                    {
                        var getMPById = _unitOfWork.MissingProductRepository.GetByIdAsync(pod.MissingProductId.Value);
                        getMPById.Result.ProviderId = poHeaderDTO.ProviderId;
                        getMPById.Result.StateMissingProductId = 4;
                        getMPById.Result.PurchasedQuantity = pod.PurchasedQuantity;
                        _unitOfWork.MissingProductRepository.Update(getMPById.Result);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePrices(PurchaseOrderHeaderDTO poHeaderDTO)
        {
            try
            {
                foreach (var pod in poHeaderDTO.PurchaseOrderDetails)
                {
                    if (pod.MissingProductId.HasValue)
                    {
                        var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(pod.MissingProductId.Value));
                        if (getMPById.Result.Count() > 0)
                        {
                            var mpEntity = getMPById.Result.First();
                            var getRPPByProductAndProviderId = _unitOfWork.RelProviderProductRepository.FindAsync(new RelProviderProductByProductAndProviderIdSpecification(mpEntity.ProductId, poHeaderDTO.ProviderId));
                            if (getRPPByProductAndProviderId.Result.Count() > 0)
                            {
                                var rppEntity = getRPPByProductAndProviderId.Result.First();
                                var rppDTO = _mapper.Map<RelProviderProductDTO>(rppEntity);

                                if (pod.MoneyType == 1)
                                {
                                    rppDTO.PesosPrice = pod.UnitPrice;
                                }
                                else if (pod.MoneyType == 2)
                                {
                                    rppDTO.DollarPrice = pod.UnitPrice;
                                }

                                rppEntity = _mapper.Map<RelProviderProduct>(rppDTO);
                                _unitOfWork.RelProviderProductRepository.Update(rppEntity);
                            }
                        }
                    }
                    else if (pod.ProductId.HasValue)
                    {
                        var getProductById = _unitOfWork.ProductRepository.FindAsync(new ProductWithAllSpecification(pod.ProductId.Value));
                        if (getProductById.Result.Count() > 0)
                        {
                            var productEntity = getProductById.Result.First();
                            var getRPPByProductAndProviderId = _unitOfWork.RelProviderProductRepository.FindAsync(new RelProviderProductByProductAndProviderIdSpecification(productEntity.Id, poHeaderDTO.ProviderId));
                            if (getRPPByProductAndProviderId.Result.Count() > 0)
                            {
                                var rppEntity = getRPPByProductAndProviderId.Result.First();
                                var rppDTO = _mapper.Map<RelProviderProductDTO>(rppEntity);

                                if (pod.MoneyType == 1)
                                {
                                    rppDTO.PesosPrice = pod.UnitPrice;
                                }
                                else if (pod.MoneyType == 2)
                                {
                                    rppDTO.DollarPrice = pod.UnitPrice;
                                }

                                rppEntity = _mapper.Map<RelProviderProduct>(rppDTO);
                                _unitOfWork.RelProviderProductRepository.Update(rppEntity);
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

        public async Task createIncomesByPODetails(PurchaseOrderHeaderDTO poHeaderDTO)
        {
            try
            {
                var poHeader = MapToEntity(poHeaderDTO);

                var newIncomeHeader = new IncomeHeader();
                newIncomeHeader.IncomeDate = poHeader.Date;
                newIncomeHeader.ProviderId = poHeader.ProviderId;
                newIncomeHeader.TransportProviderId = poHeader.ProviderId;
                newIncomeHeader.OwnTransport = true;

                foreach (var pod in poHeader.PurchaseOrderDetails)
                {
                    var newIncomeDetail = new IncomeDetail();
                    newIncomeDetail.PurchaseOrderDetail = pod;
                    if (pod.MissingProductId.HasValue)
                    {
                        newIncomeDetail.MissingProductId = pod.MissingProductId.Value;
                        newIncomeDetail.IncomeProductId = pod.MissingProduct.ProductId;
                        newIncomeDetail.Quantity = pod.PurchasedQuantity;
                        if (pod.MissingProduct.QuantityToOrderUnitMeasureId.HasValue)
                        {
                            newIncomeDetail.UnitId = pod.MissingProduct.QuantityToOrderUnitMeasureId.Value;
                        }
                    }
                    else if (pod.ProductId.HasValue)
                    {
                        newIncomeDetail.IncomeProductId = pod.ProductId.Value;
                        newIncomeDetail.Quantity = pod.ProviderQuantity;
                        newIncomeDetail.UnitId = pod.ProviderUnitMeasureId;
                    }
                    else
                    {
                        break;
                    }

                    //newIncomeDetail.Quantity = pod.ProviderQuantity;
                    //newIncomeDetail.UnitId = pod.ProviderUnitMeasureId;

                    var purchaseOrderHeaders = _unitOfWork.PurchaseOrderHeaderRepository.FindAsync(new PurchaseOrderHeaderSpecification());
                    if (purchaseOrderHeaders.Result.Count() > 0)
                    {
                        newIncomeDetail.OCNumber = purchaseOrderHeaders.Result.Max(poh => poh.Id);
                        newIncomeDetail.OCNumber += 1;
                    }
                    else
                    {
                        newIncomeDetail.OCNumber = 1;
                    }

                    // "Pendiente de recepción".
                    newIncomeDetail.IncomeStateId = 1;

                    // Set "Reception" and "NextStation"
                    var getProductById = await _unitOfWork.ProductRepository.FindAsync(new ProductWithSubCategoryAndCategorySpecification(newIncomeDetail.IncomeProductId));
                    if (getProductById.Count() > 0)
                    {
                        if (getProductById.First().SubCategory.Prefix == "CT" || getProductById.First().SubCategory.CategoryId == 4)
                        {
                            var getRPSByProductId = await _unitOfWork.ProductRepository.FindAsync(new ProductWithRelProductStationsSpecification(newIncomeDetail.IncomeProductId));
                            if (getRPSByProductId.Count() > 0)
                            {
                                var relProductStations = _mapper.Map<List<RelProductStation>>(getRPSByProductId.First().RelProductStations);
                                for (int i = 0; i <= (relProductStations.Count() - 1); i++)
                                {
                                    newIncomeDetail.Reception = relProductStations[i].Station.Abbreviation + " - " + relProductStations[i].Station.Description;
                                    if ((i + 1) <= (relProductStations.Count() - 1))
                                    {
                                        newIncomeDetail.NextStation = relProductStations[i + 1].Station.Abbreviation + " - " + relProductStations[i + 1].Station.Description;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Reception by default = "R01 - Recepción 01"
                            // ¿Esta mal la NextStation cuando el producto no es CT?
                            newIncomeDetail.NextStation = "";
                        }
                    }

                    newIncomeHeader.IncomeDetails.Add(newIncomeDetail);

                }

                if (newIncomeHeader.IncomeDetails.Count > 0)
                {
                    await _unitOfWork.IncomeHeaderRepository.CreateAsync(newIncomeHeader);
                }

            }
            catch
            {
                throw;
            }
        }

    }
}
