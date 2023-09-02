using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.SupplyVoltageSpecifications
{
    public class ProductSupplyVoltageByProductIdSpecification : BaseSpecification<ProductSupplyVoltage>
    {
        public ProductSupplyVoltageByProductIdSpecification(int productId) : base(psv => psv.ProductId == productId)
        {
            AddInclude(psv => psv.SupplyVoltage);
        }
    }
}
