using ERP.Domain.Entities.Logistics.Incomes;

namespace ERP.Application.Specification.Logistics.Incomes
{
    public class IncomeHeaderWithAllSpecifications : BaseSpecification<IncomeHeader>
    {
        public IncomeHeaderWithAllSpecifications()
        {
            AddInclude(ih => ih.Provider);
            AddInclude(ih => ih.TransportProvider);
            AddInclude(ih => ih.ExternalProcessStation);
            AddInclude(ih => ih.IncomeDetails);

            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.IncomeProduct)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.Unit)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.FatherProduct)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.FatherStructure)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.IncomeState)}");
        }

        public IncomeHeaderWithAllSpecifications(int incomeHeaderId) : base(ih => ih.Id == incomeHeaderId)
        {
            AddInclude(ih => ih.Provider);
            AddInclude(ih => ih.TransportProvider);
            AddInclude(ih => ih.ExternalProcessStation);
            AddInclude(ih => ih.IncomeDetails);

            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.IncomeProduct)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.Unit)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.FatherProduct)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.FatherStructure)}");
            AddInclude($"{nameof(IncomeHeader.IncomeDetails)}.{nameof(IncomeDetail.IncomeState)}");
        }
    }
}
