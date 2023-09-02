using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class ProductPieceTypeEntityProfile : IEntityTypeConfiguration<ProductPieceType>
    {
        public void Configure(EntityTypeBuilder<ProductPieceType> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
