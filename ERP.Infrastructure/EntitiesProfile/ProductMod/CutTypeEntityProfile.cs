using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class CutTypeEntityProfile : IEntityTypeConfiguration<CutType>
    {
        public void Configure(EntityTypeBuilder<CutType> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
