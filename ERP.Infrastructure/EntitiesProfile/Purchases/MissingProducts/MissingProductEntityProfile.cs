using ERP.Domain.Entities.Purchases.MissingProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.MissingProducts
{
    public class MissingProductEntityProfile : IEntityTypeConfiguration<MissingProduct>
    {
        public void Configure(EntityTypeBuilder<MissingProduct> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

        }
    }
}
