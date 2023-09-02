using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class PurchaseOrderWithAllSpecifications : BaseSpecification<PurchaseOrderHeader>
    {
        public PurchaseOrderWithAllSpecifications()
        {
            AddInclude(x => x.Provider);
            AddInclude(x => x.PurchaseOrderDetails);
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.Product)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.ProviderUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.PriceUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.PurchaseState)}");

            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WidthUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.LengthUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WeightUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PresentationUnitMeasure)}");
        }

        public PurchaseOrderWithAllSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Provider);
            AddInclude(x => x.PurchaseOrderDetails);
            AddInclude(x => x.QuoteRequestResponseHeader);
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.Product)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.ProviderUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.PriceUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.PurchaseState)}");

            AddInclude($"{nameof(PurchaseOrderHeader.Provider)}.{nameof(Provider.City)}");
            AddInclude($"{nameof(PurchaseOrderHeader.Provider)}.{nameof(Provider.State)}");
            AddInclude($"{nameof(PurchaseOrderHeader.Provider)}.{nameof(Provider.Country)}");
            AddInclude($"{nameof(PurchaseOrderHeader.Provider)}.{nameof(Provider.IVACondition)}");
            AddInclude($"{nameof(PurchaseOrderHeader.Provider)}.{nameof(Provider.DocumentType)}");

            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WidthUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.LengthUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WeightUnitMeasure)}");
            AddInclude($"{nameof(PurchaseOrderHeader.PurchaseOrderDetails)}.{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PresentationUnitMeasure)}");
        }
    }
}
