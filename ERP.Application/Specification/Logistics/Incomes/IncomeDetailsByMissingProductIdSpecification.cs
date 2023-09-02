using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Logistics.Incomes
{
    public class IncomeDetailsByMissingProductIdSpecification : BaseSpecification<IncomeDetail>
    {
        public IncomeDetailsByMissingProductIdSpecification(int missingProductId) : base(id => id.MissingProductId.HasValue == true ? id.MissingProductId.Value == missingProductId : false)
        {
            AddInclude(id => id.IncomeHeader);
            AddInclude(id => id.IncomeProduct);
            AddInclude(id => id.FatherProduct);
            AddInclude(id => id.FatherStructure);
            AddInclude(id => id.Unit);
            AddInclude(id => id.IncomeState);

            AddInclude($"{nameof(IncomeDetail.IncomeHeader)}.{nameof(IncomeHeader.Provider)}");
            AddInclude($"{nameof(IncomeDetail.IncomeHeader)}.{nameof(IncomeHeader.TransportProvider)}");
            AddInclude($"{nameof(IncomeDetail.IncomeHeader)}.{nameof(IncomeHeader.ExternalProcessStation)}");

            AddInclude($"{nameof(IncomeDetail.IncomeProduct)}.{nameof(Product.Archives)}");
            AddInclude($"{nameof(IncomeDetail.IncomeProduct)}.{nameof(Product.Archives)}.{nameof(Archive.ArchiveType)}");

            AddInclude($"{nameof(IncomeDetail.PurchaseOrderDetail)}.{nameof(PurchaseOrderDetail.PurchaseOrderHeader)}");

        }
    }
}
