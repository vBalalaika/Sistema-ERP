using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class ProductPieceFormatEntityProfile : IEntityTypeConfiguration<ProductPieceFormat>
    {
        public void Configure(EntityTypeBuilder<ProductPieceFormat> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
