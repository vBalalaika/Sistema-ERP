using ERP.Domain.Entities.Purchases.MissingProducts;

namespace ERP.Application.Specification.Purchases.MissingProducts
{
    public class MissingProductByProductIdAndStateSpecification : BaseSpecification<MissingProduct>
    {
        // Obtengo el producto faltante por id de producto y por estado "Pendiente" o "A cotizar" o "Cotizado"
        public MissingProductByProductIdAndStateSpecification(int productId) : base(mp => mp.ProductId == productId && mp.StateMissingProductId <= 3)
        {

        }
    }
}
