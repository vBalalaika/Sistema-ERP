using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Logistics.Incomes
{
    public class IncomeHeaderService : GenericService<IncomeHeader, IncomeHeaderDTO>, IIncomeHeaderService
    {
        public IncomeHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(IncomeHeaderDTO entityToCreateDTO)
        {
            await MapDetails(entityToCreateDTO);
            return await base.CreateAsync(entityToCreateDTO);
        }

        public override async Task<int> UpdateAsync(IncomeHeaderDTO entityDtoToUpdate)
        {
            await MapDetails(entityDtoToUpdate);
            return await base.UpdateAsync(entityDtoToUpdate);
        }

        public async Task MapDetails(IncomeHeaderDTO headerDTO)
        {
            IReadOnlyList<IncomeDetail> oldDetails = _unitOfWork.IncomeDetailRepository.GetAll().Result.Where(x => x.IncomeHeaderId == headerDTO.Id).ToList();

            // Create
            foreach (var detail in headerDTO.IncomeDetails.Where(x => x.Id == 0))
            {
                await _unitOfWork.IncomeDetailRepository.CreateAsync(detail);
            }

            // Update
            foreach (var detail in headerDTO.IncomeDetails.Where(x => x.Id != 0 && oldDetails.Any(od => od.Id == x.Id)))
            {
                _unitOfWork.IncomeDetailRepository.Update(detail);
            }

            // Delete
            foreach (var detail in oldDetails.Where(od => !headerDTO.IncomeDetails.Any(x => x.Id != 0 && x.Id == od.Id)))
            {
                await _unitOfWork.IncomeDetailRepository.DeleteAsync(detail.Id);
            }
        }
    }
}
