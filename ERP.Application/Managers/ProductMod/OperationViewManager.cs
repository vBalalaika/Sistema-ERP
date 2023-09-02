using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Managers.ProductMod
{
    public class OperationViewManager : ViewManager<Operation, OperationDTO>, IOperationViewManager
    {
        private readonly IOperationService _operationservice;
        private readonly IMapper _mapper;

        public OperationViewManager(IOperationService operationservice, IMapper mapper) :
           base(operationservice, mapper)
        {
            _operationservice = operationservice;
            _mapper = mapper;
        }
    }
}