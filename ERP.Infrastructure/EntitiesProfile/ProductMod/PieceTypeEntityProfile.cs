using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class PieceTypeEntityProfile : IEntityTypeConfiguration<PieceType>
    {
        public void Configure(EntityTypeBuilder<PieceType> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
