using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithRelProductStationsSpecification : BaseSpecification<Product>
    {
        public ProductWithRelProductStationsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.RelProductStations);
            AddInclude($"{nameof(Product.RelProductStations)}.{nameof(RelProductStation.Station)}");
        }
    }
}
