using ERP.Domain.Entities.Logistics.Incomes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Logistics.Incomes
{
    public interface IIncomeDetailRepository : IGenericRepository<IncomeDetail>
    {
        Task<IReadOnlyList<IncomeDetail>> GetAll();
    }
}
