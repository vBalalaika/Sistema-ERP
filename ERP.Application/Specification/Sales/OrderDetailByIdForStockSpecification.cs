using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailByIdForStockSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailByIdForStockSpecification(int orderDetailId) : base(od => od.Id == orderDetailId)
        {
            AddInclude(od => od.Product);
            AddInclude(od => od.Structure);

            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(OrderDetail.Structure)}.{nameof(Structure.LastVersion)}");
            AddInclude($"{nameof(OrderDetail.Structure)}.{nameof(Structure.StructureItems)}");

            AddInclude($"{nameof(OrderDetail.Structure)}.{nameof(Structure.StructureItems)}.{nameof(StructureItem.SonProduct)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(OrderDetail.Structure)}.{nameof(Structure.StructureItems)}.{nameof(StructureItem.SonProduct)}.{nameof(Product.ProductFeature)}");
            AddInclude($"{nameof(OrderDetail.Structure)}.{nameof(Structure.StructureItems)}.{nameof(StructureItem.SonStructure)}");

        }
    }
}
